# 🩺 Health Checks Automáticos en ASP.NET Core con EF Core

## 🚀 Configuración Básica

1. **Agregar NuGet Packages**  
   Asegúrate de tener los siguientes paquetes instalados:
   ```bash
   dotnet add package Microsoft.Extensions.Diagnostics.HealthChecks
   dotnet add package Microsoft.EntityFrameworkCore.SqlServer # O el proveedor de tu base de datos
   dotnet add package AspNetCore.HealthChecks.EntityFrameworkCore
   ```

2. **Registrar Health Checks en Program.cs**
   ```csharp
   builder.Services.AddDbContext<MyDbContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

   builder.Services.AddHealthChecks()
       .AddDbContextCheck<MyDbContext>("Database");
   ```

3. **Exponer el Endpoint de Health Check**
   ```csharp
   app.MapHealthChecks("/health");
   ```

---

## 🔎 Ejemplo Avanzado: Chequeo de Base de Datos + Custom Check

```csharp
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

builder.Services.AddHealthChecks()
    .AddDbContextCheck<MyDbContext>(
        name: "SQL_DB",
        failureStatus: HealthStatus.Unhealthy,
        tags: new[] { "db", "sql" }
    )
    .AddCheck("CustomCheck", () =>
        DateTime.UtcNow.Minute % 2 == 0
            ? HealthCheckResult.Healthy("It's an even minute!")
            : HealthCheckResult.Unhealthy("Odd minute, something is off."),
        tags: new[] { "custom" }
    );
```

---

## ⚡ Personalizar la Respuesta del Endpoint

Puedes personalizar la respuesta para que sea más útil para tus sistemas de monitorización:

```csharp
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.Text.Json;

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";
        var result = JsonSerializer.Serialize(new
        {
            status = report.Status.ToString(),
            checks = report.Entries.Select(e => new {
                check = e.Key,
                status = e.Value.Status.ToString(),
                description = e.Value.Description
            }),
            timestamp = DateTime.UtcNow
        });
        await context.Response.WriteAsync(result);
    }
});
```

---

## 🟢 Ejemplo de Respuesta

```json
{
  "status": "Healthy",
  "checks": [
    { "check": "SQL_DB", "status": "Healthy", "description": "A database check" },
    { "check": "CustomCheck", "status": "Healthy", "description": "It's an even minute!" }
  ],
  "timestamp": "2025-09-30T17:00:00Z"
}
```

---

## 🛡️ Buenas Prácticas

- Usa tags para filtrar los checks en despliegues avanzados.
- Personaliza el endpoint para que devuelva solo la información necesaria.
- Configura alertas automáticas en tu infraestructura usando `/health`.

---

**¡Así tu API será robusta y confiable! 🚀**

---

## Referencias
- [Detalles](https://www.youtube.com/watch?v=4abSfjdzqms)
- [Blog](https://khalidabuhakmeh.com/health-checks-for-aspnet-core-and-entity-framework-core)
