# 🍰 Buenas Prácticas: Respuestas Parciales en APIs RESTful

## 🤔 ¿Qué son las respuestas parciales?

Las **respuestas parciales** permiten que un cliente solicite solo los campos o propiedades necesarias de un recurso, optimizando el uso de ancho de banda y acelerando las respuestas de la API.

---

## 🚀 ¿Por qué usarlas?

- ⚡ Mejoran el rendimiento y reducen la carga de datos innecesarios.
- 📱 Son ideales para aplicaciones móviles, donde cada byte cuenta.
- 🎯 Permiten interfaces más ágiles y personalizadas para distintos clientes.

---

## 💡 Ejemplo "Casero" en .NET

### 1️⃣ El cliente solicita campos específicos vía query string:
```
GET /api/products?fields=id,name,price
```

### 2️⃣ El controlador filtra la respuesta:
```csharp
[HttpGet]
public IActionResult Get([FromQuery] string fields = "")
{
    var products = _db.Products.ToList();
    if (string.IsNullOrEmpty(fields))
        return Ok(products);

    var fieldList = fields.Split(',').Select(f => f.Trim()).ToList();

    var partialProducts = products.Select(p =>
        fieldList.ToDictionary(
            field => field,
            field => p.GetType().GetProperty(field)?.GetValue(p, null)
        )
    );
    return Ok(partialProducts);
}
```
*Nota: Esto es un ejemplo básico, puedes expandirlo usando librerías como [AutoMapper](https://automapper.org/) o técnicas más robustas.*

---

## 🛡️ Buenas Prácticas

| Práctica             | Descripción                                                        |
|----------------------|--------------------------------------------------------------------|
| ✅ Validar campos    | Rechaza campos inválidos o sensibles que no deben ser expuestos.   |
| ✅ Documentar        | Explica en la API docs cómo solicitar respuestas parciales.         |
| ✅ Optimizar queries | Filtra los datos en la base de datos, no solo en memoria.          |
| ✅ Usar estándares   | Prefiere `$select` de OData si tu stack lo soporta.                |

---

## 🔗 Recursos
---

**¡Haz tu API ágil y eficiente! 🏎️✨**
