[🔙 Indice](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/ef-core#indice) → [🏠 Main](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/main?tab=readme-ov-file#da2-tecnologia--dise%C3%B1o-de-aplicaciones-2)

# 🚀 Test de conexión a la base de datos

---

## 🎯 Objetivo del proyecto

Esta aplicación de consola tiene como objetivo verificar que el connection string de tu base de datos SQL funcione correctamente. El programa se conecta al servidor, crea una base de datos y realiza operaciones básicas utilizando Entity Framework Core (EFCore).

> 💡 **Importante:**  
> En la propiedad `Server` del connection string, asegúrate de colocar el nombre de tu propio servidor SQL para que la aplicación funcione en tu entorno.

---

## 🛠️ Explicación detallada del código

### 🔧 Configuración del motor de base de datos en EFCore

El primer paso es definir **qué motor de base de datos usará EFCore**. EFCore soporta varios motores, y aquí trabajamos con SQL Server. Esta decisión determina el comportamiento del contexto y la forma en que se conecta a la base de datos.

Para configurar el contexto en una aplicación de consola, usamos el patrón `Builder` para crear una instancia de configuración específica para el contexto:

```csharp
var builder = new DbContextOptionsBuilder<TestDbContext>();
```

Con esto, indicamos que queremos configurar el contexto `TestDbContext`.

A continuación, especificamos el motor de base de datos y configuraciones adicionales (como el logging):

```csharp
builder
    .UseSqlServer(connectionString)
    .LogTo(Console.WriteLine, LogLevel.Information);
```

- `UseSqlServer(connectionString)`: Configura EFCore para trabajar con SQL Server usando el connection string proporcionado.
- `LogTo(Console.WriteLine, LogLevel.Information)`: Permite registrar en la consola todas las acciones (queries, comandos, etc.), lo que facilita la depuración y el aprendizaje.

Luego, creamos la instancia del contexto usando la configuración anterior:

```csharp
var context = new TestDbContext(builder.Options);
```

- El contexto requiere una instancia de `DbContextOptions`, obtenida desde el builder mediante `.Options`.

---

### 🏗️ Creación de la base de datos

Con el contexto configurado, el siguiente paso es asegurarnos de que la base de datos exista y esté lista para operar. Para esto, utilizamos la función `EnsureCreated`:

```csharp
context.Database.EnsureCreated();
```

Esta función verifica si la base de datos existe; si no, la crea con las tablas y relaciones definidas en el modelo.  
Si no se produce ningún error ni excepción en este punto, podemos afirmar que el connection string es válido y que el servidor SQL está disponible y funcionando correctamente.

---

### 👨‍💻 Operaciones básicas de persistencia y consulta

Para validar la conexión y el funcionamiento de EFCore, realizamos operaciones como agregar entidades y consultarlas:

```csharp
var newUser = new User
{
    Name = "something",
    Book = new("book")
};
context.Users.Add(newUser);
context.SaveChanges();

var users = context.Users.ToList();
Console.WriteLine(users);
```

- Creamos una instancia de `User` y la asociamos a una instancia de `Book`.
- Usamos `Add` para agregar el usuario al contexto.
- `SaveChanges` guarda los cambios en la base de datos.
- Luego, obtenemos todos los usuarios con `ToList` y los mostramos en la consola.

---

## 🧩 Breakdown de operaciones en EFCore

**¿Cómo gestiona EFCore la manipulación de datos?**

Cuando accedemos al contexto (`TestDbContext`), manipulamos las tablas mediante las propiedades `DbSet` definidas en el contexto. Por ejemplo, `context.Users` representa la tabla `Users`.

Al llamar a métodos como `Add`, `Update` o `Delete` sobre una entidad, **EFCore no ejecuta ninguna query inmediatamente**. Estas operaciones solo marcan los cambios en el contexto, permitiendo agrupar múltiples acciones para ejecutarlas en una sola interacción con la base de datos, lo que mejora la performance.

Ejemplo:
```csharp
context.Table1.Add(entity1);
context.Table2.Add(entity2);
context.Table3.Add(entity3);

context.SaveChanges();
```

Solo al invocar `SaveChanges`, EFCore ejecuta todas las queries necesarias (INSERT, UPDATE, DELETE) en una sola transacción.

Esta estrategia es especialmente útil cuando queremos realizar múltiples operaciones en diferentes tablas, asegurando que la base de datos solo se impacte una vez, lo que minimiza el consumo de recursos y optimiza la eficiencia de la aplicación.

---

### 📤 Persistencia y consultas

Para indicar que los cambios deben guardarse en la base de datos, usamos:

```csharp
context.SaveChanges();
```

En ese momento, EFCore genera y ejecuta las queries correspondientes, que pueden ser visualizadas en la consola si el logging está activado.

> 💡 **NOTA:**  
> En este ejemplo, al agregar un usuario con su libro, se realiza una agregación en cascada. El libro es requerido para el usuario, y la relación queda reflejada en la base de datos como una Foreign Key (`BookId`) en la tabla `Users`.

Para obtener entidades, simplemente acceder a `context.Users` no ejecuta ninguna consulta; es necesario utilizar métodos como `ToList`, `First`, o `FirstOrDefault` para que EFCore realice la consulta SQL (`SELECT`) correspondiente.

---

## 📝 Breakdown de entidades

### 👤 Entidad User

```csharp
public sealed record class User()
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; set; } = null!;
    public Guid BookId { get; set; }
    public Book Book { get; set; } = null!;
    public User(string name) : this() { Name = name; }
}
```

- `sealed`: Indica que la clase no puede tener clases derivadas, lo que protege el modelo.
- `record class`: Permite comparar instancias por sus valores en vez de por referencia en memoria. Además, no es inmutable, permitiendo modificar su estado.
- `Id { get; init; } = Guid.NewGuid();`: Inicializa la Primary Key (PK) con un valor único cada vez que se crea un usuario.
- `null!`: Indica que la propiedad será inicializada antes de usarse, evitando advertencias de nulabilidad.
- Constructor primario (`User()`): En C# 12, permite inicializar la clase de forma más simple y directa.
- Relación con `Book`: Al incluir una propiedad de tipo `Book` y un campo `BookId`, EFCore detecta la relación y crea la Foreign Key correspondiente en la base de datos.

### 📚 Entidad Book

```csharp
public sealed record class Book()
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; set; } = null!;
    public Book(string name) : this() { Name = name; }
}
```

- Ambas entidades tienen una propiedad `Id`, que EFCore interpreta automáticamente como Primary Key.
- Es recomendable que toda entidad persistida tenga una PK única para facilitar su acceso y manipulación.
- El tipo `Guid` es ideal para PK porque garantiza unicidad global, mejora la seguridad (difícil de adivinar) y permite escalabilidad en aplicaciones distribuidas.

---

## 🏁 Cierre de la conexión

```csharp
context.Dispose();
```

Cuando la conexión es abierta manualmente, es recomendable cerrarla explícitamente usando `Dispose` para liberar recursos y evitar posibles problemas de concurrencia o consumo innecesario.

---

## 🧑‍🏫 Ejercicios prácticos

### ✏️ Ejercicio 1

Modifica el código existente para **crear un usuario y asociarlo a un libro ya existente en la base de datos**.  
Debes validar que el libro existe (por su ID o nombre), pero **no debes obtener el libro completo**.

---

### ✏️ Ejercicio 2

Modifica el código para **crear un libro de forma independiente de la creación de un usuario**.  
Luego, crea un usuario que referencie ese nuevo libro.  
Ambas operaciones deben realizarse en una sola transacción, es decir, deben impactar a la base de datos juntas cuando se llame a `SaveChanges`.

---

## 📚 Notas adicionales y recomendaciones

> 📢 **NOTA:**  
> En este ejemplo, la agregación es en cascada: se agregan usuarios junto con su libro, porque el libro es requerido para la creación del usuario.  
> Esto implica que en la tabla `Users`, la columna `BookId` (FK) no puede ser nula o vacía, ya que debe referenciar a una fila existente en `Books`.

---

## 🌟 ¡Experimenta, aprende y pregunta! 😃

Modifica el código, realiza pruebas, observa los logs y aprende cómo funcionan las operaciones en EFCore y cómo se relacionan las entidades en la base de datos.  
Si tienes dudas, consulta la documentación oficial o pregunta en la comunidad.
