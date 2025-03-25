# Creación de proyecto de prueba unitaria por comandos

- Abrir terminal en el directorio de la solución. Para saber si estamos bien situados ejecutar `ls` y se debería de ver el archivo `.sln`

```C#
ls
```

Comandos:

- `ls`: lista elementos en un directorio
<p align="center">
<img src='./images/image-16.png'>
</p>

<p align="center">
[Terminal en directorio]
</p>

- Entrar al directorio del proyecto de prueba. Vidly.BusinessLogic.Test

```C#
cd Vidly.BusinessLogic.Test
```

Comandos y parámetros:

- `cd`: entrar a un directorio

<p align="center">
<img src='./images/image-24.png'>
</p>

<p align="center">
[Entrar a directorio de prueba]
</p>

- Agregar referencia a `Moq`

```C#
dotnet add package Moq
```

Comandos

- `add`: operación de agregar
- `package`: paquete de `nuget.org`
- `Moq`: nombre de paquete

<p align="center">
<img src='./images/image-25.png'>
</p>

<p align="center">
[Agregar Moq]
</p>

- Chequear que se agrego `Moq`. Hacer doble click en `Vidly.BusinessLogic.Test.csproj`
<p align="center">
<img src='./images/image-28.png'>
</p>

<p align="center">
[Chequear Moq]
</p>
</p>

- Agregar referencia a `FluentAssertions`

```C#
dotnet add package FluentAssertinos
```

Comandos

- `add`: operación de agregar
- `package`: paquete de `nuget.org`
- `FluentAssertions`: nombre de paquete

<p align="center">
<img src='./images/image-27.png'>
</p>

<p align="center">
[Agregar FluentAssertions]
</p>

- Chequear que se agrego `FluentAssertions`. Hacer doble click en `Vidly.BusinessLogic.Test.csproj`
<p align="center">
<img src='./images/image-26.png'>
</p>

<p align="center">
[Chequear FluentAssertions]
</p>
</p>
