# Filtros

Los filtros en ASP.NET Core nos permiten ejecutar código antes de que se procese una request o después de que la misma ya fue procesada.

Los filtros son ejecutados por ASP.NET Core dentro de la lista de tareas que deben ser invocadas al momento de procesar una request.

<p align="center">
<img src="./images/image.png"/>
</p>

<p align="center">
[Lista de tareas por las cuales pasa una request]
</p>

En la imagen podemos ver 3 tareas predefinidas por el framework por las cuales pasa la request.

- Other Middleware -> es un custom middleware que el desarrollador crea para ser ejecutado.
- Routing Middleware -> es el middleware que enruta la request al controller
- Action Selection -> es el middleware que enruta la request al método del controller
- Filter Pipeline -> es el espacio donde los filtros serán ejecutados

Existen diferentes tipos de filtros los cuales son ejecutados en etapas diferentes dentro de la línea de procesamiento de una request.

Los tipos de filtros son:

- Authorization

  - Son los primeros en ser ejecutados.
  - Determinan si el usuario está autorizado o no.

- Resource

  - Son ejecutados después de los Authorization.
  - El método `OnResourceExecuting` es ejecutado antes de ejecutar el resto de los filtros.
  - El método `OnResourceExecuted` es ejecutado después que el resto de los filtros se ejecutaron de forma exitosa.

- Action:

  - Son ejecutados de forma inmediata antes y después de invocar al método del controller.
  - Pueden cambiar los argumentos que son pasados al método.
  - Pueden cambiar el resultado del método.

- Endpoint:

  - Son ejecutados de forma inmediata antes y después de invocar al método del controller.
  - Pueden cambiar los argumentos que son pasados al método.
  - Pueden cambiar el resultado del método.
  - Pueden ser invocados en endpoints de acciones y de clase.

- Exception: aplican politicas globales para manejar excepciones que ocurren antes de brindar una respuesta al cliente.

- Result:

  - Son ejecutados de forma inmediata antes y después de brindar una respuesta.
  - Son ejecutados solamente cuando el método se proceso de forma exitosa.
  - Son útiles para lógica relacionada al formateo.

  La siguiente imagen ilustra como los diferentes filtros se comportan entre ellos en una aplicación ASP.NET Core.

  <p align="center">
  <img src="./images/image-1.png"/>
  </p>

  <p align="center">
  [Interacción entre filtros]
  </p>

## Definición y orden de ejecución

Los filtros pueden ser agregados a la línea de procesamiento de una forma de las siguientes tres posibles:

- A nivel de controller
- A nivel de método
- Global (para todos los controllers y métodos)

Cuando existen multiples filtros definidos para etapas particulares en la línea de procesamiento, el alcance que abarca dicho filtro determina el orden de la ejecución de los mismos.

A continuación se muestra como sería la ejecución en orden de los filtros

```
El código de antes en un filtro global
  El código de antes de un filtro de controller
    El código de antes de un filtro de método
      Código a invocar
    El código de después de un filtro de método
  El código de después de un filtro de controller
El código de después de un filtro global
```

El siguiente ejemplo ilustra el orden en el cual son ejecutados los métodos de los diferentes filtros

| Secuencia | Alcance del filtro | método del filtro |
| --------- | ------------------ | ----------------- |
| 1         | Global             | OnActionExecuting |
| 2         | Controller         | OnActionExecuting |
| 3         | Método             | OnActionExecuting |
| 4         | Código             |         -         |
| 5         | Método             | OnActionExecuted  |
| 6         | Controller         | OnActionExecuted  |
| 7         | Global             | OnActionExecuted  |

## Sobrescribir el orden de ejecución

El orden de ejecución de los filtros puede ser sobreescrito al implementar la interfaz `IOrderedFilter`. Esta interfaz expone una property `Order` que es tomada con mayor prioridad al alcance del filtro que determina el orden de ejecución. Un filtro con valor menor de `Order`:

- Se ejecutará el código de antes, antes que el del filtro con mayor valor de `Order`
- Se ejecutará el código de después, después que el del filtro con mayor valor de `Order`.

Por ejemplo:

```C#
[SampleActionFilter(Order = int.MinValue)]
public class ControllerFiltersController : Controller
{
    // ...
}
```

El filtro `SampleActionFilter` es un filtro a nivel de controller y tiene el menor valor de `Order` posible

```C#
builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalSampleActionFilter>(int.MaxValue);
});
```

El siguiente filtro `GlobalSampleActionFilter` es un filtro de nivel global y tiene el máximo valor de `Order` posible.

Esto hace que el filtro `SampleActionFilter` sea ejecutado antes que el `GlobalSampleActionFilter`, cuando el comportamiento normal, sin sobrescribir el `Order`, sería ejecutar primero `GlobalSampleActionFilter` y luego `SampleActionFilter`.

## Cancelar ejecución de un filtro

Los filtros pueden acortar la línea de procesamiento de la request en cualquier momento. Para hacer esto los mismos tendrán que setearle un valor a la property `Result` en el parámetro `context` que reciben.

## Filtros con dependencias

Estos filtros pueden ser agregados por tipo, por instancia o como atributos. Si son agregados por instancia, esa instancia es usada para cada request. Si son agregados por tipo significa que:

