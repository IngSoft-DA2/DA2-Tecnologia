# 🗂️ Versionado de APIs en .NET: Guía Práctica y Opciones

## 🤔 ¿Por qué versionar tu API?

El versionado de tu API garantiza que puedas:
- 🚀 Evolucionar tu servicio sin romper aplicaciones existentes
- 🛠️ Implementar nuevas funcionalidades de forma segura
- 📚 Mantener la compatibilidad con clientes antiguos

---

## 🏠 Versionado "Casero": Manual y Simple

### 1️⃣ Por URL

Una forma sencilla es incluir la versión en la ruta:

```
GET /api/v1/products
GET /api/v2/products
```

En .NET Core, puedes definir rutas en tu controlador:

```csharp
[Route("api/v1/products")]
public sealed class ProductsV1Controller
: ControllerBase
{
  ...
}

[Route("api/v2/products")]
public sealed class ProductsV2Controller
: ControllerBase
{
  ...
}
```

### 2️⃣ Por Header personalizado

El cliente envía la versión en el header:

```
GET /api/products
Headers: X-API-Version: 1
```

En el controlador puedes leer el header y actuar en consecuencia:

```csharp
string version = Request.Headers["X-API-Version"];
```

---

## 🛠️ Versionado Automático: Usando Microsoft.AspNetCore.Mvc.Versioning

### 1️⃣ Instalación

```bash
dotnet add package Microsoft.AspNetCore.Mvc.Versioning
```

### 2️⃣ Configuración en Program.cs

```csharp
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader(); // O HeaderApiVersionReader
});
```

### 3️⃣ Uso en Controladores

```csharp
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/products")]
public sealed class ProductsController
: ControllerBase
{
    // Acción para v1
}

[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public sealed class ProductsV2Controller
: ControllerBase
{
    // Acción para v2
}
```

#### Alternativamente por Header

```csharp
options.ApiVersionReader = new HeaderApiVersionReader("X-API-Version");
```

---

## 🎯 Buenas Prácticas

| Práctica              | Descripción                                         |
|-----------------------|-----------------------------------------------------|
| ✅ Documentar         | Explica cómo consumir cada versión.                  |
| ✅ Depreciar versiones| Comunica y elimina versiones obsoletas gradualmente. |
| ✅ Pruebas por versión| Testea cada versión por separado.                    |
| ✅ Automatiza         | Usa herramientas para facilitar el mantenimiento.    |

---

## 📚 Recursos

- [Microsoft Docs: API Versioning](https://learn.microsoft.com/en-us/aspnet/core/web-api/advanced/versioning)
- [API Versioning Github](https://github.com/microsoft/aspnet-api-versioning)

---

**¡Versiona tu API, evoluciona sin miedo! 🚀**
