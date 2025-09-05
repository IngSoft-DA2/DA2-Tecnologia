[Volver - Main](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/main)

# Correr GitHub Self-Hosted Runner en un Docker Container

## 1. Introducción

GitHub Actions es una herramienta que permite automatizar tareas dentro del flujo de trabajo de un proyecto, como compilar código, correr tests o desplegar aplicaciones.

Por defecto, GitHub ejecuta estos procesos en máquinas virtuales que ellos mismos proveen (llamadas *runners hospedados por GitHub*). Sin embargo, también es posible usar nuestras propias máquinas para ejecutar estas tareas, lo que se conoce como un **runner auto-hospedado (self-hosted runner)**.

### ¿Por qué usar un runner self-hosted?

Usar un runner auto-hospedado tiene varias ventajas:

- No consume minutos del plan gratuito.
- Puede tener acceso a recursos o archivos privados de tu red.
- Puede ser más rápido o potente que los runners de GitHub.
- Permite usar sistemas operativos o configuraciones personalizadas.

### ¿Por qué correrlo en Docker?

Ejecutar el runner dentro de un contenedor Docker agrega más beneficios:

- Aísla el runner del sistema operativo principal.
- Permite levantarlo y bajarlo fácilmente con `docker-compose`.
- Facilita la portabilidad y la replicación del entorno.

En esta guía vamos a ver cómo correr un self-hosted runner de GitHub dentro de un contenedor Docker, paso a paso.

### ¿Por qué cada colaborador debe configurar el contenedor del self-hosted runner en su entorno local?

El modelo de runner self-hosted en contenedor funciona como una **pila compartida de runners registrados** en el repositorio. GitHub Actions no asigna un workflow a un runner específico por colaborador, sino que simplemente envía el trabajo al primer runner disponible que cumpla con los requisitos del job.

Esto significa que:

- Si solo uno o dos colaboradores tienen sus contenedores activos, **todos los workflows del equipo (incluidos pull requests de otros)** se ejecutarán en esos runners.
- Si ninguno de los runners está activo en el momento de la ejecución, **los workflows quedarán pendientes indefinidamente**, afectando la automatización y el flujo de trabajo.
- No hay garantía de que el runner local de un colaborador ejecute exclusivamente sus propios workflows; **el primer runner disponible es el que toma el trabajo**, sin importar quién hizo el PR o el push.

Por eso, es fundamental que **cada colaborador configure y mantenga activo su propio contenedor de runner en su entorno local**, ya que:

- **Se incrementa la disponibilidad global de runners**, reduciendo el riesgo de bloqueos por falta de recursos.
- **Se distribuye la carga de ejecución de workflows**, permitiendo trabajo en paralelo y evitando saturar un único contenedor.
- **Se mantiene el flujo de CI funcionando de forma autónoma**, sin depender del estado de una única máquina o usuario.

Este enfoque colaborativo asegura que el sistema de automatización del repositorio sea resiliente, distribuido y siempre disponible durante el desarrollo.

## 3. Organización del proyecto

Para que el runner funcione correctamente, los archivos que vamos a ver en los pasos siguientes (**.env**, **docker-compose.yml**, **Dockerfile** y **entrypoint.sh**) deben estar agrupados dentro de un mismo directorio.

Ese directorio **debe ubicarse por fuera del repositorio de código que va a usar el runner**, ya que cumple una función separada: alojar el entorno del runner.

Crearemos este directorio o carpeta en nuestra maquina y **no lo subiremos a GitHub** dado que es una configuracion que utilizaremos localmente en todo momento.

### 📂 Nombre del directorio

El nombre del directorio debe seguir esta convención:

Donde `nombre-del-repositorio` es el nombre real del repositorio de GitHub donde se quiere registrar el runner.

Por ejemplo, si el repositorio se llama `app-control-remoto`, el directorio del runner debería llamarse:


> ⚠️ Es muy importante **no poner estos archivos dentro del repositorio GitHub que va a usar el runner**. El runner debe ejecutarse de forma externa, no como parte del código del proyecto.

### 📁 Estructura esperada

