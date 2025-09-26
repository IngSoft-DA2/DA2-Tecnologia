[🔙 Indice](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/filters?tab=readme-ov-file#indice) -> [🏠 Main](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/main?tab=readme-ov-file#-temas-y-ejemplos-de-c%C3%B3digo)

# 🛡️ Filtros de Autorización en ASP.NET Core

Los **filtros de autorización** son los primeros en ejecutarse dentro de la pipeline de filtros de ASP.NET Core y su función principal es controlar el acceso a las solicitudes (requests) antes de que cualquier otra lógica sea procesada.

---

## 🚦 ¿Cómo funcionan?

- **Prioridad máxima:** Se ejecutan antes que cualquier otro filtro o middleware relacionado al controlador.
- **Control de acceso:** Deciden si una solicitud puede continuar en la pipeline o debe ser bloqueada.
- **Manejo de excepciones:** Si ocurre una excepción en este filtro, **no será manejada automáticamente** por otros filtros, por lo que debes implementar el manejo de errores dentro del filtro.

---

## 🧑‍💻 Implementación de un filtro de autorización personalizado

Para crear un filtro custom de autorización, implementa la interfaz `IAuthorizationFilter` y el método `OnAuthorization`.

```csharp
public sealed class AuthorizationFilterAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // Tu lógica de autorización aquí
        // Ejemplo: verificar credenciales, roles, claims, etc.
    }
}
```

---

## 📌 Cómo aplicar el filtro en Controllers y Actions

Puedes utilizar el filtro tanto a nivel de clase (controller) como a nivel de método (action):

```csharp
[ApiController]
[Route("endpoints")]
[AuthorizationFilter] // Aplica el filtro al controlador completo
public sealed class CustomController : ControllerBase
{
    [HttpGet]
    [AuthorizationFilter] // Aplica el filtro solo a esta acción
    public void MyAction()
    {
        // Acción protegida por el filtro de autorización
    }
}
```

---

## 🧐 Buenas prácticas

- **Maneja las excepciones dentro del filtro:** Usa `try-catch` en tu lógica para evitar errores no controlados que puedan terminar la request abruptamente.
- **Haz la lógica clara:** Asegúrate de que las reglas de autorización sean fáciles de entender y mantener.
- **Combina con roles y claims:** Puedes aprovechar los mecanismos de roles y claims que ASP.NET Core provee para hacer tu filtro más flexible y seguro.

---

## 📚 Material de lectura

- [Authorization en ASP.NET Core (Documentación Oficial)](https://learn.microsoft.com/en-us/aspnet/core/security/authorization/introduction?view=aspnetcore-8.0)

---

> Los filtros de autorización son tu primera línea de defensa para proteger los endpoints de tu aplicación. ¡Úsalos sabiamente! 🛡️🚀
