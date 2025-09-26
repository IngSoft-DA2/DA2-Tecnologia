[🔙 Indice](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/filters?tab=readme-ov-file#indice) -> [🏠 Main](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/main?tab=readme-ov-file#-temas-y-ejemplos-de-c%C3%B3digo)

# 💥 Filtros de Excepciones en ASP.NET Core

Los **filtros de excepciones** permiten manejar de manera global las excepciones no controladas que ocurren en tu aplicación ASP.NET Core. Son ideales para definir políticas personalizadas para el tratamiento de errores, como el registro de logs, la transformación de mensajes de error o la respuesta uniforme al cliente.

---

## ⚡ ¿Cómo funcionan?

- Se ejecutan cuando una excepción no ha sido capturada (_uncaught_) durante la ejecución de un controlador o acción.
- Permiten interceptar errores y definir una respuesta personalizada antes de que la excepción llegue al cliente.
- Son especialmente útiles como **último recurso**, pero no ofrecen la flexibilidad total de los middlewares para el manejo de errores.

---

## 🧑‍💻 Implementación básica

Para crear un filtro de excepción personalizado, implementa la interfaz `IExceptionFilter` y su método `OnException`:

```csharp
public sealed class CustomExceptionFilterAttribute : Attribute, IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        // Tu lógica para manejar la excepción
        // Ejemplo: Registrar el error y enviar una respuesta amigable
        context.ExceptionHandled = true;
        context.Result = new ObjectResult("Ha ocurrido un error inesperado 😱")
        {
            StatusCode = 500
        };
    }
}
```

---

## 📌 Uso: Aplicar el filtro en controllers

Puedes usar el filtro a nivel de clase para que todas las acciones del controlador estén protegidas:

```csharp
[ApiController]
[Route("endpoints")]
[CustomExceptionFilter] // El filtro maneja todas las excepciones del controlador
public sealed class CustomController : ControllerBase
{
    [HttpGet]
    public void Action()
    {
        // Código que podría lanzar una excepción
    }
}
```

---

## 🏗️ Estrategias de Implementación

### 1️⃣ Filtros por controlador

Puedes crear un filtro de excepción independiente para cada controlador, manejando únicamente las excepciones que ocurren en ese contexto:

```csharp
public sealed class Controller1ExceptionFilterAttribute : Attribute, IExceptionFilter
{
    // Maneja errores específicos de Controller1
}

public sealed class Controller2ExceptionFilterAttribute : Attribute, IExceptionFilter
{
    // Maneja errores específicos de Controller2
}
```

Y aplicarlos así:

```csharp
[ApiController]
[Route("controller1")]
[Controller1ExceptionFilter]
public sealed class Controller1Controller : ControllerBase
{
    // Acciones protegidas por el filtro de Controller1
}

[ApiController]
[Route("controller2")]
[Controller2ExceptionFilter]
public sealed class Controller2Controller : ControllerBase
{
    // Acciones protegidas por el filtro de Controller2
}
```

> Esta estrategia facilita el mantenimiento y permite reglas personalizadas por controlador.

---

### 2️⃣ Filtro global de excepciones

Puedes definir un solo filtro de excepción para toda la aplicación y registrarlo globalmente en `Program.cs`:

```csharp
builder.Services.AddControllers(options =>
{
    options.Filters.Add<CustomExceptionFilter>();
});
```

> Así, cualquier excepción no controlada será interceptada por este filtro, sin necesidad de aplicarlo en cada controlador.

---

## 📝 Detalles importantes

- Para que el filtro gestione la excepción correctamente, asigna `context.ExceptionHandled = true` o establece un valor en `context.Result`.
- Los filtros de excepción **no capturan errores lanzados fuera del contexto MVC** (por ejemplo, en middlewares).
- Considera complementar los filtros de excepción con middlewares para un manejo más flexible y global de errores.

---

## 🛠️ Alternativa: Middleware de manejo de errores (`UseExceptionHandler`)

Una alternativa poderosa y recomendada en .NET 8 para el manejo global de errores es el uso del middleware `UseExceptionHandler`. Este middleware captura **todas** las excepciones no controladas en la pipeline, incluso las que ocurren fuera del MVC (por ejemplo, en middlewares, endpoints minimalistas, etc).

### 🚦 ¿Cómo configurarlo?

En `Program.cs`:

```csharp
app.UseExceptionHandler("/error");

// O para desarrollo (muestra detalles de la excepción)
app.UseDeveloperExceptionPage();
```

Esto redirige cualquier excepción no manejada al endpoint `/error`.

---

### 🏁 ¿Cómo debe ser el endpoint `/error`?

Debes definir el endpoint `/error` en tu API para manejar la excepción y devolver una respuesta amigable al cliente. Un ejemplo típico:

```csharp
[ApiController]
[Route("error")]
public class ErrorController : ControllerBase
{
    [HttpGet]
    public IActionResult HandleError()
    {
        var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
        var exception = exceptionFeature?.Error;

        // En producción, evita mostrar detalles del error
        var details = HttpContext.RequestServices.GetService<IWebHostEnvironment>()?.IsDevelopment()
            ? exception?.Message
            : "Ha ocurrido un error inesperado.";

        return Problem(
            statusCode: 500,
            title: "Error en el servidor"
            detail: details,
        );
    }
}
```

---

# ❓ ProblemDetails y Results.Problem en ASP.NET Core

## ¿Qué es ProblemDetails?

**ProblemDetails** es una clase estándar en ASP.NET Core que define el formato recomendado para las respuestas de error en APIs REST, siguiendo el estándar [RFC 7807](https://tools.ietf.org/html/rfc7807).

Cuando tu API retorna errores usando este formato, los clientes pueden interpretar, mostrar y manejar los problemas de manera uniforme y predecible.

### Ejemplo de respuesta ProblemDetails (JSON)

```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.6.1",
  "title": "An error occurred while processing your request.",
  "status": 500,
  "detail": "Información adicional del error",
  "instance": "/error"
}
```

- **type**: URL con más información sobre el tipo de error.
- **title**: Breve descripción del error.
- **status**: Código HTTP.
- **detail**: Mensaje adicional o detalles del error.
- **instance**: Endpoint donde ocurrió el error.

---

## ¿Qué es Results.Problem?

`Results.Problem` es un método en ASP.NET Core (Minimal APIs y Controllers) que te permite devolver respuestas usando el formato **ProblemDetails** de manera sencilla y estandarizada.

### Ejemplo en un Controller

```csharp
public IActionResult HandleError()
{
    return Problem(
        detail: "Ocurrió un error inesperado.",
        statusCode: 500,
        title: "Error en el servidor"
    );
}
```

### Ejemplo en Minimal API

```csharp
app.Map("/error", (HttpContext context) =>
{
    var ex = context.Features.Get<IExceptionHandlerFeature>()?.Error;
    return Results.Problem(
        statusCode: 500,
        title: "Error en el servidor"
        detail: ex?.Message ?? "Error desconocido",
    );
});
```

---

## 🚦 ¿Por qué usar ProblemDetails y Results.Problem?

- **Estandarización:** Las respuestas de error tienen un formato uniforme.
- **Facilidad de parsing:** Los clientes pueden interpretar errores fácilmente.
- **Documentación:** El cliente sabe cómo entender y mostrar los errores.
- **Extensibilidad:** Puedes agregar campos personalizados si lo necesitas.

---

### 🛡️ ¿Qué pasa en modo Release?

- En **modo Release** (`ASPNETCORE_ENVIRONMENT=Production`), el middleware oculta los detalles internos de la excepción por seguridad.
- El endpoint `/error` debe evitar enviar información sensible en la respuesta.
- El método `Results.Problem` (o `ProblemDetails`) ayuda a estandarizar el formato de los errores y evitar exponer datos confidenciales.

---

## 📚 Material de lectura

- [Filtros de excepción en ASP.NET Core (Documentación Oficial)](https://learn.microsoft.com/en-us/aspnet/core/mvc/controllers/filters?view=aspnetcore-8.0#exception-filters)
- [UseExceptionHandler Middleware](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/error-handling?view=aspnetcore-8.0#the-useexceptionhandler-exception-handling-middleware)
- [RFC 7807 - Problem Details for HTTP APIs](https://tools.ietf.org/html/rfc7807)
- [Documentación oficial ProblemDetails](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.problemdetails)
- [Use Results.Problem en Minimal APIs](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-8.0#problem-details-responses)

---

> Los filtros de excepción y los middlewares de manejo de errores pueden usarse juntos para crear una estrategia sólida y flexible, protegiendo tu API en todos los escenarios. ¡Elige el enfoque que mejor se adapte a tus necesidades! 🚀🛡️