Una posible estructura sería la siguiente:

```
|--app-control-remoto
|--app-control-remoto-self-hosted-runner
| |-.env
| |-Dockerfile
| |-docker-compose.yml
| |-entrypoint.sh
```

De esta forma, el runner está bien separado del código y se puede configurar, iniciar o detener sin afectar al repositorio principal.

## 4. Configurar el archivo `.env`

El archivo `.env` contiene las variables necesarias para registrar y ejecutar el runner dentro del contenedor Docker. Estas variables permiten vincular el runner con un repositorio específico y asignarle un nombre.

Dentro del directorio `nombre-del-repositorio-self-hosted-runner`, creá un archivo llamado `.env` con el siguiente contenido:

```
REPO_URL: <url del repositorio del obligatorio>
GITHUB_PAT: <personal access token>
RUNNER_NAME: <su cuenta de github>
ARCH: <tipo de arquitectura de su maquina> 
```

### Explicación de las variables

- `REPO_URL`: Es la URL del repositorio de GitHub donde se quiere registrar el runner. Este runner solo podrá ejecutar workflows de ese repositorio. Ejemplo de URL: `https://github.com/IngSoft-DA2/DA2-Tecnologia`

- `GITHUB_PAT`: Es un **token de acceso personal (Personal Access Token)** que debe tener permisos para administrar runners en ese repositorio.  
  > ⚠️ Este valor es sensible y **no debe compartirse públicamente ni subirse a GitHub**.

- `RUNNER_NAME`: Es el nombre que tendrá el runner dentro del repositorio de GitHub. El nombre debe ser el username de github.

- `ARCH`: Es el tipo de arquitectura de la maquina host, acepta dos valores posibles `x64` o `arm64`
    - Intel usa `x64`
    - MacOS con chip m1, m2, m3, m4 usa `arm64`
    - Chip Snapdragon `arm64`

> ✅ Asegurate de que el archivo `.env` esté guardado en el mismo directorio que el `docker-compose.yml`, `Dockerfile` y `entrypoint.sh`.

### 🛠 Cómo generar el `GITHUB_PAT`

