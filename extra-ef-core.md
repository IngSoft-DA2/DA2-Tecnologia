[🔙 Indice](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/ef-core#indice) → [🏠 Main](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/main?tab=readme-ov-file#da2-tecnologia--dise%C3%B1o-de-aplicaciones-2)

# 🧠 Resumen EF Core

---

## 📝 ¿Qué es EF Core?

**Entity Framework Core (EF Core)** es un ORM (Object Relational Mapper) moderno para .NET que facilita el trabajo con bases de datos relacionales desde código C#. Permite mapear clases (entidades) a tablas y operar con datos de forma abstracta, sin escribir SQL manualmente la mayor parte del tiempo.

---

## 🔍 ¿Cómo opera EF Core?

- **Mapeo de entidades:** Las clases de C# se mapean a tablas y las propiedades a columnas.
- **DbContext:** Es el núcleo de EF Core. Representa una sesión de trabajo y el punto de acceso para todas las operaciones con la base de datos (consultas, escritura, migraciones, etc).
- **Consultas LINQ:** Permite escribir queries en C# usando LINQ, que EF Core traduce a SQL bajo el capó.
- **Seguimiento de cambios (Change Tracking):** EF Core registra los cambios en las entidades para saber qué actualizar en la base de datos cuando se llama a `SaveChanges()`.
- **Migraciones:** Puedes crear y aplicar migraciones para evolucionar el esquema de la base de datos desde el código.
- **Transacciones:** EF Core soporta transacciones automáticas y manuales para garantizar la integridad de los datos en operaciones complejas.
- **Relaciones:** Puedes modelar relaciones uno a uno, uno a muchos y muchos a muchos entre entidades.

---

## 🗂️ Elementos principales a tener en cuenta

### 1. **DbContext**
- El ciclo de vida importa: normalmente se usa una instancia por cada request en aplicaciones web (Scoped).
- Nunca compartas una instancia de DbContext entre threads.
- El DbContext se encarga de abrir y cerrar la conexión a la base de datos solo cuando lo necesita (late open, early close).

### 2. **Entidades**
- Las clases que representan los datos; deben tener una clave primaria (`Id`).
- Pueden incluir propiedades de navegación para modelar relaciones.

### 3. **DbSet**
- Es una propiedad en el DbContext que representa una tabla.
- Ejemplo: `public DbSet<User> Users { get; set; }`

### 4. **Consultas**
- Se definen con LINQ, pero **no se ejecutan hasta que se enumeran** (por ejemplo con `.ToList()`)
- Puedes filtrar, ordenar, proyectar y agrupar los datos.

### 5. **Seguimiento de cambios**
- EF Core detecta automáticamente los cambios en entidades que están siendo "trackeadas".
- Puedes desactivar el seguimiento para consultas de solo lectura usando `.AsNoTracking()`.

### 6. **Migraciones**
- Permiten actualizar el esquema de la base de datos sin perder datos.
- Se gestionan con comandos como `dotnet ef migrations add` y `dotnet ef database update`.

### 7. **Transacciones**
- EF Core inicia una transacción implícita al llamar a `SaveChanges()`.
- Puedes controlar las transacciones manualmente para mayor control.

### 8. **Configuración**
- Puedes configurar el modelo usando Data Annotations (atributos en las clases) o la API Fluent (en el método `OnModelCreating`).

---

## ⚠️ Buenas prácticas y recomendaciones

- Define una clave primaria en todas las entidades.
- Usa nombres claros y consistentes para las clases y propiedades.
- Evita operaciones masivas en el DbContext; divídelas en lotes si es necesario.
- Usa migraciones para mantener el esquema sincronizado con el código.
- Entiende el ciclo de vida del DbContext y evita compartirlo innecesariamente.
- Desactiva el seguimiento cuando solo necesites leer datos.

---

## 📚 Material recomendado

- [Documentación oficial de EF Core](https://learn.microsoft.com/en-us/ef/core/)
- [Guía rápida de EF Core](https://learn.microsoft.com/en-us/ef/core/get-started/overview/first-app)
- [Migraciones en EF Core](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/)
- [Configuración avanzada del modelo](https://learn.microsoft.com/en-us/ef/core/modeling/)

---

<p align="center">
  <b>EF Core simplifica y potencia el acceso a datos en .NET.<br>
  Conocer sus conceptos básicos y buenas prácticas es clave para lograr aplicaciones eficientes y mantenibles.</b>
</p>
