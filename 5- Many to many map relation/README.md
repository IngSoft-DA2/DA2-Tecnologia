[🔙 Indice](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/ef-core#indice) → [🏠 Main](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/main?tab=readme-ov-file#da2-tecnologia--dise%C3%B1o-de-aplicaciones-2)

# 🔄 Configuración de relación muchos a muchos (N a N) en EFCore

---

## 🎯 Objetivo

Este ejemplo explica cómo modelar y configurar una relación **muchos a muchos** entre dos entidades en Entity Framework Core. Una relación muchos a muchos significa que varias entidades de un tipo pueden estar relacionadas con varias entidades de otro tipo. Por ejemplo, varios usuarios pueden leer varios libros, y cada libro puede ser leído por varios usuarios.

---

## 🛠️ Modelado de las entidades

### 👤 Entidad User

Para indicar que un usuario puede estar relacionado con varios libros, agregamos una propiedad de tipo colección en la clase `User`:

```csharp
public sealed record class User()
{
    // ...
    public List<Book> Books { get; init; } = [];
    // ...
}
```

- La propiedad `Books` representa la lista de libros que ha leído el usuario.
- Inicializar la lista como vacía previene errores por referencias nulas y facilita el trabajo con la colección.

---

### 📚 Entidad Book

De forma similar, en la entidad `Book` definimos una colección de usuarios que han leído el libro:

```csharp
public sealed record class Book()
{
    // ...
    public List<User> Readers { get; init; } = [];
    // ...
}
```

- La propiedad `Readers` guarda la lista de usuarios asociados a cada libro.

---

## ⚡ Configuración automática de la relación

Si solo agregamos estas propiedades de colección y no hacemos ninguna configuración adicional, **EFCore interpreta automáticamente la relación muchos a muchos** y genera una tabla asociativa interna para manejar los vínculos entre usuarios y libros.  
Esto es conveniente para proyectos simples, pero a veces necesitamos tener más control sobre la tabla asociativa o sobre los datos que guarda.

---

## 👨‍💻 Clase de asociación personalizada

Para tener mayor control sobre la relación y sobre la tabla que se genera en la base de datos, podemos **crear una clase de asociación** explícita que se mapeará a la tabla intermedia:

```csharp
public sealed record class UserBook()
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public Guid ReaderId { get; init; }
    public User Reader { get; init; } = null!;

    public Guid BookId { get; init; }
    public Book Book { get; init; } = null!;
}
```

- `UserBook` representa el vínculo entre un usuario y un libro.
- Incluye campos para las claves foráneas (`ReaderId` y `BookId`) y referencias de navegación (`Reader` y `Book`), además de un identificador único (`Id`) para la fila.

Por defecto, **ni la entidad `User` ni la entidad `Book` referencian directamente a esta clase asociativa**. Para que EFCore la utilice como la tabla intermedia, debemos configurarlo explícitamente en el contexto.

---

## 🏗️ Configuración en el DbContext

En el contexto, definimos el `DbSet` para la clase asociativa y configuramos la relación en el método `OnModelCreating`:

```csharp
public sealed class TestDbContext : DbContext
{
    // ...
    public DbSet<UserBook> UsersBooks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity
                .HasMany(u => u.Books)
                .WithMany(b => b.Readers)
                .UsingEntity<UserBook>();
        });
    }
}
```

- Con `HasMany().WithMany().UsingEntity<UserBook>()` le indicamos a EFCore que use la clase `UserBook` como tabla de asociación entre `User` y `Book`.
- Esto nos da mayor control sobre el modelo de datos, lo que permite agregar propiedades extra a la clase asociativa (por ejemplo, fecha de lectura, calificación, etc.) si lo necesitamos en el futuro.

---

## 🧩 Consideraciones y buenas prácticas

- Utilizar una clase asociativa permite extender la información sobre la relación (por ejemplo, guardar metadatos como fecha de registro, comentarios, etc.).
- Si no necesitas información extra sobre la relación, puedes dejar que EFCore genere la tabla intermedia automáticamente, solo definiendo las propiedades de colección en ambas entidades.
- Siempre inicializa las colecciones en las entidades para evitar errores por referencias nulas.

---

## 🌟 ¡Experimenta, aprende y pregunta! 😃

Modifica el modelo, crea usuarios y libros, vincúlalos usando la relación muchos a muchos y observa cómo EFCore gestiona la tabla de asociación y las restricciones en la base de datos.  
Si tienes dudas, consulta la documentación oficial o participa en la comunidad para seguir aprendiendo sobre EFCore y sus relaciones.
