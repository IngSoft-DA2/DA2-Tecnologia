[🔙 Indice](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/ef-core#indice) → [🏠 Main](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/main?tab=readme-ov-file#da2-tecnologia--dise%C3%B1o-de-aplicaciones-2)

# 🔗 Configuración de relación 1 a N (Uno a Muchos) en EFCore

---

## 🎯 Objetivo

En este ejemplo aprenderás cómo modelar y configurar una relación **uno a muchos** entre dos entidades usando Entity Framework Core. Esto significa que una entidad principal (por ejemplo, `User`) puede tener varias entidades relacionadas (por ejemplo, `Book`), mientras que cada entidad secundaria (`Book`) está asociada a una sola entidad principal (`User`).

---

## 🛠️ Modelado de las entidades

### 👤 Entidad principal: User

Para expresar la relación, la entidad principal debe tener una colección que represente las entidades asociadas. Aquí usamos una propiedad de tipo `List<Book>` en la clase `User`:

```csharp
public sealed record class User()
{
    // ...

    public List<Book> Books { get; init; } = [];
    
    // ...
}
```

- La propiedad `Books` representa la colección de libros asociados a un usuario.
- Inicializamos la lista como vacía (`= []`), lo que evita tener que verificar si es `null` antes de operar sobre ella y previene errores comunes de referencia nula.
- Es recomendable inicializar siempre las colecciones en las entidades para facilitar la manipulación y evitar sorpresas durante el ciclo de vida del objeto.

---

### 📚 Entidad secundaria: Book

En la entidad secundaria, configuramos la clave foránea (`FK`) que apunta a la entidad principal. La relación se define de la siguiente manera:

```csharp
public sealed record class Book()
{
    // ...
    public Guid AuthorId { get; init; }
    public User Author { get; init; } = null!;
    // ...
}
```

- `AuthorId`: Es la clave foránea que referencia la entidad principal (`User`). Al nombrar explícitamente la propiedad, controlamos el nombre de la columna en la base de datos y evitamos que EFCore lo genere automáticamente.
- `Author`: Representa la referencia de navegación hacia el usuario relacionado. El uso de `null!` indica a EFCore y al compilador que esta propiedad será inicializada correctamente (aunque no se puede garantizar en tiempo de compilación), ayudando a evitar advertencias de nulabilidad.
- El tipo `Guid` para `AuthorId` garantiza que la propiedad no pueda ser nula, ya que `Guid` es un `struct` en C#, y por lo tanto, siempre contiene un valor (aunque pueda ser `Guid.Empty`).

> 💡 **Nota de buenas prácticas:**  
> Al modelar relaciones en EFCore, es importante inicializar las colecciones y definir claramente las claves foráneas para tener un modelo robusto y fácil de mantener. Esto permite que EFCore entienda la relación y genere correctamente las restricciones en la base de datos.

---

## 🧩 Consideraciones adicionales

- Al definir explícitamente la propiedad `AuthorId` como clave foránea, tienes control total sobre cómo se mapea la relación desde el modelo a la base de datos. Esto es útil si necesitas seguir convenciones de nombres específicas o trabajar con bases de datos existentes.
- La propiedad de navegación `Author` facilita el acceso desde un libro a su usuario relacionado, permitiendo cargar datos de manera eficiente y realizar queries más expresivas.

---

## 🌟 ¡Explora, prueba y experimenta! 😃

Modifica el modelo, agrega entidades, vincula diferentes usuarios con varios libros y observa cómo EFCore gestiona la relación y genera las restricciones necesarias en la base de datos.  
Si tienes dudas, consulta la documentación oficial o participa en la comunidad para aprender aún más sobre relaciones en EFCore.
