[Volver - EFCore](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/ef-core#indice)
# Configuracion N a N

Para realizar esta configuracion es necesario utilizar un tipo de coleccion para una property en ambas clases y que estos se referencien entre ellos. Para ello tendremos en la clase `User` lo siguiente:
```C#
public sealed record class User()
{
    // ...
    public List<Book> Books { get; init; } = [];
    // ...
}
```

Y en la clase `Book` tendremos lo siguiente de forma similar:
```C#
public sealed record class Book()
{
    // ...
    public List<User> Readers { get; init; } = [];
    // ...
}
```

Si dejamos solamente esta configuracion, `EFCore` la sabra interpretar y el creara lo necesario automaticamente. Una de las cosas se crearan automaticamente para modelarlo en tablas, es una tabla de asociacion que conecte ambas tablas. Esta tabla de asociacion `EFCore` decidira como nombrarla y como nombrar las columnas que guarde.

Para lograr tener mas control sobre el dominio y de lo que se utilice, es necesario crear la clase de asociacion que se va a mapear a la tabla de asociacion. Dicha clase seria la siguiente:
```C#
public sealed record class UserBook()
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public Guid ReaderId { get; init; }

    public User Reader { get; init; } = null!;

    public Guid BookId { get; init; }

    public Book Book { get; init; } = null!;
}
```
Una vez creada la clase, vemos que ninguna de las entidades `User` ni `Book` referencian dicha clase. Para que `EFCore` tome dicha clase como la tabla asociativa, es necesario configurarlo en el contexto de la siguiente manera:
```C#
public sealed class TestDbContext
    : DbContext
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
En el `override` del metodo `protected` `OnModelCreating` estamos configurando que la relacion con los libros desde `User` se utilice la estructura de la clase asociativa `UserBook`.
