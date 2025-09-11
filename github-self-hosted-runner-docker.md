[Volver - Main](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/main)

# 🚀 Guía para Correr un GitHub Self-Hosted Runner en Docker

## 1. Introducción

GitHub Actions permite automatizar tareas como compilar código, correr tests o desplegar aplicaciones. Por defecto, GitHub ejecuta estos procesos en runners propios (*hosted runners*), pero para DA2 a menudo es preferible usar **runners self-hosted**.

### ¿Por qué usar un runner self-hosted?

- No consume minutos del plan gratuito de GitHub.
- Puede acceder a recursos privados de tu red.
- Suele ser más rápido y flexible.
- Permite configuraciones personalizadas.

### ¿Por qué en Docker?

- Aísla el runner del sistema operativo principal.
- Permite levantarlo y bajarlo fácilmente con `docker-compose`.
- Facilita la portabilidad y el trabajo en equipo.

---

### ❗ ¡Importante! Todos deben tener el runner activo

El modelo de runners self-hosted funciona como una **pila compartida de runners**. Si solo uno o dos compañeros tienen el runner encendido, **todos los workflows del equipo** (incluso los de otros miembros) quedarán pendientes si nadie más lo tiene activo.

**Por eso, cada colaborador debe tener su contenedor de runner activo mientras trabaja.**

---

## 📽️ Video explicativo

Si prefieres ver el proceso explicado paso a paso, puedes ver este video de apoyo:

