[Volver - EFCore](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/ef-core)
# Test de conexión a la base de datos

La idea de este aplicacion de consola es poder probar que el connection string funcione correctamente. Esta aplicación de consola lo que hace es conectarse al servidor sql, crear una base de datos con el nombre `Test` si no existe y agregar un elemento.

En el valor de la property `Server` en el connection string, deberan de proveer su server para que les funcione.

## Explicacion del codigo
Una de las configuraciones que tenemos que realizar es que motor de base de datos EFCore va a estar operando. EFCore soporta muchos motores de base de datos, la eleccion de cual utilizar es una decision de negocio y de costos. Algo importante a tener en cuenta es que los motores de base dedatos soportados por EFCore son motores de base de datos relacionales.

Para realizar dicha configuracion en una aplicacion de consola, es necesario crear una instancia manualmente utilizando el patron `Builder` para el tipo de la configuracion de la siguiente manera:

```C#
var builder = new DbContextOptionsBuilder<TestDbContext>();
```

De esta forma estamos indicando que queremos realizar una configuracion para un contexto en particular, en este caso `TestDbContext`.

Una vez creada la instancia queda setearle en que motor va a estar trabajando (esta configuracion es requerida) y si es necesario alguna configuracion opcional.

```C#
builder
    .UseSqlServer(connectionString)
    .LogTo(Console.WriteLine, LogLevel.Information);
```

En este ejemplo se configuro que el contexto trabaje con un motor de base de datos SQL al utilizar la funcion extension `UseSqlServer(string connectionString)`. Se le llama funcion extension ya que no es una funcion que venga en el paquete `Microsoft.EntityFrameworkCore` sino que hay que instalar el paquete correspondiente que soporte dicho motor, el cual es `Microsoft.EntityFrameworkCore.SqlServer`.

La siguiente funcion `LogTo`, sirve para que se loguee cualquier accion realizada con el contexto en la consola. En este caso se le configuro que el logueo de las acciones sean en la consola y que sean unicamente de nivel de informacion. Esta configuracion tiene propositos para inspeccionar como opera el contexto con la base de datos y se pueda entender como esta armando las consultas y que consultas esta realizando. Esta configuracion nunca deberia de ser usada en un ambiente de produccion ya que es contraproducente para la performance de la aplicacion, solo deberia de ser usada con fines de inspeccion en ambientes locales. 

Una vez realizada la configuracion que nuestor contexto queremos que tenga, se lo pasamos al contexto para poder crear una instancia de este:
```C#
var context = new TestDbContext(builder.Options);
```

Como el contexto declaro que su dependencia es del tipo `DbContextOptions` y nuestra configuracion es del tipo `DbContextOptionsBuilder`, dos tipos que por si solos no matchean, es necesario decirle a `DbContextOptionsBuilder` que nos de la configuracion que se creo.
U
na vez creado el contexto, al solo querer probar la conexion sin la necesidad de ningun tipo de migracion, al momento de ejecucion tenemos que asegurarnos de que dicha base de datos exista y en caso de que no que se cree.

Para ello llamaremos la funcion `EnsureCreated` que es parte de la instancia de un contexto: 
```C#
context.Database.EnsureCreated();
```
Hasta este punto si no ocurrio ningun tipo de excepcion, podemos decir que el connection string utilizado es correcto, es decir, que al server se esta apuntando existe y esta en ejecucion.

Lo siguiente a verificar son operaciones simples con la base de datos para corroborar de que este todo correctamente con las clases utilizadas:
```C#
var newUser = new User
{
    Name = "something",
    Book = new("book")
};
context.Users.Add(newUser);
context.SaveChanges();

var users = context
    .Users
    .ToList();

Console.WriteLine(users);
```

Este codigo lo que realiza es la creacion de un usuario con un libro los cuales se van agregar a la tabla `Users`, y luego se van a obtener todos.

## Breakdown de operaciones basicas con EFCore
Cuando uno utiliza una instancia del contexto concreto puede acceder a las tablas a traves de las properties `DbSet` que el contexto defina, es por eso que podemos agregar usuarios de la siguiente manera:
```C#
context.Users.Add(newUser);
```
Al momento de llamar la funcion `Add`, podremos ver en la consola que no se realizo ninguna query a la base de datos, esto es porque EFCore al llamar el metodo `Add`, `Update` o `Delete` es marcar las entidades pasadas por parametros con un estado `Added`, `Modified`, `Deleted`.

Esto es verdaderamente util para poder concatenar mas de una operacion y al cuando se quiera ir a la base de datos ir una unica vez en vez de muchas individuales. Es por esto que dichas funciones por si solas no tienen ningun tipo de efecto.

Esto es realmente util cuando por ejemplo quisieramos realizar multiples agregaciones en tablas diferentes y solo se quiere impactar en la base de datos una unica vez:
```C#
context.Table1(entity1);
context.Table2(entity2);
context.Table3(entity3);

context.SaveChanges();
```
Este diseño de EFCore ayuda en la performance de la aplicacion, ya que es mas performante una unica interaccion con la base de datos con multiples operaciones antes que multiples interacciones individuales.

Como ya se podra deducir, para indicar que se quiere impactar en la base de datos, se tiene que utilizar la funcion:
```C#
context.SaveChanges();
```
De esta forma le indicamos a EFCore que guarde los cambios concatenados en el contexto e impacte en la base de datos.

