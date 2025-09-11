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

En C#, la mejor práctica moderna es definir los DTOs como **readonly struct**. Esto significa que:

- Son tipos por valor (no referencia), lo que mejora la eficiencia en escenarios de alto rendimiento.
- Son inmutables: una vez creados, sus valores no pueden ser modificados.
- Solo exponen propiedades de solo lectura y no tienen métodos de lógica.

**Ejemplo correcto de DTO:**

```csharp
public readonly struct UserDto
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }
}
```

- **readonly struct:** Indica que es inmutable.
- **init:** Permite la inicialización solo al momento de crear la instancia, no después.
- **Solo tipos primitivos o simples:** Para facilitar la serialización/deserialización y evitar dependencias con el dominio.

---

## 👍 Buenas prácticas

- **Defínelos como `readonly struct`.**
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
