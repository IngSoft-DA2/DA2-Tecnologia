# LaunchSettings.json

Es un archivo de configuración usado para configurar varios aspectos sobre cómo nuestra aplicación debería de ser ejecutada y debugeada durante el desarrollo. Este archivo se puede encontrar en la carpeta `Properties` y es usado principalmente por Visual Studio y .NET CLI.

Las configuraciones que este archivo contiene, serán usadas cuando corramos nuestra web api desde Visual Studio o por comandos usando .NET CLI. El punto más importante es que este archivo solo es usado de forma local en el desarrollo. Este archivo no es requerido cuando nosotros despleguemos la aplicación en un servidor de producción.

Cualquier configuración que se ponga en este archivo que se quiera utilizar en cualquier otro ambiente que no sea local, deberá de ser movida para el archivo `appsettings.json`.

En este archivo encontraremos lo siguiente:

```C#
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

## Profiles

El archivo contiene diferentes perfiles para ejecutar la aplicación. Cada perfil puede especificar diferentes configuraciones como la URL a usar, variables de ambiente, etc. Estos perfiles pueden ayudar a setear diferentes ambientes para el desarrollo, como un ambiente de prueba para probar la aplicación con diferentes configuraciones sin cambiar el código.

En este `launchsettings.json` podemos encontrar dos secciones, `IIS Express` y `https`. El perfil `https` es la configuración necesaria a usar para un `servidor web Kestrel`, y el perfil `IIS Express` es para un servidor `IIS`.

Podemos configurar diferentes perfiles con diferentes configuraciones basándonos en nuetras necesidades de desarrollo. Cuando nosotros ejecutamos nuestra aplicación .NET Core en Visual Studio o usando .NET CLI, podemos seleccionar uno de los perfiles disponibles para especificar como nuestra aplicación debería de ser ejecutada y debugueada.

Hablemos del contenido de cada perfil:

- **CommandName**: El valor puede ser cualquiera de los siguientes: **IISExpress**, **IIS**, **Project**. Este valor determina el web server que se va a utilizar para hostear la aplicación y manejar las requests http.

- **LaunchBrowser**: Es un booleano que determina si el navegador por defecto se debería de abrir cuando la aplicación inicia. Esto significa que si el valor es `true` el navegador por defecto abrirá una nueva ventana con la url raíz.

- **LaunchUrl**: En caso de que ```LaunchBrowser``` se encuentre activado, se abrirá en el recurso de la API que se indique en el ```LaunchUrl```.

- **DotnetRunMessages**: Su funcion principal es habilitar o deshabilitar el despliegue de ciertos mensajes cuando la aplicación es ejecutada usando .NET CLI. El valor de esta variable es booleano.

- **ApplicationUrl**: Especifica la url base que uno puede usar para acceder a la aplicación. Si HTTPS fue habilitado al momento de crear el proyecto, se obtendrán dos urls, una usando el protocolo HTTP y otra usando el protocolo HTTPS. Entonces esta variable especifica en que URLs la aplicación va a estar escuchando requests HTTP cuando este en ejecución. Esto es útil para probar la aplicación en diferentes puertos o host names durante desarrollo.

- **sslPort**: Esta variable especifica el puerto HTTPS para acceder en el caso de usar un servidor IIS Express. El valor 0 significa que uno no puede acceder a la aplicación usando el protocolo HTTPS.

- **WindowsAuthentication**: Aca se especificará si la autenticación por windows está habilitada para la aplicación o no. Es un valor booleano.

- **AnonymousAuthentication**: Podremos especificar si la autenticación anónima está habilitada para la aplicación. Es un valor booleano.

La sección `iisSettings` es la configuración a usar por un servidor `IISExpress`.

## Cuando usar este archivo

Este archivo es usado principalmente en un ambiente de desarrollo para configurar como la aplicación debería de ser ejecutada y setear variables de ambiente iniciales. Acá se pueden encontrar algunos escenarios y objetivos para usar este archivo:

- **Definición de variables de ambiente**: Es común usar diferentes configuraciones según el ambiente en el cual se este corriendo la aplicación, desarrollo, staging, producción, qa, etc. Este archivo permite la definición de ciertas variables de ambiente como `ASPNETCORE_ENVIRONMENT`, la cual puede ser tomar los valores `Development`, `Staging` o `Production`. Esto ayuda a la hora de probar como la aplicación se comporta bajo diferentes configuraciones sin la necesidad de cambiar código.

- **Configurar varios perfiles**: Permite la configuración de varios perfiles de ejecución para diferentes escenarios. La flexibilidad permite cambiar los contextos y probar la aplicación en diferentes ambientes rápidamente.

- **Customizacion de la url**: Para propósitos de desarrollo, uno podría querer correr la aplicación en un puerto específico o con cierto hostname.

## Limpieza del archivo

Si solo se requiere un solo perfil y el servidor `Kestrel` el archivo quedaría:

```JSON
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