Es en este momento donde vamos a poder ver que queries arma EFCore y se ejecutan.

> [!NOTE]
> En este ejemplo se esta haciendo una agregacion en cascada, se agregan los usuarios con el libro, porque el libro es requerido para la creacion del usuario.
> Esto quiere decir que una row en la tabla Users no puede tener un null o un string vacio en la columna BookId por ser una FK a la tabla Books.

Para la obtencion de las entidades, por si solo el acceso a la tabla `context.Users` tampoco tendra ningun tipo de efecto, debe ser acompañado por la funcion `ToList`, `First` o `FirstOrDefault`.

Esto va hacer que EFCore realice la query `SELECT` correspondiente a la base de datos.

## Breakdown de las entidades
En este codigo podemos encontrar la entidad `User`
```C#
public sealed record class User()
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string Name { get; set; } = null!;

    public Guid BookId { get; set; }

    public Book Book { get; set; } = null!;

    public User(string name)
      : this()
    {
      Name = name
    }
}
```
Donde con:
- `sealed` indicamos que dicha clase no tiene clases hijas
- `record class` indicamos que dicha clase se compare por los valores y no por la posicion en memoria y que no sea inmutable, que la instancia se le pueda modificar el estado
- Los setters `init` indican que solamente se le puede setar un valor a la property a la hora de inicializacion de un objeto de la clase, una vez pasado el momento no se le podra cambiar el valor
- En la linea de definicion ocurre tambien una inicializacion inicial `public Guid Id { get; init; } = Guid.NewGuid();`
- `null!` es para indicar que dichas properties van a tener un valor una vez inicializados, sirve para indicar tambien a EFCore que son requeridos.
- En C# 12 podemos encontrar el concepto de constructores primarios `User()`, el constructor primario debe ser invocado en cualquier constructor secundario `public User(string name) : this()`

Para indicar una referencia `unidireccional` de `User` a `Book` basta con poner una property de tipo `Book` en la clase `User`. EFCore al detectar esta relacion la interpreta y la resuelve de forma correcta con el motor de base de datos a utilizar.

Esto genera el resultado de que se tenga una columna llamada `BookId` en la tabla `Users` que es para indicar que existe una `FK` (foreign key) a la tabla `Books` a la property `Id` de esta tabla.

La construccion de dicha `FK` ocurre concatenando el nombre de la property de la relacion en `User` que se llama `Book` con el nombre de la property `PK` (primary key) en `Book` que es `Id`, de esa forma nace `BookId` en la tabla `Users` como FK.

Esta construccion automatica de EFCore nos impide tener control directo de los valores de estas columnas autogeneradas. Para tener control sobre estas `FK` es necesario crear nosotros mismos la property en la clase. Es por eso que existe la property `Guid BookId` en la clase `User`. Con esta property vamos a poder controlar los valores de la `FK` pudiendo actualizar las relaciones sin la necesidad de tener valores en la property `Book Book`.

Luego tenemos la entidad `Book`
```C#
public sealed record class Book()
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string Name { get; set; } = null!;

    public Book(string name)
        : this()
    {
        Name = name;
    }
}
```

Como podran ver, en ambas entidades existe una property llamada `Id`, este nombre en particular es para indicar que dicha property sea tomada como `PK` por EFCore de forma automatica, es decir, que no es necesario realizar alguna configuracion adicional para marcar dicha property como `PK`.

Es buena practica que toda entidad persistida contenga una `Id` unica para asegurar un acceso rapido a la entidad. Tambien es buena practica que dicha property siempre sea tratada como `PK` ya que dicho valor nunca deberia de ser cambiado.

El tipo de esta `PK` puede ser tanto `string`, `int` como el usado `Guid`. El tipo `Guid` (Globally Unique Identifier) es un entero de 128-bit usado para asegurar un identificador unico. Un valor de este tipo es desplegado con el siguiente formato en string `123e4567-e89b-12d3-a456-426614174000`. El uso de `Guid` como tipo de `PK` nos brinda las siguientes ventajas:

- **Valor global unico**: nos aseguramos de que las `PK` de todas las tablas tengan valores unicos
- **Seguridad**: son difisiles de adivinar en comparacion a valores secuenciales como `int`
- **Escalabilidad**: donde multiples instancias de una misma aplicacion esta haciendo inserciones, estas no se tienen que sincronizar para averiguar el siguiente valor disponible para `PK`

Por ultimo se tiene la siguiente linea
```C#
context.Dispose();
```
La cual sirve para indicar que se cierre la conexion establecida con la base de datos. Esto lo tenemos que hacer manual ya que la conexion la abrimos nosotros mismos, cuando la conexion la establece el framework no es necesario explicitar el cierre de la misma nosotros mismos.

## Ejercicio 1
Modificar el codigo existente para crear un usuario y asociarlo a un libro ya existente a la base de datos, se debe de validar que dicho libro exista. No se debe de obtener el libro.

## Ejercicio 2
Modificar el codigo existente para crear un libro independientemente a la creacion de un usuario. Este usuario tiene que referenciar a dicho libro nuevo. Dichas creaciones deben mantenerse con un unico impacto a la base de datos.
