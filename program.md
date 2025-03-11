# Program.cs

Es el punto de entrada de nuestra aplicacion, es donde vamos a configurar un host de tipo web application. Tambien se configurara y se registraran todos aquellos servicios requeridos para el funcionamiento de nuestra aplicacion en conjunto con porceso de middlewares y endpoints.

## Host

Un host es un envoltorio que cubre nuestra aplicacion. Es el responsable de iniciar y gestionar la vida de la aplicacion. El host contiene la configuracion de la aplicacion y un servidor HTTP (en nuestro caso un servidor - [Kestrel](https://github.com/daniel18acevedo/DA2-Tecnologia/blob/web-api/kestrel.md)) que esta atento a peticiones HTTP para atender y devolver una respuesta. Tambien es la que configura otros aspectos como logging, inyeccion de dependencia, procesamiento de las requests, etc.

Asi como configuramos una web application, tambien existen otros tipos de host:

- Web Application (o minimal host)
- Generic Host
- Web host

En esta clase podremos encontrar lo siguiente

<p align="center">
  <img src="images/image-12.png"/>
</p>

Lo primero que podemos notar es que no existe un metodo `main`. El metodo `main` en versiones anteriores o en otro tipo de aplicaciones como las aplicaciones de consola, es el punto de entrada del programa. Pero a partir de la version 9 de `C#` se introdujo los segmentos top-level, donde uno no tiene porque especificar un metodo `main` o un `namespace`. Esto nos permite tener una clase que solo contiene declaraciones.

La clase `program` crea la `web application` en tres pasos, **crea**, **configura** y **corre**.

<p align="center">
  <img src="images/image-13.png"/>
</p>

## CreateBuilder

La primera linea crea una instancia de tipo `WebApplicationBuilder`. El metodo `CreateBuilder` es un metodo estatico de la clase `WebApplication`, y el resultado es una configuracion por defecto de los siguientes elementos:

- Tipo de servidor HTTP que se va a usar (**[Kestrel](https://github.com/daniel18acevedo/DA2-Tecnologia/blob/web-api/kestrel.md)**)

- Logging

- Configuracion

- Contenedor de servicios

- Agrega algunos servicios del framework

El resultado de la instancia de tipo `WebApplicationBuilder` es un objeto donde podemos hacer configuraciones adicionales necesarias para nuestra aplicacion.

## Build

El metodo `Build` de la clase `WebApplicationBuilder` crea una instancia de tipo `WebApplication`. Utilizaremos este objeto para setear middleware's y endpoints. En las lineas siguientes podemos ver como se configuraron tres middleware's.

```C#
app.UseHttpsRedirection();
```

Es un middleware de .Net Core para redireccionar request HTTP a requests HTTPS, tambien realiza el mismo comportamiento para las respuestas. Esto quiere decir que si se expone dos puertos, uno en `http` y otro en `https`, todas las request destinadas al puerto en `http` seran forwardeadas al puerto de `https`.

```C#
app.UseAuthorization();
```

Es un middleware que autoriza a usuarios a acceder recursos protegidos al chequear permisos del usuario antes de realizar ciertas operaciones.

```C#
app.MapControllers();
```

Es un middleware que se de buscar los controllers en el proyecto y crear los endpoints apropiados segun los valores en los atributos de ruta en los mismos. Este middleware hace disponible los endpoints para luego consumirlos desde un cliente HTTP.

## Run

Este metodo inicia la aplicacion y escucha requests http en los puertos configurados para su ejecucion
