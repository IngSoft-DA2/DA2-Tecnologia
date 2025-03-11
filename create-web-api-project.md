# Creacion de proyecto Web API
A la solucion creada en el paso [Creacion de una solucion](https://github.com/IngSoft-DA2/DA2-Tecnologia/blob/main/solution-creation.md) utilizada en la [Creacion de proyecto MSTest](https://github.com/IngSoft-DA2/DA2-Tecnologia/blob/unit-testing/create-unit-test-project.md), le vamos agregar un proyecto `Web API` que sera nuestro punto de entrada para consumir nuestra aplicacion.

- Abrir una terminal en el directorio de la solucion. Para saber si estamos bien situados ejecutar `ls` y se deberia de ver el archivo `.sln`

```
ls
```

Comandos:

- `ls`: lista elementos en un directorio

<p align="center">
  <img src="images/image-20.png"/>
</p>

<p align="center">
[Terminal en directorio]
</p>

- Para crear el proyecto `Web API` nos situaremos en la carpeta `src` con la terminal ejectuando:

```
cd src
```

Comandos:

- `cd`: movimiento a un directorio en particular


- Una vez parados en la carpeta `src` con la terminal, crearemos un proyecto `Web API` ejecutando lo siguiente

```
  dotnet new webapi -au none --no-openapi -controllers -n <<Nombre del negocio>>.WebApi
```

Comandos y parametros:

- `new`: crea un nuevo proyecto
- `webapi`: tipo de proyecto a crear
- `-n`: nombre del proyecto
- `<<Nombre del negocio>>.WebApi`: `<<Nombre del negocio>>` es el contexto del negocio, `WebApi` es un nombre que me ayuda identificar como sera expuesta mi aplicacion
- `au`: sirve para configurar el tipo de autenticacion que queremos que se requiera, el valor `none` es para indicar que no queremos que se configure.
- `no-openapi`: para indicar que no se quiere integrar `Swagger` en la web api.
- `controllers`: para indicar que se usen controllers y no una `minimal-api`

<p align="center">
<img src='./images/image-21.png'>
</p>

<p align="center">
[Creacion proyecto Web API]
</p>

- Chequear que se creo el proyecto. En el directorio ejecutar `ls`.

```C#
ls
```

<p align="center">
<img src='./images/image-22.png'>
</p>
<p align="center">
[Chequear creacion de proyecto]
</p>

- Ahora dicho proyecto `Web API` lo debemos de agregar a la solucion. Para esto debemos situarnos con la terminal en el directorio donde esta la solucion `.sln`, para esto ejecutaremos:

```
cd ..
```

Comandos:

- `cd ..`: nos posiciona la terminal en un directorio para atras

Una vez que estemos bien situados, para agregar el proyecto a la solucion `.sln` debemos ejecutar:

```C#
dotnet sln add src/Vidly.WebApi
```

Comandos y parametros

- `sln`: operar con solucion
- `add`: agregar proyecto a la solucion
- `src/Vidly.WebApi`: nombre del proyecto a agregar a la solucion

<p align="center">
<img src='./images/image-23.png'>
</p>

<p align="center">
[Agregar proyecto a solucion]
</p>

- Chequear que se agrego el proyecto a la solucion

```C#
dotnet sln list
```

Comandos:

- `sln`: operar con solucion
- `list`: listar proyectos en solucion

<p align="center">
<img src='./images/image-24.png'>
</p>

<p align="center">
[Chequear que se agrego a la solucion]
</p>

- Como este proyecto sera el que exponga nuestra aplicacion por la `web`, este tiene que referenciar a dicho proyecto
```
cd src
cd Vidly.WebApi
dotnet add reference ../Vidly.BusinessLogic/Vidly.BusinessLogic.csproj
```
<p align="center">
<img src='./images/image-25.png'>
</p>

<p align="center">
[Chequear que se agrego la referencia de BusinessLogic en WebApi]
</p>
