# Inyección de dependencia

Gracias al framework .NET Core, podremos utilizar el patrón inyección de dependencia gracias a la libreria: `Microsoft.Extensions.DependencyInjection` de forma muy simple. Dicho patrón apunta a la gestión y control de las dependencias del sistema. Una de las ganancias de aplicar dicho patrón es poder implementar el principio de **inversión de dependencia** de forma clara y sencilla.

## Dependencia

Una dependencia entre clases o paquetes se da cuando una clase o paquete necesita un elemento (funcionalidad, clase, tipo, etc) en particular para poder realizar una operación específica en casos en los que este elemento se encuentra por fuera de la clase o paquete.

Conceptualmente, la dependencia se trata como `uso`, es decir, cuando existe una dependencia entre clases, es porque una clase `usa` a la otra, lo mismo se puede aplicar a paquetes. Este tipo de relación la podemos ver entre clases, paquetes e incluso entre elementos `cliente` y `servidor`, los cuales son más tangibles y menos lógicos.

## Problema que el patrón intenta resolver

Sin el uso de la inyección de dependencia, el desarrollador deberá de instanciar, de forma manual, las dependencias necesarias directamente en las clases o métodos que las necesiten. Este approach puede ser útil en etapas inicilaes en un sistema, pero a la larga, genera un fuerte acoplamiento entre componentes, haciendo el código rígido y difícil de mantener o modificar. Algunas desventajas de instanciar las dependencias manualmente son:

- Poca flexibilidad: Cuando una clase instancia directamente sus dependencias, se vuelve difícil cambiarlas o remplazarlas por otras. Por ejemplo, si una clase depende de una implementación específica sobre el acceso a datos de una base de datos, para poder cambiarla por otra implementacion sobre otra base de datos, se requerirá cambiar la clase en si. Esta falta de flexibilidad, se debe a que instanciar elementos concretos dentro de una clase aumenta el acoplamiento y disminuye la cohesión, no es responsabilidad de la clase crear ese elemento ni debería de conocer la implementación concreta.

- Difícil de probar de forma unitaria: un fuerte acomplamiento a implementaciones hace que realizar pruebas unitarias sea muy difícil o imposible. Cuando una clase instancia directamente sus dependencias, se vuelve desafiante aislar dicha clase para probarla. La realización de pruebas es crucial para mantener la calidad de código y asgurar que cambios emergentes no introduzcan comportamientos inesperados.

- Difícil de escalar: A medida que la aplicación crece, manejar las dependencias se vuelve una tarea cada vez mas compleja. La intanciación de dichas dependencias de forma manual en diferentes lugares puede llevar a introducir bugs y a un decaimiento de la productividad.

Ejemplo del problema con código:
Dado dos clases, `MovieController` y `MovieLogic`. `MovieController` es el controller específico para gestionar el recurso `movies` y exponer operaciones acorde a dicho recurso. La clase `MovieLogic` es la clase específica que encapsula toda la lógica de negocio sobre el recurso `Movie`, esta expondrá de forma pública ciertas operaciones para ser usadas por otras clases.

La dependencia se da desde `MovieController` hacia `MovieLogic`, como se puede ver en el segmento de código siguiente:

```C#
[ApiController]
[Route("movies")]
public sealed class MovieController : ControllerBase
{
  private readonly MovieLogic _movieLogic;

  public MovieController()
  {
    _movieLogic = new MovieLogic();
  }

  [HttpGet]
  public List<Movie> GetAll()
  {
    return _movieLogic.GetAll();
  }

  // ... rest of the code
}
```

Podemos observar como `MovieController` necesita de una instancia de `MovieLogic` para realizar ciertas operaciones, es por eso que `MovieLogic` es una dependencia de `MovieController`.

```C#
public sealed class MovieLogic
{
  private readonly IMovieRepository _movieRepository;
  private readonly IUserRepository _userRepository;

  public MovieLogic()
    {
      var dbContext = new DbContext();
      _movieRepository = new MovieRepository(dbContext);
      _userRepository_ = new UserRepository(dbContext);
    }

  // behaviour
}
```

Configurar esta dependencia es muy simple porque `MovieLogic` nos lo permite, pero esto se podría complicar fácilmente si `MovieLogic` estuviese definido de la siguiente manera:

```C#
public sealed class MovieLogic
{
  // ...

  public MovieLogic(DbContext context)
    {
      // ...
    }

  // ...
}
```

Haciendo que `MovieController` tuviese que definir sus dependencias y las dependencias de las dependencias.

```C#
[ApiController]
[Route("movies")]
public sealed class MovieController : ControllerBase
{
  private readonly MovieLogic _movieLogic;

  public MovieController()
  {
    // declar MovieLogic dependencies
    _movieLogic = new MovieLogic(/* set MovieLogic dependencies*/);
  }

  [HttpGet]
  public List<Movie> GetAll()
  {
    return _movieLogic.GetAll();
  }

  // ... rest of the code
}
```

Con este sencillo ejemplo logramos ver lo fácil que es comprometer y complejizar el mantenimiento y la calidad de código.

## ¿Cómo el patrón resuelve el problema?

El patrón resuelve estas desventajas al desacoplar los elementos entre ellos y gestionar por ellos las dependencias que utilizarán. Gracias a esto obtenemos las siguientes ventajas.

- Poco acoplamiento: DI promueve que haya poco acoplamiento entre componentes al remover las instanciaciones de las dependencias de forma directa/manual. Las clases solo pueden utilizar abstracciones (interfaces o clases abstractas), permitiendoles una fácil modificación y extensión de las mismas.

