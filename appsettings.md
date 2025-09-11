[🔙 Indice](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/web-api?tab=readme-ov-file#indice) → [🏠 Main](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/main?tab=readme-ov-file#da2-tecnologia--dise)

# ⚙️ AppSettings.json en ASP.NET Core

El archivo `appsettings.json` es un **archivo de configuración central** para aplicaciones .NET y ASP.NET Core.  
Aquí se almacenan parámetros como strings de conexión, configuraciones de la aplicación, logging, y cualquier dato necesario para el funcionamiento flexible de la app.  
¡Es el corazón de la configuración dinámica! 💓

---

## 📝 ¿Cómo funciona?

- El archivo es leído en **tiempo de ejecución** y sus valores pueden ser **sobrescritos** automáticamente por archivos de ambiente (`appsettings.Development.json`, `appsettings.Production.json`), *user secrets* o variables de entorno.
- Así, puedes tener valores predeterminados en `appsettings.json` y personalizarlos según el entorno sin tocar el archivo base.
- El orden de prioridad es:
  1. `appsettings.json`
  2. `appsettings.{environment}.json`
  3. User secrets (en desarrollo)
  4. Environment variables 🌎

> ⚡ **Importante:**  
> Si una clave aparece en varias de estas fuentes, **el valor final será el de mayor prioridad** (por ejemplo, una variable de entorno sobreescribe lo definido en los archivos JSON).

---

## 🚫 Seguridad: No pongas secretos aquí

- **No guardes información sensible** (contraseñas, claves secretas, tokens, etc.) en `appsettings.json` si el archivo está bajo control de versiones.  
- Usa **variables de entorno** o sistemas de secretos seguros para guardar configuraciones sensibles.

---

## 📦 Ejemplo típico de appsettings.json

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

- **Logging:** Niveles de logueo para diferentes componentes.
- **AllowedHosts:** Qué dominios pueden hacerle peticiones a la aplicación.

---

## 🏞️ Variables de Ambiente

Las **variables de ambiente** (environment variables) permiten configurar tu aplicación sin modificar archivos, ideal para producción o entornos donde no quieres exponer valores sensibles.  
Puedes definirlas en tu sistema operativo o en los scripts de despliegue, por ejemplo:

- **Windows CMD:**
  ```cmd
  set ASPNETCORE_ENVIRONMENT=Development
  set ConnectionStrings__DefaultConnection=SecretConnectionString
  ```
- **Linux/Mac:**
  ```bash
  export ASPNETCORE_ENVIRONMENT=Development
  export ConnectionStrings__DefaultConnection=SecretConnectionString
  ```

Las variables de ambiente tienen la **máxima prioridad** y sobreescriben cualquier valor definido en los archivos JSON.

---

## 🔒 User Secrets: Manejo seguro de secretos en desarrollo

Las **User Secrets** son una funcionalidad de ASP.NET Core para almacenar información sensible localmente durante el desarrollo, sin escribirla en los archivos del proyecto ni subirla al repositorio.

### 🤔 ¿Qué son las User Secrets?

- Es un almacenamiento seguro y local de claves y valores, asociado a cada proyecto .NET.
- Solo se usan en **entorno de desarrollo** (cuando `ASPNETCORE_ENVIRONMENT=Development`).
- Los secretos se guardan fuera del árbol de archivos del proyecto, en una carpeta del usuario.

### 🚀 ¿Cómo se configuran?

1. **Inicializa User Secrets en tu proyecto:**
   En la terminal, ubícate en la carpeta del proyecto y ejecuta:
   ```bash
   dotnet user-secrets init
   ```
   Esto agrega una referencia `UserSecretsId` en tu archivo `.csproj`.

2. **Agrega secretos:**
   Por ejemplo, para agregar una cadena de conexión:
   ```bash
   dotnet user-secrets set "ConnectionStrings:DefaultConnection" "MiSuperConnectionString"
   ```

3. **Lee los valores en tu app:**
   La aplicación los leerá automáticamente igual que si estuvieran en `appsettings.Development.json`.

4. **Ver, editar o eliminar secretos:**
   - Listar todos los secretos:
     ```bash
     dotnet user-secrets list
     ```
   - Eliminar un secreto:
     ```bash
     dotnet user-secrets remove "ConnectionStrings:DefaultConnection"
     ```
   - Eliminar todos los secretos:
     ```bash
     dotnet user-secrets clear
     ```

> 📖 Más info: [Documentación oficial de User Secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-9.0&tabs=windows)

---

## 👍 Buenas Prácticas

- 🚫 **Evita credenciales hard-coded:** No guardes información sensible aquí si el archivo va al repositorio.
- 🌱 **Usa archivos de ambiente:** Sobrescribe lo necesario en `appsettings.{environment}.json`.
- 🔑 **Usa User Secrets en desarrollo:** No subas secretos al repositorio, usa `dotnet user-secrets`.
- 🌎 **Usa variables de ambiente en producción:** Configura los valores sensibles y finales en variables de entorno.
- 🔍 **Control de versiones:** Si incluyes este archivo en git, asegúrate de no violar la primera práctica.

---

> 💡 **Tip**: Puedes modificar el orden y fuentes de configuración si tu aplicación lo requiere (por ejemplo, usando código en `Program.cs`).

---
