# Cheat sheet (Lista de comandos desde la consola)

Commando | Resultado
------------ | -------------
`dotnet new sln` | Creamos solucion (principalmente útil para VisualStudio, cuando queremos abrir la solución y levantar los proyectos asociados)
`dotnet new webapi -n "Nombre del Proyecto"` | Crear un nuevo Proyecto del template WebApi
`dotnet sln add` | Asociamos el proyecto creado al .sln
`dotnet sln list` | Vemos todos los proyectos asociados a la solución
`dotnet new classlib -n "Nombre del Proyecto"` | Crear un nueva libreria (standard)
`dotnet add "Nombre del Proyecto 1".csproj reference "Nombre del Proyecto 2".csproj` | Agrega una referencia al Proyecto 1 del Proyecto 2
`dotnet add package "Nombre del Package"` | Instala el Package al proyecto actual. Similar a cuando se agregaban paquetes de Nuget en .NET Framework.
`dotnet build` | Compilar y generar los archivos prontos para ser deplegados (_production build_)
`dotnet run` | Compilar y correr el proyecto
`dotnet ef migrations add "Nombre de la migracion"` | Compilar y crear la migracion para impactar en la base de datos
`dotnet ef database update` | Ejecutar las migraciones creadas

Siempre que se necesite ayuda con el comando o no se sabe cual usar, la mejor ayuda es correr `dotnet [COMANDO] -h` para una explicacion de los parametros del comando o cual es su funcionalidad. Tambien se puede correr `dotnet -h` para poder ver los comandos disponibles

### Es necesario crear una solucion? (SLN)

No, no es necesario. Sin embargo, tener una trae varias utilidades:
* Si usamos Visual Studio 2022, es necesario crearla para levantar todos los proyectos dentro del IDE
* Aunque no usemos VS2022, tener una solucion permite crear/compilar/manejar todos los proyectos involucrados juntos, sin tener que correr cada uno, por ejemplo. Se maneja todo como una unica unidad.

Para mas informacion, se puede leer [aquí](https://stackoverflow.com/questions/42730877/net-core-when-to-use-dotnet-new-sln)

## Creacion de proyecto Vidly.WebApi

A continuación crearemos un proyecto de ejemplo, sobre el cual seguiremos trabajando y seguiremos agregnadole funcionalidad.

### Creamos el sln para poder abrirlo en vs2022 y otras utilidades (opcional)

```
dotnet new sln
```

### Creamos el proyecto webapi y lo agregamos al sln
```
dotnet new webapi -au none -n Vidly.WebApi
dotnet sln add Vidly.WebApi
```

**X.csproj**

Este es el archivo de configuracion del proyecto. Aqui se definen varias cosas como: 

* Version del framework usado
* Dependencias a otros proyectos dentro de una solucion y el path a ellos
* Dependencias de paquetes externos de nuget. Cuando se toma el proyecto nuevo, se utiliza esta informacion para bajar los archivos necesarios de nuget packages. 

Este archivo vendria a tener una funcionalidad similar a la que tienen archivos similares en otros lenguajes/plataformas, como Javascript con el `package.json`

**/bin** 

Aca se encuentran todos los archivos compilados. Cada vez que se hace `dotnet run` o `dotnet build`, se compila el proyecto y se generan los `.dll` correspondientes. Lo mejor es ignorar de git esta carpeta

**/obj**

Son varios archivos que utiliza despues el compilador para compilar el proyecto. Son una especie de "archivos intermedios". Tambien conviene ignorar en git esta carpeta.






