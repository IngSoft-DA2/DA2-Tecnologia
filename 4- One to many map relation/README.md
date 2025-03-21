[Volver - EFCore](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/ef-core)
# Configuración relacion 1 a N

Para realizar esta configuración es necesario guardar en la entidad primaria (`User`) un estado de tipo colección como `List` del tipo de entidad secundaria (`Book`). Quedando la clase `User` de la siguiente manera:
```C#
public sealed record class User()
{
    // ...

    public List<Book> Books { get; init; } = [];

    //...
}
```
Dicho estado es inicializado para asegurarnos que siempre tenga un valor, aunque sea una lista vacía. De esta forma nos evitamos estar preguntando si la colección es `null` constantemente. También estaríamos realizando la buena practica de nunca retornar `null` cuando se tiene que retornar una colección. El valor por defecto de este típo debemos asegurarnos de que sea una colección vacía.

Luego en la entidad secundaria tenemos que configurar la `FK` a la entidad primaria de la siguiente manera:
```C#
public sealed record class Book()
{
    // ...
    public Guid AuthorId { get; init; }

    public User Author { get; init; } = null!;
    // ...
}
```
En esta entidad estamos configurando que la columna con la constraint `FK` se llame `AuthorId` no dejando la opción de que `EFCore` genere el nombre automaticamente. Con esta configuración tenemos más control sobre el modelado de tablas en la base de datos, es una relación uno a uno y podemos evolucionar y mantener dicho esquema con mayor facilidad. Tambien trae la ventaja de tener cierta flexibilidad en la manipulación de los datos pudiendo ser mas performantes y optimos en el armado de las queries.

Otra cosa a notar es que la property `AuthorId` no tiene configurado de que sea requerido, esto es porque el tipo `Guid` es un `struct` por lo que asegura de que no se le pueda asociar un valor `null`, esta configuración se le pone a la property que identifica la relación, la cual es `Author`. También con esta configuración estamos eligiendo el nombre del rol de como estas dos entidades se ven entre ellas, logrando mas transparencia y similitud con lo encontrado en la base de datos.