[Ver video en Google Drive](https://drive.google.com/file/d/12C_hM3mBgsa5fr4xPsV5tHlclZOufeVA/view)

---

## 2. Organización y Estructura de Archivos

El entorno del runner debe estar **fuera del repositorio de código**, idealmente en una carpeta llamada:

```
<nombre-del-repo>-self-hosted-runner
```

Por ejemplo, si tu repo es `app-control-remoto`, la carpeta sería:

```
|--app-control-remoto
|--app-control-remoto-self-hosted-runner
|   |-.env
|   |-Dockerfile
|   |-docker-compose.yml
|   |-entrypoint.sh
```

> ⚠️ **No subas estos archivos al repo de código.** Son solo para tu entorno local.

---

## 3. Configuración del archivo `.env`

Crea el archivo `.env` dentro de la carpeta del runner con:

```
REPO_URL=<URL del repositorio DA2>
GITHUB_PAT=<tu personal access token>
RUNNER_NAME=<tu usuario de GitHub>
ARCH=<x64 o arm64>
```

- `REPO_URL`: URL **sin** `.git` al final. Ejemplo: `https://github.com/IngSoft-DA2/DA2-Tecnologia`
- `GITHUB_PAT`: [Genera un token personal](https://github.com/settings/tokens) con permisos: `repo`, `workflow`, `read:org`, `admin:repo_hook`.
- `RUNNER_NAME`: Tu usuario de GitHub.
- `ARCH`: Tu arquitectura (`x64` para Intel, `arm64` para Mac M1/M2/M3/M4/Snapdragon).

**Verifica que el `.env` esté junto a los otros archivos del runner.**

---

### 🛠 Cómo probar tu PAT

Puedes probar tu token con Postman:

- POST a: `https://api.github.com/repos/<OWNER>/<REPO>/actions/runners/registration-token`
- Header: `Authorization: token <TU_GITHUB_PAT>`
- Header: `Accept: application/vnd.github+json`

Si es válido, recibes un JSON con un `token` y un `expires_at`.

---

## 4. Dockerfile

Crea un archivo `Dockerfile` en la carpeta del runner:

```Dockerfile
FROM ubuntu:22.04

ARG ARCH
ENV DEBIAN_FRONTEND=noninteractive

RUN apt-get update && \
    apt-get install -y curl tar git jq sudo python3 python3-pip wget apt-transport-https ca-certificates gnupg software-properties-common && \
    apt-get clean

RUN wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb && \
    dpkg -i packages-microsoft-prod.deb && \
    rm packages-microsoft-prod.deb && \
    apt-get update && \
    apt-get install -y dotnet-sdk-8.0 && \
    apt-get clean

RUN useradd -m runner && mkdir -p /runner && chown runner:runner /runner
WORKDIR /runner
USER runner

ENV RUNNER_VERSION=2.323.0

RUN curl -o actions-runner.tar.gz -L https://github.com/actions/runner/releases/download/v${RUNNER_VERSION}/actions-runner-linux-${ARCH}-${RUNNER_VERSION}.tar.gz && \
    tar xzf ./actions-runner.tar.gz && rm actions-runner.tar.gz

COPY --chown=runner:runner entrypoint.sh /entrypoint.sh
RUN chmod +x /entrypoint.sh

USER root
RUN ./bin/installdependencies.sh
RUN mkdir -p /runner/_work && chown -R runner:runner /runner
USER runner

ENTRYPOINT ["/entrypoint.sh"]
```

---

## 5. Script de inicio `entrypoint.sh`

Crea el archivo `entrypoint.sh` con permisos de ejecución:

```bash
#!/bin/bash
set -e

if [[ -z "$GITHUB_PAT" || -z "$REPO_URL" || -z "$RUNNER_NAME" ]]; then
  echo "Missing required environment variables: GITHUB_PAT, REPO_URL or RUNNER_NAME"
  exit 1
fi

REPO_PATH=$(echo "$REPO_URL" | sed -E 's|https://github.com/||')
API_URL="https://api.github.com/repos/$REPO_PATH/actions/runners/registration-token"

register_runner() {
  echo "Fetching registration token for $REPO_PATH..."
  RUNNER_TOKEN=$(curl -sX POST -H "Authorization: token $GITHUB_PAT" \
    -H "Accept: application/vnd.github+json" "$API_URL" | jq -r .token)

  if [[ "$RUNNER_TOKEN" == "null" || -z "$RUNNER_TOKEN" ]]; then
    echo "❌ Failed to fetch registration token"
    exit 1
  fi

  echo "Registering the runner..."
  ./config.sh \
    --url "$REPO_URL" \
    --token "$RUNNER_TOKEN" \
    --name "$RUNNER_NAME" \
    --work _work \
    --unattended \
    --replace
}

cleanup() {
  echo "Removing runner from GitHub..."
  REMOVE_TOKEN=$(curl -sX POST -H "Authorization: token $GITHUB_PAT" \
    -H "Accept: application/vnd.github+json" "$API_URL" | jq -r .token)

  if [[ "$REMOVE_TOKEN" != "null" && -n "$REMOVE_TOKEN" ]]; then
    ./config.sh remove --unattended --token "$REMOVE_TOKEN"
  else
    echo "⚠️ Could not obtain removal token; skipping unregister"
  fi
}

trap 'cleanup; exit 130' INT
trap 'cleanup; exit 143' TERM

if [[ -f ".runner" ]]; then
  echo "✅ Existing runner config found, reusing it..."
else
  echo "🆕 No config found, registering new runner..."
  register_runner
fi

echo "🚀 Starting runner..."
./run.sh
```

---

## 6. Archivo `docker-compose.yml`

Crea el archivo `docker-compose.yml` en la misma carpeta:

```yaml
services:
  da2-self-hosted-runner:
    build:
      context: .
      args:
        ARCH: ${ARCH}
    image: dotnet-self-hosted-runner
    container_name: da2-self-hosted-runner

    environment:
      REPO_URL: ${REPO_URL}
      GITHUB_PAT: ${GITHUB_PAT}
      RUNNER_NAME: ${RUNNER_NAME}

    volumes:
      - runner-data:/runner/_work
    restart: unless-stopped

volumes:
  runner-data:
```
> Cambia `da2-self-hosted-runner` por un nombre representativo si lo deseas.

---

## 7. Levantar el runner

Abre la terminal, navega a la carpeta y ejecuta:

```bash
docker-compose up --build
```

Esto construye la imagen y levanta el contenedor, registrando el runner automáticamente.

---

## 8. Verificar el runner en GitHub

- Ve a tu repo en GitHub.
- Settings → Actions → Runners
- Deberías ver tu runner con el nombre que pusiste en `RUNNER_NAME` y en estado **Idle**.

---

## 9. Consejos y Troubleshooting

- **Siempre debe haber al menos un runner activo** o los workflows quedarán pendientes.
- Si tu runner queda inactivo, simplemente vuelve a levantar el contenedor.
- Si eliminas el contenedor sin desregistrar el runner, GitHub lo mostrará como inactivo. Puedes eliminarlo manualmente desde Settings → Actions → Runners.
- Para ver logs:  
  ```bash
  docker logs -f da2-self-hosted-runner
  ```

---

## 10. Links útiles

- [Documentación oficial GitHub Self-Hosted Runners](https://docs.github.com/en/actions/hosting-your-own-runners/managing-self-hosted-runners/about-self-hosted-runners)

---

Con este setup, tu equipo tendrá un entorno CI robusto, distribuido y alineado a las buenas prácticas profesionales. ¡No olvides mantener tu runner activo mientras trabajas! 👨‍💻🚦