1. Iniciá sesión en [https://github.com](https://github.com).
2. Hacé clic en tu foto de perfil (arriba a la derecha) y elegí **"Settings"**.
3. En el menú de la izquierda, selecciona **"Developer settings"** situado abajo del todo.
4. Ingresá a la sección **"Personal access tokens"** → **"Tokens (classic)"**.
5. Hacé clic en el botón **"Generate new token"** → **"Generate new token (classic)"**.
6. Completá los siguientes campos:
   - **Note**: Escribí un nombre para el token: `nombre-del-repositorio-self-hosted-runner`, sustituyendo `nombre-del-repositorio` por el nombre del repositorio del obligatorio.
   - **Expiration**: Elegir valor **No expiration**.
   - **Scopes** (permisos): Activá la opción:
     - `repo` → Para acceso a repositorios privados.
     - `workflow`
     - `read:org`
     - `admin:repo_hook` → Para administrar webhooks y runners.
7. Hacé clic en **"Generate token"**.
8. Copiá el token generado. **Solo se muestra una vez**, así que guardalo en un lugar seguro.

Pegá ese token en el archivo `.env` como valor de la variable `GITHUB_PAT`.

---

### ✅ Cómo probar que el `GITHUB_PAT` funciona

Este token se va a usar internamente para obtener un **registration token**, que es necesario para que el runner se registre correctamente en el repositorio.

Podés probar si el `GITHUB_PAT` funciona correctamente utilizando **Postman**:
1. Abrí Postman y creá una nueva petición `POST`.
2. URL: https://api.github.com/repos/IngSoft-DA2/**NOMBRE DE TU REPO**/actions/runners/registration-token

(Reemplazá **NOMBRE DE TU REPO** por el nombre de tu repo)

3. En la pestaña **Headers**, agregá: 
    - **Key**: `Authorization`, **Value**: `token TU_GITHUB_PAT`
    
      (Reemplazá `TU_GITHUB_PAT` por el token que generaste)
    
    - **Key**: `Accept`, **Value**: `application/vnd.github+json`
4. En la pestaña **Body**, dejá la opción **raw** vacía (el endpoint no requiere contenido).
5. Hacé clic en **Send**.

Si el token es válido, vas a recibir una respuesta con un JSON que contiene un `token` y un `expires_at`. Ese es el token de registro que el runner usará internamente.

> 🧪 Si recibís un error (por ejemplo, 401 Unauthorized), es probable que el token esté mal escrito, vencido, o no tenga los permisos necesarios.

## 5. Crear el archivo `Dockerfile`

El archivo `Dockerfile` define cómo construir la imagen del contenedor que actuará como runner. Este contenedor tiene que estar preparado para ejecutar los jobs que GitHub Actions le asigne, por eso se instalan herramientas como .NET SDK y Python.

También se copia y utiliza un archivo llamado `entrypoint.sh`, que es un script responsable de registrar el runner automáticamente al iniciar el contenedor.

Dentro del directorio `nombre-del-repositorio-self-hosted-runner`, creá un archivo llamado `Dockerfile` con el siguiente contenido:

```Dockerfile
FROM ubuntu:22.04

ARG ARCH

# Avoid interactive questions during installation
ENV DEBIAN_FRONTEND=noninteractive

# Install dependencies including Python and .NET prerequisites
RUN apt-get update && \
    apt-get install -y \
    curl \
    tar \
    git \
    jq \
    sudo \
    python3 \
    python3-pip \
    wget \
    apt-transport-https \
    ca-certificates \
    gnupg \
    software-properties-common && \
    apt-get clean

# Install .NET SDK 8.0
RUN wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb && \
    dpkg -i packages-microsoft-prod.deb && \
    rm packages-microsoft-prod.deb && \
    apt-get update && \
    apt-get install -y dotnet-sdk-8.0 && \
    apt-get clean

# Create a non-root user and working directory
RUN useradd -m runner && mkdir -p /runner && chown runner:runner /runner
WORKDIR /runner

# Switch to non-root user
USER runner

# Set version of GitHub Actions runner
ENV RUNNER_VERSION=2.323.0

# Download and extract the GitHub Actions runner
RUN curl -o actions-runner.tar.gz -L https://github.com/actions/runner/releases/download/v${RUNNER_VERSION}/actions-runner-linux-${ARCH}-${RUNNER_VERSION}.tar.gz && \
    tar xzf ./actions-runner.tar.gz && \
    rm actions-runner.tar.gz

COPY --chown=runner:runner entrypoint.sh /entrypoint.sh
RUN chmod +x /entrypoint.sh

# Run dependency installer as root
USER root
RUN ./bin/installdependencies.sh

RUN mkdir -p /runner/_work && chown -R runner:runner /runner

# Go back to non-root user for safety
USER runner

ENTRYPOINT ["/entrypoint.sh"]
```

Este contenedor:

- Usa como base `ubuntu:22.04`.
- Instala herramientas necesarias como `curl`, `git`, `python3`, `dotnet-sdk`, etc.
- Descarga la versión 2.323.0 del **GitHub Actions runner**.
- Copia y prepara el script `entrypoint.sh`, que será el encargado de registrar y ejecutar el runner automáticamente cada vez que se inicie el contenedor.

En el siguiente paso explicamos en detalle qué hace el archivo `entrypoint.sh`.

## 6. Crear el archivo `entrypoint.sh`

El archivo `entrypoint.sh` es un script de inicio que se ejecuta cuando se inicia el contenedor. Este script se encarga de registrar el runner con el repositorio de GitHub usando el **registration token** y luego ejecuta el runner en sí.

Dentro del directorio `nombre-del-repositorio-self-hosted-runner`, creá el archivo `entrypoint.sh` con el siguiente contenido:

```bash
#!/bin/bash
set -e

if [[ -z "$GITHUB_PAT" || -z "$REPO_URL" || -z "$RUNNER_NAME" ]]; then
  echo "Missing required environment variables: GITHUB_PAT, REPO_URL or RUNNER_NAME"
  exit 1
fi

# Extract owner/repo path
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

  # Attempt to get a fresh token for removal
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

# Register only if no existing config
if [[ -f ".runner" ]]; then
  echo "✅ Existing runner config found, reusing it..."
else
  echo "🆕 No config found, registering new runner..."
  register_runner
fi

# Start the runner
echo "🚀 Starting runner..."
./run.sh
```

### ¿Qué hace el script `entrypoint.sh`?

- **Registra el Runner**: Usa el `GITHUB_PAT` que se definió en el archivo `.env` para obtener el **registration token** desde el repositorio de GitHub. Este token es necesario para vincular el runner con el repositorio.

- **Configura el Runner**: Una vez que se obtiene el token de registro, el script configura el runner con el nombre definido en la variable `RUNNER_NAME`.

- **Inicia el Runner**: Después de registrar el runner, el script inicia el servicio del GitHub Actions runner, lo que lo pone en espera de trabajos (jobs) que se asignen desde GitHub.

Este archivo es crucial para que el contenedor funcione correctamente como runner self-hosted, ya que asegura que el runner esté registrado y listo para ejecutar las acciones de GitHub.

## 7. Crear el archivo `docker-compose.yml`

El archivo `docker-compose.yml` permite configurar y gestionar fácilmente los contenedores que vamos a ejecutar, incluyendo el contenedor con el GitHub Actions runner que hemos creado previamente. Usaremos Docker Compose para definir el servicio del runner y las variables de entorno necesarias, así como la relación con el archivo `.env` para que todo funcione correctamente.

Dentro del directorio `nombre-del-repositorio-self-hosted-runner`, creá el archivo `docker-compose.yml` con el siguiente contenido:

```yaml
services:
  nombre-de-la-negocio-self-hosted-runner:
    build:
      context: .
      args:
        ARCH: ${ARCH}
    image: dotnet-self-hosted-runner
    container_name: nombre-del-negocio-self-hosted-runner

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
*Sustituir donde dice **nombre-del-negocio** por el nombre del negocio*

### 1. Servicios (`services`)

- **build**: Instrucción que indica que Docker debe construir la imagen a partir del `Dockerfile` ubicado en el directorio actual (`.`).

- **image**: Define el nombre de la imagen del contenedor que se construirá.

- **container_name**: Le asigna un nombre al contenedor para facilitar la referencia en comandos de Docker.

- **environment**: Define las variables de entorno que serán utilizadas dentro del contenedor. Se hace uso de las variables definidas en el archivo `.env` (`REPO_URL`, `GITHUB_PAT`, `RUNNER_NAME`).

### 2. Sección `volumes`

La sección `volumes` en Docker Compose permite montar directorios o volúmenes dentro del contenedor. En este caso, estamos montando un volumen llamado `runner-data` en el directorio `/runner/_work` dentro del contenedor.

- **Volumen `runner-data`**: Es un volumen persistente que se crea automáticamente cuando Docker Compose se ejecuta. El propósito de este volumen es almacenar el trabajo del runner, lo que significa que cualquier archivo generado o modificado por el GitHub Actions runner (como los resultados de las tareas) se guardará en este volumen, incluso si el contenedor se detiene o reinicia. Esto asegura que los datos no se pierdan entre ejecuciones.

### 3. Sección `restart`

La política de reinicio `restart` controla cómo Docker maneja los reinicios del contenedor.

- **`restart: unless-stopped`**: Esto significa que el contenedor se reiniciará automáticamente si se detiene, salvo que se haya detenido manualmente. Esto es útil para mantener el runner siempre en funcionamiento, incluso si el contenedor falla o si el sistema se reinicia. Sin embargo, si el contenedor es detenido explícitamente (por ejemplo, con `docker stop`), no se reiniciará hasta que se inicie de nuevo manualmente.

### 4. Sección `volumes` fuera de `services`

```yaml
volumes:
  runner-data:
```
Esta sección define el volumen runner-data que se usará para almacenar los datos del contenedor. Docker gestionará este volumen automáticamente, asegurando que los archivos de trabajo del runner persistan entre reinicios del contenedor.

Con este archivo `docker-compose.yml`, podés iniciar y gestionar el contenedor del GitHub Actions runner con facilidad, asegurando que los datos del runner no se pierdan y que el contenedor se reinicie automáticamente cuando sea necesario.

### ¿Qué hace el archivo `docker-compose.yml`?

- **Define el servicio**: Define el contenedor que ejecutará el GitHub Actions runner. Este servicio depende de las configuraciones del `Dockerfile` y utiliza el `entrypoint.sh` para iniciar el runner automáticamente al ejecutar el contenedor.
  
- **Usa el archivo `.env`**: Hace uso del archivo `.env` para cargar las variables de entorno necesarias (como `GITHUB_PAT`, `REPO_URL` y `RUNNER_NAME`) sin tener que incluirlas directamente en el archivo `docker-compose.yml`. Esto asegura que las credenciales y configuraciones no estén expuestas en el archivo de configuración del contenedor.

---

Ahora que hemos configurado todos los archivos necesarios, ya podés comenzar a probar el contenedor y verificar que el GitHub Actions runner esté registrado correctamente en tu repositorio de GitHub.

## Paso 8: Levantar el contenedor del runner

Una vez que todos los archivos necesarios estén creados (`.env`, `Dockerfile`, `entrypoint.sh`, `docker-compose.yml`) y ubicados dentro del directorio correspondiente (`nombre-del-repo-self-hosted-runner`), ya podés iniciar el contenedor del runner.

### Comando para levantar el contenedor

Desde la terminal, ubicándote dentro del directorio donde están los archivos mencionados, ejecutá el siguiente comando:

```bash
docker-compose up --build
```
Este comando:

- Construirá la imagen del contenedor a partir del `Dockerfile`.
- Iniciará el contenedor en segundo plano (`-d` significa *detached mode*).
- Registrará automáticamente el runner en el repositorio utilizando el `GITHUB_PAT`.
- Dejará el runner en estado "escuchando", listo para ejecutar los trabajos que reciba desde GitHub Actions.

> **Importante:** Asegurate de tener Docker Compose instalado y funcionando correctamente en tu máquina antes de ejecutar este paso.

### Verificación de salida

Si no hubo errores durante el proceso de construcción e inicio del contenedor, deberías ver una impresión en consola similar a la siguiente (podés verla usando `docker logs <nombre-del-contenedor>` si estás en modo detached):

```bash
--------------------------------------------------------------------------------
workflow-test-github-runner  | |        ____ _ _   _   _       _          _        _   _                      |                                                                                            
workflow-test-github-runner  | |       / ___(_) |_| | | |_   _| |__      / \   ___| |_(_) ___  _ __  ___      |                                                                                            
workflow-test-github-runner  | |      | |  _| | __| |_| | | | | '_ \    / _ \ / __| __| |/ _ \| '_ \/ __|     |                                                                                            
workflow-test-github-runner  | |      | |_| | | |_|  _  | |_| | |_) |  / ___ \ (__| |_| | (_) | | | \__ \     |                                                                                            
workflow-test-github-runner  | |       \____|_|\__|_| |_|\__,_|_.__/  /_/   \_\___|\__|_|\___/|_| |_|___/     |                                                                                            
workflow-test-github-runner  | |                                                                              |
workflow-test-github-runner  | |                       Self-hosted runner registration                        |                                                                                            
workflow-test-github-runner  | |                                                                              |                                                                                            
workflow-test-github-runner  | --------------------------------------------------------------------------------                                                                                            
workflow-test-github-runner  |                                                                                                                                                                             
workflow-test-github-runner  | # Authentication                                                                                                                                                            
workflow-test-github-runner  |                                                                                                                                                                             
workflow-test-github-runner  | 
workflow-test-github-runner  | √ Connected to GitHub
workflow-test-github-runner  |                                                                                                                                                                             
workflow-test-github-runner  | # Runner Registration
workflow-test-github-runner  |                                                                                                                                                                             
workflow-test-github-runner  |                                                                                                                                                                             
workflow-test-github-runner  | 
workflow-test-github-runner  |                                                                                                                                                                             
workflow-test-github-runner  | A runner exists with the same name                                                                                                                                          
workflow-test-github-runner  | √ Successfully replaced the runner
workflow-test-github-runner  | √ Runner connection is good
workflow-test-github-runner  | 
workflow-test-github-runner  | # Runner settings                                                                                                                                                           
workflow-test-github-runner  |                                                                                                                                                                             
workflow-test-github-runner  |                                                                                                                                                                             
workflow-test-github-runner  | √ Settings Saved.                                                                                                                                                           
workflow-test-github-runner  |                                                                                                                                                                             
workflow-test-github-runner  |                                                                                                                                                                             
workflow-test-github-runner  | √ Connected to GitHub
workflow-test-github-runner  |                                                                                                                                                                             
workflow-test-github-runner  | Current runner version: '2.323.0'                                                                                                                                           
workflow-test-github-runner  | 2025-04-17 19:24:02Z: Listening for Jobs
√ Connected to GitHub
2025-04-17 10:32:15Z: Listening for Jobs
```
*Donde dice workflow-test deberia decir el nombre del repositorio del obligatorio*

## Paso 9: Verificar el registro en GitHub

Una vez que el contenedor se encuentra en ejecución y ves en los logs que el runner está **"Listening for Jobs"**, podés verificar que el registro haya sido exitoso directamente desde GitHub.

### ¿Dónde verificarlo?

1. Ingresá al repositorio en GitHub donde estás registrando el runner.
2. Hacé clic en la pestaña **Settings** (Configuración).
3. En el menú lateral, seleccioná **Actions** > **Runners**.
4. Deberías ver tu runner registrado con el nombre que definiste en la variable `RUNNER_NAME`.

Si todo salió bien, el runner aparecerá en estado **Idle**, lo que indica que está listo para ejecutar jobs.

> 🟢 Esto confirma que tu runner self-hosted está correctamente vinculado al repositorio y funcionando.

## Paso 10: Consideraciones y troubleshooting

A continuación, se listan algunos puntos importantes a tener en cuenta para garantizar el correcto funcionamiento de los runners self-hosted:

### 🧠 Consideraciones importantes

- **Siempre debe haber al menos un runner en ejecución:**  
  Si no hay ningún runner activo en el equipo, los workflows de GitHub Actions (como los que se ejecutan en los Pull Requests) quedarán en estado "pendiente" indefinidamente hasta que se levante un runner.

- **Las etiquetas (`labels`) en los PRs ya no se crean automáticamente:**  
  Al usar los **self hosted runners** las etiquetas de los PRs que bloqueaban el PR en caso de encontrar un error ya no existen.

- **Podés reintentar jobs fallidos sin hacer nuevos commits:**  
  Si una ejecución falla (por ejemplo, por un error de red, permisos, o por un problema temporal del runner), podés reintentar el job desde la interfaz de GitHub usando el botón **"Re-run jobs"**, sin necesidad de modificar el código ni realizar un nuevo commit.

### 🛠️ Troubleshooting

- **Errores del runner (no de build/test/coverage):**  
  Si el runner presenta errores inesperados, como problemas de conexión o errores en el registro, muchas veces alcanza con **detener el contenedor y volverlo a levantar**. Esto restablece el estado del runner sin afectar los archivos del volumen persistente.

- **Verificá los logs del contenedor:**  
  Podés revisar los logs del runner con el siguiente comando:
  ```bash
    docker logs -f nombre-de-tu-repo-self-hosted-runner
  ```

- **Limpiá runners obsoletos desde GitHub si es necesario**
  Si eliminás un contenedor sin desregistrar el runner primero, GitHub seguirá mostrando ese runner como **inactivo**.  
  Podés eliminarlo manualmente desde: **Settings > Actions > Runners** en el repositorio del obligatorio.

---

Con estos pasos y recomendaciones, tu equipo ya puede usar **GitHub Actions** de manera eficiente con **runners propios**, optimizando tiempos y recursos.

### Links de interes:
- [Github self hosted runner](https://docs.github.com/en/actions/hosting-your-own-runners/managing-self-hosted-runners/about-self-hosted-runners)
