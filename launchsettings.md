# 🚦 LaunchSettings.json en ASP.NET Core

El archivo `launchSettings.json` es un archivo de configuración fundamental que define **cómo se comporta tu aplicación durante el desarrollo y depuración**.  
Se encuentra en la carpeta `Properties` de tu proyecto y te permite personalizar la manera en que se ejecuta la app cuando la lanzas localmente desde Visual Studio, VS Code o usando .NET CLI.  
¡Ideal para adaptar tu entorno de desarrollo a tus necesidades! 🛠️

---

## 📝 ¿Cómo funciona?

Cuando ejecutas tu aplicación en modo desarrollo, `launchSettings.json` le indica a .NET cómo iniciar y con qué parámetros. Permite definir diferentes perfiles de ejecución, cada uno con configuraciones específicas como URL base, variables de ambiente, tipo de servidor web, y más.  
Este archivo **solo afecta el ambiente local de desarrollo**: cualquier configuración necesaria para ambientes externos (producción, staging, etc.) debe moverse a archivos como `appsettings.json` o variables de entorno.

---

## 🗂️ ¿Qué contiene launchSettings.json?

Un archivo típico luce así:

```json
{
  "iisSettings": {
    "windowsAuthentication": false,
    "anonymousAuthentication": true,
    "iisExpress": {
      "applicationUrl": "http://localhost:23307",
      "sslPort": 44333
    }
  },
  "profiles": {
    "https": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "applicationUrl": "https://localhost:7017;http://localhost:5159",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "IIS Express": {
      "commandName": "IISExpress",
      "launchBrowser": true,
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
```

---

## 🧩 Profiles: Múltiples formas de ejecutar tu app

El corazón de este archivo es la sección **profiles**.  
Cada perfil define cómo lanzar la aplicación: qué servidor usar, qué URLs habilitar, si abrir el navegador automáticamente, variables de ambiente, y más.

Por ejemplo:
- **https**: Usa el servidor Kestrel, define URLs HTTP/HTTPS, abre el navegador y setea el entorno a Development.
- **IIS Express**: Usa el servidor IIS Express, ideal para debugging en Visual Studio.

Puedes crear y personalizar tantos perfiles como necesites para distintos escenarios de desarrollo.

---

## 🔍 Descripción de parámetros clave

- **commandName**: Determina el servidor web usado: `Project` (Kestrel), `IISExpress` o `IIS`.
- **launchBrowser**: Si es `true`, abre el navegador predeterminado al iniciar la aplicación.
- **launchUrl**: Si está presente, abre el navegador directamente en la ruta indicada al iniciar la app.
- **dotnetRunMessages**: Muestra mensajes adicionales al ejecutar con .NET CLI.
- **applicationUrl**: URLs base de la app local, separadas por `;` si hay más de una (HTTP y HTTPS).
- **sslPort**: Puerto HTTPS usado por IIS Express.
- **windowsAuthentication**/**anonymousAuthentication**: Habilitan o deshabilitan autenticación Windows o anónima.
- **environmentVariables**: Variables de entorno que se setean automáticamente al lanzar la app (por ejemplo, `ASPNETCORE_ENVIRONMENT`).

---

## 🛠️ ¿Cuándo usar launchSettings.json?

- **Definir variables de ambiente**: Ideal para setear configuraciones según el entorno de desarrollo.
- **Configurar varios perfiles**: Alterna fácilmente entre distintos escenarios de ejecución (ejemplo: debugging, integración, pruebas locales).
- **Customizar la URL y puertos**: Ejecuta la app en puertos específicos, con o sin HTTPS, según tus necesidades.

---

🔙 Indice](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/web-api?tab=readme-ov-file#indice) → [🏠 Main](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/main?tab=readme-ov-file#da2-tecnologia--dise%C3%B1o-de-aplicaciones-2)

## 🧹 Limpieza del archivo

Si solo necesitas un único perfil para Kestrel y tu Web API, puedes simplificar el archivo así:

```json
{
  "profiles": {
    "<<Nombre del negocio>>.WebApi": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "launchUrl": "health",
      "applicationUrl": "https://localhost:7087;http://localhost:5116",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
```

---

## ⚠️ Consideraciones importantes

- Este archivo **no debe usarse para configuraciones de producción**.
- Si necesitas que una variable esté disponible en otros ambientes, usa `appsettings.json` o variables de entorno.
- No almacenes información sensible aquí, ya que suele estar bajo control de versiones.

---

> 💡 **Tip:** ¡Adapta tus perfiles para probar múltiples escenarios y mantén tu entorno de desarrollo tan cómodo como necesites!

---
