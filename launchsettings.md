# LaunchSettings.json

Es un archivo de configuracion usado para configurar varios aspectos sobre como nuestra aplicacion deberia ser ejecutada y debugeada durante el desarrollo. Este archivo se puede encontrar en la carpeta `Properties` y es usado principalmente por Visual Studio y .NET CLI.

Las configuraciones que este archivo contiene, seran usadas cuando corramos nuestra web api desde Visual Studio o por comandos usando .NET CLI. El punto mas importante es que este archivo solo es usado de forma local en el desarrollo. Este archivo no es requerido cuando nosotros despleguemos la aplicacion en un servidor de produccion.

Cualquier configuracion que se ponga en este archivo que se quiera utilizar en cualquier otro ambiente que no sea local, debera de ser movida para el archivo `appsettings.json`.

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

El archivo contiene diferentes perfiles para ejecutar la aplicacion. Cada perfil puede especificar diferentes configuraciones como la URL a usar, variables de ambiente, etc. Estos perfiles pueden ayudar a setear diferentes ambientes para el desarrollo, como un ambiente de prueba para probar la aplicacion con diferentes configuraciones sin cambiar el codigo.

En este `launchsettings.json` podemos encontrar dos secciones, `IIS Express` y `https`. El perfil `https` es la configuracion necesaria a usar para un `servidor web Kestrel`, y el perfil `IIS Express` es para un servidor `IIS`.

Podemos configurar diferentes perfiles con diferentes configuraciones basandonos en nuetras necesidades de desarrollo. Cuando nosotros ejecutamos nuestra aplicacion .NET Core en Visual Studio o usando .NET CLI, podemos seleccionar uno de los perfiles disponibles para especificar como nuestra aplicacion deberia de ser ejecutada y debugueada.

Hablemos del contenido de cada perfil:

- **CommandName**: El valor puede ser cualquiera de los siguientes: **IISExpress**, **IIS**, **Project**. Este valor determina el web server que se va a utilizar para hostear la aplicacion y manejar las requests http.

- **LaunchBrowser**: Es un booleano que determina si el navegador por defecto se deberia de abrir cuando la aplicacion inicia. Esto significa que si el valor es `true` el navegador por defecto abrira una nueva ventana con la url raiz.

- **DotnetRunMessages**: Su funcion principal es habilitar o deshabilitar el despliegue de ciertos mensajes cuando la aplicacion es ejecutada usando .NET CLI. El valor de esta variable es booleano.

- **ApplicationUrl**: Especifica la url base que uno puede usar para acceder a la aplicacion. Si HTTPS fue habilitado al momento de crear el proyecto, se obtendran dos urls, una usando el protocolo HTTP y otra usando el protocolo HTTPS. Entonces esta variable especifica en que URLs la aplicacion va a estar escuchando requests HTTP cuando este en ejecucion. Esto es util para probar la aplicacion en diferentes puertos o host names durante desarrollo.

- **sslPort**: Esta variable especifica el puerto HTTPS para acceder en el caso de usar un servidor IIS Express. El valor 0 significa que uno no puede acceder a la aplicacion usando el protocolo HTTPS.

- **WindowsAuthentication**: Aca se especificara si la autenticacion por windows esta habilitada para la aplicacion o no. Es un valor booleano.

- **AnonymousAuthentication**: Podremos especificar si la autenticacion anonima esta habilitada para la aplicacion. Es un valor booleano.

La seccion `iisSettings` es la configuracion a usar por un servidor `IISExpress`.

## Cuando usar este archivo

Este archivo es usado principalmente en un ambiente de desarrollo para configurar como la aplicacion deberia de ser ejecutada y setear variables de ambiente iniciales. Aca se pueden encontrar algunos escenarios y objetivos para usar este archivo:

- **Definicion de variables de ambiente**: Es comun usar diferentes configuraciones segun el ambiente en el cual se este corriendo la aplicacion, desarrollo, staging, produccion, qa, etc. Este archivo permite la definicion de ciertas variables de ambiente como `ASPNETCORE_ENVIRONMENT`, la cual puede ser tomar los valores `Development`, `Staging` o `Production`. Esto ayuda a la hora de probar como la aplicacion se comporta bajo diferentes configuraciones sin la necesidad de cambiar codigo.

- **Configurar varios perfiles**: Permite la configuracion de varios perfiles de ejecucion para diferentes escenarios. La flexibilidad permite cambiar los contextos y probar la aplicacion en diferentes ambientes rapidamente.

- **Customizacion de la url**: Para propositos de desarrollo, uno podria querer correr la aplicacion en un puerto especifico o con cierto hostname.

## Limpieza del archivo

Si solo se requiere un solo perfil y el servidor `Kestrel` el archivo quedaria:

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