- Realizacion de pruebas: Con DI, la realización de pruebas unitarias es sumamente straightforward el poder replazar las dependencias con mocks o implementaciones dummy para las pruebas. Esta aislación permite una mayor efectividad a la hora de realizar pruebas unitarias, llevando a tener una mejor calidad de código y menos bugs.

- Flexibilidad: DI facilita la modularidad y la extensibilidad. Es fácil intercambiar dependencias o introducir nuevas sin la necesidad de modificar código existente, lo cual promueve a reutilizar código y hacer un sistema más adaptable a cambios de requerimientos.

- Configuración de dependencias centralizada: DI promueve centralizar la configuración de las dependencias. El lugar indicado de realizar esto es al momento de iniciar la aplicación. Esto permite tener una consistencia sobre las dependencias en toda la aplicación.

- Cumple con OCP y SRP

Siguiendo el ejemplo de código, la aplicación de DI dejaría el código de la siguiente manera:

```C#
[ApiController]
[Route("movies")]
public sealed class MovieController(IMovieLogic movieLogic)
 : ControllerBase
{
  [HttpGet]
  public List<Movie> GetAll()
  {
    return movieLogic.GetAll();
  }

  // ... rest of the code
}
```

```C#
public sealed class MovieLogic(
IMovieRepository movieRepository,
IUserRepository userRepository)
{
  // behaviour
}
```

Nuestro código se vió impactado en no instanciar las dependencias sino que en declarar que se necesita para que la clase funcione correctamente. Quien se encargue de instanciar estas dependencias en el momento adecuado será el framework siguiendo la configuración que nosotros especifiquemos.

Para terminar de configurar el uso de DI en nuestro sistema, debemos de configurar las dependencias en el inicio de nuestra aplicaión. En un proyecto web-api usando .NET 8 es en la clase `Program.cs`. Dicha clase inicialmente se encuentra de la siguiente manera:

```C#
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();
// Configure the HTTP request pipeline.

app.MapControllers();

app.Run();
```

Para realizar la configuración de las dependencias, las mismas se deberán de encontrar después de la primera línea `var builder = WebApplication.CreateBuilder(args);` y antes de la compulación de la app `var app = builder.Build();`.

### Registro de servicios (dependencias)

Los serivcios involucrados en el registro son aquellos que son dependencias de otro servicio. Dicho registro ocurre para un contenedor de servicios al cual podemos seleccionar el ciclo de vida que le queremos dar y este contenedor gestionará la vida de los mismos.
Existen 3 ciclos de vida:

#### Scope

Estos servicios serán instanciados una vez dentro de un contexto de uso.

Bajo el dominio de una web api, los servicios `scope` serán instanciados por cada request, y dicha instancia será compartida entre los lugares que se requiera dicha dependencia. Esto quiere decir que, si dos servicios (A y B) distintos, dependen del mismo servicio (C) y esta es declarada como `scope`, A y B comparten la misma instancia de C (siempre y cuando A y B se utilicen en la misma request).

Esto implica que la instancia del servicio C es reusada para todos los servicios que la necesiten. Este ciclo de vida asegura consistencia y evita instancias duplicadas innecesariamente. Esta instancia es disposed por el contenedor de DI cuando la request termino de ejecutarse.

Para registrar un servicio con este ciclo de vida se deberá de usar el metodo `AddScoped`

#### Transient

Estos servicios serán instanciadas para cada servicio que lo requiera.

Esto quiere decir que si tenemos los servicios A y B que dependen de C, y C esta declarado con este ciclo de vida, la instancia pasada al servicio A es difrente a la instancia pasada al servicio B, esto implica que la instancia de C no es reusable.

El largo de la vida de estos servicios es menor al largo de vida de los servicios `scope`;

#### Singleton

Solo existirá una única instancia de estos servicios y la misma será compartida y distribuida para aquellos servicios que la necesiten.

Estos servicios se instanciarán en la primera petición y luego se reusará la instancia para peticiones futuras.

La duración de vida de estos servicios es acorde a la vida del sistema.

Las dependencias entre los diferentes servicios debe ser en sentido gradual con respecto al largo de vida de los mismos. Esta es una restricción para no utilizar servicios que el framework le hizo un dispose.

El orden de vida es el siguiente: `Singleton > Scope > Transient`, traduciendose a: los servicios `Singleton` son los que perduran más en el tiempo seguido por los servicios `Scope` y luego los `Transient`. Esto hace que el sentido de depdendencias sea de forma inversa, quedando: `Transient -> Scope -> Singleton` y traduciendose a que servicios Transient pueden depender de otros servicios `Transient`, `Scope` o `Singleton`, servicios `Scope` pueden depender de servicios `Scope` o `Singleton` y servicios `Singleton` solo a servicios `Singleton`.

El siguiente código muestra como configurar servicios con el ciclo de vida `Scope`

```C#
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// services -> es el contenedor de servicios
var services = builder.Services;

// Registro de logica de negocio
services
  .AddScoped<IMovieLogic, MovieLogic>();

// Registro de acceso a datos
services
  .AddScoped<DbContext, VidlyContext>()
  .AddScoped<IMovieRepository, MovieRepository>()
  .AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();
// Configure the HTTP request pipeline.

app.MapControllers();

app.Run();
```

Dado esta configuración, el framework sabrá como tratar e instanciar nuestros servicios para cuando llegue una request. El contenedor de servicios autogestiona las dependencias sin tener que involucrarse manualmente.

## Referencias

[DI - Dependency injection in .NET Core](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-8.0#entity-framework-contexts)