- Una instancia del filtro es creada por cada request

- Las dependencias del filtro serán populadas con inyección de dependencia desde el contenedor de servicios.

Los filtros implementados como atributos y agregados directamente a nivel de controller o métodos, no pueden tener constructores con dependencias para ser inyectados desde el contenedor de servicios. Las dependencias que están pensadas ser provistas por el contenedor de servicios no será posible porque los atributos tienen que tener constructores sin parámetros o los parámetros ser provistos al momento de aplicar el atributo.

En caso de que el filtro necesite un constructor con DI, se podrá utilizar los siguientes filtros para llevar a cabo dicha restricción:

- ServiceFilterAttribute
- TypeFilterAttribute
- IFilterFactory

## ServiceFilterAttribute

Los filtros de tipo de servicio son registrados en `Program.cs`. Este atributo obtiene una instancia del filtro desde el contenedor de servicios, DI.

El siguiente código muestra la definición del filtro custom:

```C#
public class CustomFilterService(IMyDependency dependency)
 : IResultFilter
{
    public void OnResultExecuting(ResultExecutingContext context)
    {
      // some code
      dependency.Action();
    }

    public void OnResultExecuted(ResultExecutedContext context)
    {
      dependency.Action();
    }
}
```

El siguiente código muestra el registro del filtro custom en `Program.cs`

```C#
builder.Services.AddSingleton<IMyDependency, MyDependency>();

builder.Services.AddScoped<CustomFilterService>();
```

El siguiente código muestra como usar el filtro custom con `ServiceFilterAttribute`

```C#
[ServiceFilter<CustomFilterService>]
public void MyActionOnController()
{
  // some code
}
```

Este atributo, `ServiceFilter`, no debe ser usado con filtros que dependan de servicios con ciclos de vida diferente a `Singleton`.

## TypeFilterAttribute

Funciona de forma similar a `ServiceFilterAttribute`, pero el tipo del filtro no es resuelto por el contenedor de servicios. Crea una instancia del tipo provisto al usar `Microsoft.Extensions.DependencyInjection.ObjectFactory`.

Como los tipos provistos no son resueltos por el contenedor de servicios:

- Los filtros provistos no necesitan ser registrados en el contenedor de servicios, DI.
- `TypeFilterAttribute` puede opcionalmente aceptar argumentos del filtro.

Este filtro tiene la misma restriccion que `ServiceFilter`, las dependencias del filtro que se le provee, no pueden tener otro ciclo de vida que no sea `Singleton`.

El siguiente código muestra la definición del filtro custom:

```C#
public class CustomFilterService(
  IMyDependency dependency,
  string parameter1,
  string parameter2)
 : IResultFilter
{
    public void OnResultExecuting(ResultExecutingContext context)
    {
      // some code
      dependency.Action();
    }

    public void OnResultExecuted(ResultExecutedContext context)
    {
      dependency.Action();
    }
}
```

El siguiente código muestra como usar dicho filtro con `TypeFilterAttribute`:

```C#
[TypeFilter(typeof(CustomFilter), Arguments = new object[]{'custom','arguments'})]
public void MyActionOnController()
{
  // some code
}
```

## Filtros como atributos

Los filtros custom que quieran ser utilizados como atributos, deberán de heredar de la clase `Attribute` para ser tratados como tal, independientemente del tipo que sean.

El constructor de dichos filtros deberá ser sin parámetros o con parámetros que se le puedan proveer un valor al momento de declararlos.

Tienen la desventaja de que no se le puedan inyectar dependencias desde el contenedor de servicios de forma automática.

El siguiente código muestra la definición de un filtro como atributo:

```C#
public sealed class CustomFilterAttribute : Attribute, // y también se deberá indicar el tipo de filtro, Authorization, Resource, Action, etc, implementando la interfaz correspondiente
{
  // some code
}
```

El siguiente código muestra como usar dicho filtro custom a nivel de clase y de método.

```C#
[ApiController]
[Route("endpoints")]
[CustomFilter]
public sealed class MyController : ControllerBase
{
  [HttpGet]
  [CustomFilter]
  public void MyAction()
  {
    // some code
  }
}
```

En caso de que el filtro custom necesite hacer uso de alguna dependencia registrada en el contenedor de servicios, este deberá obtener dicha dependencia de forma manual de la siguiente manera:

```C#
public sealed class CustomFilterAttribute : Attribute, //tipo de filtro
{
  public void FilterFunction(FilterContext context)
  {
    var dependency = context.HttpContext.RequestServices.GetRequiredService<IMyDependency>(); // en caso de que no exista un registro de dependencia para este servicio, una excepcion es lanzada
  }
}
```

Donde:

- `FilterFunction` es la función del tipo de filtro a implementar en `CustomFilterAttribute`

- `FilterContext` es el parámetro de la función del tipo de filtro usado

- `IMyDependency` es la dependencia registrada previamente en el contenedor de servicios a utilizar en el filtro

Acá no se tiene la restricción de que `IMyDependency` tenga que tener el ciclo de vida `Singleton`, puede ser de cualquier tipo.

## Material de lectura

[Filtros en ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/mvc/controllers/filters?view=aspnetcore-8.0)
