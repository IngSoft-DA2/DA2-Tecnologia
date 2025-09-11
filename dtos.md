[🔙 Indice](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/web-api?tab=readme-ov-file#indice) → [🏠 Main](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/main?tab=readme-ov-file#da2-tecnologia--dise%C3%B1o-de-aplicaciones-2)

# 📦 DTO (Data Transfer Object) en .NET

Un **DTO** (*Data Transfer Object*) es un objeto simple cuyo principal propósito es **transportar datos** entre procesos, capas o servicios en una aplicación. Los DTOs NO contienen lógica de negocio, solo estado. Son extremadamente comunes en arquitecturas en capas, especialmente en APIs, donde se necesita enviar y recibir información entre el frontend y el backend.

---

## 🤔 ¿Por qué usar un DTO?

- **Separación de responsabilidades:** Permiten desacoplar la estructura interna de las entidades de dominio de la forma en la que se exponen los datos a consumidores externos (como APIs).
- **Seguridad y control:** Puedes exponer solo la información necesaria y proteger datos sensibles internos.
- **Optimización:** Permiten enviar solo los campos requeridos, reduciendo el tamaño y la complejidad de la información transferida.
- **Versionamiento:** Facilitan la evolución de la API sin afectar la lógica de negocio interna.

---

## 🎯 Objetivo de un DTO

El objetivo principal de un DTO es **servir como contrato de datos** entre diferentes capas o sistemas, garantizando que solo se transfiera la información necesaria, en el formato esperado, y evitando exponer detalles internos o sensibles de la aplicación.

---

## ✅ Ventajas

- **Encapsulamiento:** No expones entidades de dominio ni estructuras internas.
- **Menor acoplamiento:** Cambios internos no afectan directamente a los consumidores externos.
- **Simplificación:** Solo transfieres lo que es necesario, evitando datos redundantes.
- **Validación y consistencia:** Puedes validar los datos recibidos antes de mapearlos a tus modelos de dominio.

---

## ⚠️ Desventajas

- **Sobrecarga de código:** Necesitas crear, mapear y mantener estructuras adicionales.
- **Duplicación:** Puede haber campos repetidos en distintos DTOs.
- **Sin lógica:** No pueden tener comportamiento, solo datos.
- **Mantenimiento:** Si el modelo cambia frecuentemente, los DTOs pueden volverse difíciles de mantener.

---

## 🏗️ Estructura recomendada de un DTO en C#

### Implicancias de definir DTOs como `readonly struct`

- **Tipo por valor:** Los `struct` son tipos por valor. Se copian en cada asignación o parámetro por valor, lo que puede mejorar la eficiencia para objetos pequeños, pero puede penalizar para objetos grandes.
- **Inmutabilidad:** Con `readonly`, garantizas que las propiedades no pueden cambiar una vez creado el struct.
- **Rendimiento:** Beneficioso en escenarios de alto rendimiento y objetos pequeños; evita la presión en el recolector de basura (GC).
- **Limitaciones:** No soporta herencia, ni igualdad estructural nativa, ni constructor primario (ver más abajo).
- **Serialización:** Compatible con serializadores modernos, pero puede requerir configuración adicional.
- **Escenarios ideales:** DTOs muy simples y pequeños.

### Implicancias de definir DTOs como `sealed record`

- **Tipo por referencia:** Los `record` son tipos por referencia (clase), viven en el heap y se pasan por referencia.
- **Inmutabilidad:** Inmutables por defecto si usas propiedades `init`.
- **Igualdad estructural:** Comparan por valores de sus propiedades, no por referencia.
- **Serialización:** Soportados de forma nativa y sencilla por los serializadores actuales.
- **Herencia:** Un `sealed record` no puede ser heredado, lo que refuerza su uso como contrato cerrado.
- **Escenarios ideales:** DTOs de tamaño medio a grande, o cuando buscas máxima claridad y compatibilidad.

### Record struct y constructor primario

- **record struct:** Permite tener un tipo valor (struct) con igualdad estructural y constructor primario.
- **Constructor primario:** Permite definir todos los parámetros del constructor en la declaración, igual que los records de clase.
- **readonly record struct:** Combina inmutabilidad, igualdad estructural, tipo valor y constructor primario.

#### Ejemplo de record struct:
```csharp
public readonly record struct ProductRequestDto(string Name, decimal Price);
```

### ¿Puedo definir un struct tradicional con constructor primario?

- **No es posible** en C# (hasta la versión 12) definir un `struct` tradicional con constructor primario.
- Los constructores primarios solo están permitidos en `record class` y `record struct`.

#### Tabla comparativa

| Tipo                  | Valor/Referencia | Inmutable | Igualdad estructural | Constructor primario | Herencia | Serialización fácil |
|-----------------------|:---------------:|:---------:|:-------------------:|:-------------------:|:--------:|:------------------:|
| struct                | Valor           | Opcional  | No                  | No                  | No       | Sí (*)             |
| readonly struct       | Valor           | Sí        | No                  | No                  | No       | Sí (*)             |
| record class          | Referencia      | Sí        | Sí                  | Sí                  | Sí       | Sí                 |
| sealed record         | Referencia      | Sí        | Sí                  | Sí                  | No       | Sí                 |
| record struct         | Valor           | Opcional  | Sí                  | Sí                  | No       | Sí                 |
| readonly record struct| Valor           | Sí        | Sí                  | Sí                  | No       | Sí                 |

> (*) Los struct pueden requerir configuración extra para serialización con algunos frameworks.

### Ejemplos prácticos

**readonly struct**
```csharp
public readonly struct UserDto
{
    public int Id { get; init; }
    public string Name { get; init; }
}
```

**sealed record**
```csharp
public sealed record UserDto(int Id, string Name);
```

**readonly record struct**
```csharp
public readonly record struct UserDto(int Id, string Name);
```

### Recomendaciones

- Usa `sealed record` o `readonly record struct` para la mayoría de los DTOs modernos: son claros, inmutables, fácilmente serializables y soportan constructor primario.
- Reserva `readonly struct` para casos de DTOs muy pequeños y de uso intensivo donde el rendimiento por valor sea crítico.
- Evita clases mutables o herencia desde entidades de dominio.

---

## 👍 Buenas prácticas

- **No definirlos como `class`.**
- **Mantén los DTOs simples:** Solo propiedades, sin métodos de lógica.
- **Usa tipos primitivos o estructuras simples:** Para facilitar la serialización/deserialización.
- **Agrupa solo los datos necesarios para el contexto de uso:** No incluyas más información de la necesaria.
- **Usa DTOs para entrada y salida:** Puedes crear DTOs diferentes para requests y responses si es necesario.
- **Valida los datos en el DTO antes de mapearlos al modelo de dominio:** Utiliza data annotations si aplica.
- **Utiliza automappers o herramientas similares para mapear entre entidades y DTOs:** Facilita el mantenimiento y reduce errores.

---

## 👎 Malas prácticas

- **Agregar lógica de negocio en los DTOs.**
- **Heredar DTOs de entidades de dominio, o viceversa.**
- **Permitir que el DTO tenga referencias a la infraestructura o lógica de la aplicación.**
- **Exponer directamente entidades del dominio como DTOs.**
- **Hacer DTOs innecesariamente complejos o anidados.**
- **Definir DTOs como clases mutables o no inmutables.**

---

## 📝 Ejemplo de buena y mala práctica

**✔️ Correcto:**

```csharp
public readonly struct ProductRequestDto
{
    public string Name { get; init; }
    public decimal Price { get; init; }
}
```

**❌ Incorrecto:**

```csharp
public class ProductDto : Product // Herencia de entidad de dominio: ¡MAL!
{
    public bool IsValid() { ... } // Lógica en el DTO: ¡MAL!
    public decimal Price { get; set; } // Propiedad mutable: ¡MAL!
}
```

---

## 📚 Resumen

- Los DTOs son objetos simples e inmutables usados para transferir datos entre capas o servicios.
- Defínelos como `readonly struct` en C# para máxima seguridad y eficiencia.
- Son útiles para mantener el desacoplamiento y proteger la arquitectura interna.
- Deben ser simples, sin lógica, y contener solo los datos necesarios.
- Su uso correcto mejora la mantenibilidad y seguridad de tu aplicación.

---
