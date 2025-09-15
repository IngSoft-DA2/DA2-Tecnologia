# 💉 Inyección de Dependencias en .NET Core

¡Bienvenido! En este documento encontrarás una guía práctica y conceptual sobre el patrón **Inyección de Dependencias** (DI) usando .NET Core y la librería oficial [`Microsoft.Extensions.DependencyInjection`](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection).

---

## 📖 Índice

- [¿Qué es una dependencia?](#qué-es-una-dependencia)
- [Problemas que resuelve el patrón DI](#problemas-que-resuelve-el-patrón-di)
- [Ejemplo de código: Sin DI](#ejemplo-de-código-sin-di)
- [¿Cómo el patrón DI resuelve el problema?](#cómo-el-patrón-di-resuelve-el-problema)
- [Ejemplo de código: Con DI](#ejemplo-de-código-con-di)
- [Configuración de DI en .NET Core](#configuración-de-di-en-net-core)
  - [Registro de servicios y ciclos de vida](#registro-de-servicios-y-ciclos-de-vida)
  - [Ejemplos de servicios por ciclo de vida](#ejemplos-de-servicios-por-ciclo-de-vida)
- [Referencias](#referencias)

---

## ❓ ¿Qué es una dependencia?

Una **dependencia** entre clases o paquetes ocurre cuando una clase necesita de otra (funcionalidad, clase, tipo, etc.) para realizar una operación específica. Conceptualmente, esto se traduce a una relación de **"uso"**:  
> Si una clase usa otra, entonces depende de ella.

Este tipo de relación puede dificultar el mantenimiento, la evolución y las pruebas de la aplicación si no se gestiona adecuadamente.

---

## 🚧 Problemas que resuelve el patrón DI

Sin la inyección de dependencias, el desarrollador debe **instanciar manualmente** las dependencias dentro de las clases o métodos. Esto genera:

- 🔒 **Poca flexibilidad:** Cambiar implementaciones o sustituir dependencias es difícil si todo está acoplado directamente.
- 🧪 **Pruebas unitarias difíciles:** El acoplamiento fuerte hace complejo el uso de mocks o fakes.
- 📈 **Difícil de escalar:** A medida que crece la aplicación, gestionar dependencias se vuelve cada vez más complejo y propenso a errores.

---

## 🗒️ Ejemplo de código: Sin DI

Supón dos clases: `MovieController` y `MovieLogic`. La dependencia se da de `MovieController` hacia `MovieLogic`.

```csharp
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
  // ... resto del código
}
```

`MovieLogic` también instancia directamente sus propias dependencias:

```csharp
public sealed class MovieLogic
{
  private readonly IMovieRepository _movieRepository;
  private readonly IUserRepository _userRepository;

  public MovieLogic()
  {
    var dbContext = new DbContext();
    _movieRepository = new MovieRepository(dbContext);
    _userRepository = new UserRepository(dbContext);
  }
  // ...
}
```

Esto **acopla** fuertemente las clases y hace el código rígido y difícil de probar.

---

## 💡 ¿Cómo el patrón DI resuelve el problema?

El patrón desacopla las clases y **gestiona automáticamente** la provisión de dependencias, brindando:

- 🔗 **Bajo acoplamiento:** Las clases dependen de abstracciones, no de implementaciones concretas.
- 🧪 **Pruebas fáciles:** Es simple reemplazar dependencias por mocks o dummies.
- 🏗️ **Flexibilidad y escalabilidad:** El código es modular y fácil de extender.
- ⚙️ **Configuración centralizada:** Todas las dependencias se configuran en un solo lugar: el inicio de la aplicación.
- 📚 **Cumple con OCP y SRP:** Principios de diseño SOLID.

---

## 🗒️ Ejemplo de código: Con DI

```csharp
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
  // ... resto del código
}
```

```csharp
public sealed class MovieLogic(
  IMovieRepository movieRepository,
  IUserRepository userRepository)
{
  // ...
}
```

El framework se encarga de instanciar y entregar las dependencias necesarias.

---

## ⚙️ Configuración de DI en .NET Core

La configuración se realiza en la clase principal de la aplicación (por ejemplo, `Program.cs` en un proyecto Web API con .NET 8):

```csharp
var builder = WebApplication.CreateBuilder(args);

// Registro de controladores
builder.Services.AddControllers();

// Registro de dependencias
builder.Services
  // Lógica de negocio
  .AddScoped<IMovieLogic, MovieLogic>()
  // Acceso a datos
  .AddScoped<DbContext, VidlyContext>()
  .AddScoped<IMovieRepository, MovieRepository>()
  .AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

app.MapControllers();

app.Run();
```

---

# 🔄 Ejemplos de Servicios por Ciclo de Vida en Dependency Injection (.NET)

En .NET, los servicios registrados en el contenedor de dependencias pueden tener tres ciclos de vida principales: **Singleton**, **Scoped** y **Transient**. Cada uno tiene características específicas y es adecuado para diferentes tipos de servicios.

---

## 🔄 Ejemplos de Servicios por Ciclo de Vida

### 🗄️ Singleton

- **Descripción:** Solo existe **una única instancia** durante toda la vida de la aplicación. Se comparte entre todas las peticiones y usuarios.
- **Uso típico:** Servicios que mantienen estado global o recursos compartidos, y no dependen de datos cambiantes por request.
- **Ejemplo de servicios Singleton:**
  - Servicio de configuración de la aplicación
  - Servicio de logging (que escribe en un archivo o consola)
  - Servicio de acceso a la caché en memoria global

```csharp
public interface IAppConfigService { /* ... */ }
public class AppConfigService : IAppConfigService { /* ... */ }

// Registro
services.AddSingleton<IAppConfigService, AppConfigService>();
```

---

### 🔄 Scoped

- **Descripción:** Se crea **una instancia por cada request** HTTP. Todos los componentes dentro de la misma petición comparten la instancia.
- **Uso típico:** Servicios que manejan contexto de usuario, acceso a datos o lógica de negocio donde la consistencia dentro de una misma request es importante.
- **Ejemplo de servicios Scoped:**
  - Servicio de acceso a base de datos (DbContext)
  - Servicio de lógica de negocio (por ejemplo, procesamiento de órdenes)
  - Servicio que almacena información del usuario autenticado durante la request

```csharp
public interface IOrderService { /* ... */ }
public class OrderService : IOrderService { /* ... */ }

// Registro
services.AddScoped<IOrderService, OrderService>();
```

---

### ♻️ Transient

- **Descripción:** Se crea **una nueva instancia cada vez** que se solicita la dependencia. No se comparte en ningún contexto.
- **Uso típico:** Servicios ligeros y sin estado, que no mantienen información entre usos.
- **Ejemplo de servicios Transient:**
  - Servicio de utilidad (por ejemplo, generación de tokens únicos)
  - Servicio helper de formateo o validación de datos
  - Servicio que implementa lógica puntual o tareas efímeras

```csharp
public interface ITokenGenerator { /* ... */ }
public class TokenGenerator : ITokenGenerator { /* ... */ }

// Registro
services.AddTransient<ITokenGenerator, TokenGenerator>();
```

---

### 🏁 Orden de duración de los ciclos de vida

El orden de duración (de mayor a menor) es:

```
Singleton > Scoped > Transient
```

- **Singleton**: Vive toda la vida de la aplicación (más largo).
- **Scoped**: Vive sólo durante una petición (intermedio).
- **Transient**: Vive sólo hasta que termina el uso (más corto).

> **Regla de oro:** Nunca inyectes un servicio de vida más corta (ej: Scoped o Transient) en uno de vida más larga (ej: Singleton), ya que puede causar errores y comportamientos inesperados.

---

### 📝 Resumen visual

| Ciclo de Vida | Instancias por app | Instancias por request | Instancias por uso | Ejemplo típico                    |
|:-------------:|:------------------:|:---------------------:|:------------------:|:----------------------------------|
| Singleton     |        1           |          1            |        1           | Configuración, logging, caché     |
| Scoped        |        1           |          1            |      1+ (por req)  | DbContext, lógica de negocio      |
| Transient     |        1+          |         1+            |      1+            | Helpers, generadores, validadores |

---

## 📚 Referencias

- [DI - Dependency injection in .NET Core (Microsoft Docs)](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-8.0#entity-framework-contexts)

---
