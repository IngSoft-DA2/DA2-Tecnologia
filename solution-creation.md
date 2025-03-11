# Creacion de una solucion

- Para crear una solucion debemos situarnos con la terminal en el directorio donde queremos crear dicho archivo `.sln`. Dicho directorio debera de ser el del repositorio clonado.
- Una vez situados en dicho directorio con la terminal se debera de ejecutar el siguiente comando:

```
dotnet new sln -n <<Nombre del negocio, archivo de la solucion>>
```
Comandos y parametros:
- `dotnet`: terminal CLI para realizar acciones de .NET
- `new`: parametro para indicar la creacion de algo
- `sln`: se desea crear un archivo `.sln`
- `-n`: se indica que el archivo `.sln` debe tomar, en caso de no indicar toma el nombre del directorio en donde es creado
  
- Una vez creada la solucion crearemos dos directorios: `src` y `tests`. El directorio `src` servira para agrupar nuestro codigo fuente y el directorio `tests` servira para agrupar los proyectos de prueba. De esta forma tendremos una mejor organizacion en la solucion y en el repositorio. Para crear dichos directorios se puede hacer de manera tradicional (click derecho -> Nueva carpeta) o ejecutar los siguientes comandos independientemente:

```
mkdir src
mkdir tests
```
