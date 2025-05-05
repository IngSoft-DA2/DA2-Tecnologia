# Filtro de excepciones

Este filtro define una forma de manejar las excepciones no catcheadas en el sistema de forma global. Puede ser utilizado para manejar políticas custom sobre las excepciones.

Para implementar un filtro de excepciones, se debe implementar la interfaz `IExceptionFilter`.

El siguiente código muestra como implementar este filtro con la interfaz `IExceptionFilter`:

```C#
public sealed class CustomExceptionFilterAttribute : Attribute, IExceptionFiler
{
  public void OnException(ExceptionContext context)
  {
    //some code to handle exception
  }
}
```

El siguiente código muestra como usar el filtro de excepcion a nivel de clase:

```C#
[ApiController]
[Route("endpoints")]
[CustomExceptionFilter]
public sealed class CustomController : ControllerBase
{
  [HttpGet]
  public void Action()
  {
    //some code
  }
}
```

Este filtro de excepciones custom, manejará todas las excepciones no manejadas que ocurran dentro de esta clase.

Una vez que llega la excepción al filtro, este deberá de setear a la property `ExceptionHandled` en `true`, o asignar un valor a la property `Result` para responder el error al cliente. Este filtro no puede convertir la respuesta de error en una de éxito, solo el filtro de `Action` puede hacer eso.

Estos filtros son buenos como último recurso para aquellas excepciones no manejadas dentro del sistema pero tienen la desventaja que no son tan flexibles como un middleware para manejar los errores.

## Caminos de implementación

### Camino 1

Se puede tener tantos exception filter como uno quiera, eso da a lugar que se podría tener un exception filter por cada controller donde cada exception filter solo controla las excepciones que se lancen dentro de los métodos de ese controller.

Eso quiere decir que si tenemos los controllers: `Controller1` y el `Controller2`, podríamos crear un filtro de excepción independiente para cada controller.

Teniendo lo siguiente:

```C#
public sealed class Controller1ExceptionFilterAttribute : Attribute, IExceptionFilter
{
  // code to handle unhandle exceptions of Controller1
}
```

<p align="center">
[Filtro de excepción para `Controller1`]
</p>

```C#
public sealed class Controller2ExceptionFilterAttribute : Attribute, IExceptionFilter
{
  // code to handle unhandle exceptions of Controller2
}
```

<p align="center">
[Filtro de excepción para `Controller2`]
</p>

Los cuales se usarían de la siguiente manera:

```C#
[ApiController]
[Route("controller1")]
[Controller1ExceptionFilter]
public sealed class Controller1Controller : ControllerBase
{
  // code to handle unhandle exceptions of Controller1
}
```

<p align="center">
[Controller1 con su filtro de excepciones]
</p>

```C#
[ApiController]
[Route("controller2")]
[Controller2ExceptionFilter]
public sealed class Controller2Controller : ControllerBase
{
  // code to handle unhandle exceptions of Controller1
}
```

<p align="center">
[Controller2 con su filtro de excepciones]
</p>

Esto llevaría a tener varios filtros de excepción con un tamaño adecuado para mantener.

Este camino no tiene en cuenta aquellas excepciones que no son especificas del controller, las cuales deben ser manejadas de forma global.

### Camino 2

Tener un solo filtro de excepciones el cual tiene el control de cualquier excepción no manejada y este debe ser declarado de forma global.

La implementación del filtro no varía, lo que varía es la forma de utilizarlo, ya que no se especificará en ninguna clase o método.

Para registrar un filtro de excepciones de forma global, es necesario modificar la clase `Program.cs`

```C#
builder.Services.AddControllers(options =>
{
    options.Filters.Add<CustomExceptionFilter>();
});
```

Este camino tiene la ventaja de no tener que crear varios filtros de excepciones pequeños y evita el setearlo en cada clase. Tiene la desventaja de que impacta en el mantenimiento del mismo ya que controla todas las excepciones del sistema.

## Material de lectura

[Excepcion](https://learn.microsoft.com/en-us/aspnet/core/mvc/controllers/filters?view=aspnetcore-8.0#exception-filters)

