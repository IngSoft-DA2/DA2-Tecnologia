# AppSettings.json

Es un archivo JSON que contiene configuraciones de la aplicación. En este archivo se pueden guardar configuraciones como connection strings, configuraciones de la aplicación mismo, logging, y cualquier otra cosa que uno quiera cambiar sin la necesidad de recompilar la aplicacion.

Las configuraciones en este archivo pueden ser leídas en tiempo de ejecución y sobrescritas por las configuraciones específicas del ambiente en el cual se corre. En un ambiente de desarrollo se usan los valores de `appsettings.Development.json` y en un ambiente de producción se usarían los valores de `appsettings.Production.json`.

`Appsettings.json` define las variables que se deben configurar y los valores se deben poner en extensiones según el ambiente de este archivo.

Es importante no guardar información sensible en estos archivos, como contraseñas o secret keys. Para esta información se deberá usar una forma segura de alojamiento como variables de ambiente.

Así como los `appsettings.{environment}.json` sobrescriben los valores encontrados en `appsettings.json`, hay otros fuentes de configuración que pueden sobrescribir los valores en `appsettings.{environment}.json`. El orden de carga de estas fuentes de configuración es el siguiente:

- appsettings.json
- appsettings.{environment}.json
- User secrets
- Environment variables

Esto quiere decir que si todas las fuentes configuran las mismas variables, el único valor que se tendrá será el de `environment variables`.

Este orden y las fuentes de configuración pueden ser modificadas en caso de que se requiera.

En este archivo podemos encontrar lo siguiente:

```C#
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

- **Logging**: Define el nivel de logueo para diferentes componentes de la aplicación

- **AllowedHosts**: Especifica los hosts que la aplicación va a estar escuchando

## Buenas prácticas

- **Evitar credenciales hard-coded**: No se debería guardar información sensible directamente en el código ni en estos archivos si no son ignorados.

- **Usar configuraciones específicas de ambiente**: Se debe hacer uso de `appsettings.{environment}.json` para sobrescribir y especificar valores acorde al ambiente.

- **Control de versión**: Si `appsettings.json` es incluido en el control de versión del código, respetar la primera práctica.
