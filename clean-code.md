[Volver - Main](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/main?tab=readme-ov-file#repositorio-de-clase)

# Tips Clean Code y Buenas Practicas

Clean code es codigo que:

- Facil de leer
- Facil de mantener
- Facil de entender

Escribir clean code es una habilidad la cual se puede aprender.

## 1. Remplazar condiciones de if complejas por metodos descriptivos

Las condiciones complejas dentro de un if complejisan la lectura y el entendimiento de la condicion, cuanto mas condiciones abarca, mas dificil es de interpretar y de mantener.

La solucion es mover la condicion a un metodo o variable con un nombre descriptivo. El nombre debe de ser lo mas detallado posible.

Esta practiva una mejora significante en la lectura del codigo.

<p align="center">
<img src="./images/image.png">
</p>

<p align="center">
[If statement -> Method]
</p>

## 2. Juntar varias condiciones de ifs en uno

La idea principal es evitar muchos ifs anidados los culaes abren siempre caminos logicos. Lo ideal seria agruparlos en una unica condicion.

<p align="center">
<img src="./images/image-2.png">
</p>

<p align="center">
[Merge condiciones de ifs]
</p>

## 3. Usar nameof para convertir enum a string

La palabra clave `nameof` introducido en C# 6.0, es usado para obtener el nombre de una variable, tipo como string en tiempo de compilacion. Es util para evitar strings magicos en el codigo y mantener el codigo mas refactor-friendly.

`nameof` es la forma preferible para convertir un enum a string. Esto es porque `nameof` es evaluado en tiempo de compilacion y va a inyectar un string que nunca va a cambiar, mientras que el metodo `ToString` es evaluado en tiempo de ejecucion. Esto significa que `nameof` es mas eficiente y puede mejorar la performance del codigo.

<p align="center">
<img src="./images/image-3.png">
<p>

<p align="center">
[Nameof en vez de ToString]
</p>

## 4. Mapear una Request a una entidad

Es logico pensar que la request deberia de contener un metodo para pasarse a si mismo a otra estructura si pensamos en principios como encapsulamiento y SRP.

Algunos ventajas son:

- El mapeo de Request a otra entidad esta encapsulado
- Los controllers son mas limpios porque el mapeo esta en otro lado

Solo se tiene que hacer la llamada al metodo y se obtendra una entidad relevante.

```C#
public sealed record class CreateMovieRequest
{
  public string? Title  { get; init; }

  public string? Description { get; init; }

  public CreateMovieArgs Map()
  {
    return new CreateMovieArgs(
      Title,
      Description
    )
  }
}
```

```C#
[HttpPost]
public void Create(CreateMovieRequest request)
{
  var args = request.Map();
}
```

## 5. Evitar muchos argumentos en un metodo

Es una buena practica limitar el numero de argumentos en una funcion a dos. Si una funcion requiere mas de dos argumentos significa que esa funcion esta haciendo mucho mas de lo esperado y deberia de ser refactoriado.

Podemos refactorear la funcion usando un **struct** o un **record** para encapsular los parametros relacionados en vez de pasarlos de forma individual.

Las ventajas de esta practica son:

- Se mejora la lectura del codigo
- Es mas facil de mantener
- Simplifica las pruebas
- El codigo es mas flexible

Evitar muchos argumentos en una funcion es una practica importante para escribir codigo limpio y mantenible. Al usar objetos que encapsulan la data, podemos simplificar la firma de la funcion y hacer que el codigo sea mas leible y facil de testear.

<p align="center">
<img src="./images/image-4.png">
<p>

<p align="center">
[Agrupacion muchos argumentos]
</p>

## 6. Clausulas Guard

Una clausula guard prevee la completitud de una funcion al chequear valores invalidos.

Las clausulas guard mejoran la lectura y mantenibilidad del codigo. Nos permiten manejar las excepciones de forma mas segmantica.

Tambien ayudan a implementar la metodologia Fail-fast.

Una ventaja de estas clausulas es que minimizan codigo anidado y lo hace mas detallado.

Algunos buenos casos donde usarlo son:

- Validaciones de entrada
- Comprobacion de condiciones
- Comprobacion de null

<p align="center">
<img src="./images/image-5.png">
<p>

<p align="center">
[Guard clause]
</p>

## 7. MinBy y MaxBy

Antes de .NET 6 para obtener el minimo o el maximo, se tenia que hacer en dos pasos, ordenar la coleccion de forma ascendente o descendente y obtener el primer elemento.

Pero en .NET 6 se tiene las funciones linq MinBy y MaxBy.

<p align="center">
<img src="./images/image-6.png">
<p>

<p align="center">
[MinBy MaxBy]
</p>

## 7. LINQ en vertical no en horizontal

Escribir la concatenacion de funciones LINQ se deben de escribir de forma vertial para mejorar la lectura y hacer mas facil el entendimiento de los mismos.

Concatenar las funciones LINQ de forma horizontal dificulta en la lectura, la extension y mantenibilidad.

<p align="center">
<img src="./images/image-7.png">
<p>

<p align="center">
[Concatenacion de forma horizontal]
</p>

```C#
dbContext
  .Animals
  .Where(animal => animal.HasBigEars)
  .OrderBy(animal => animal.IsDangerous)
  .Select(animal => (animal.Id, animal.Name))
  .ToList();
```
<p align="center">
[Concatenacion de forma vertical]
</p>

## 8. Convencion de nombres de properties
A continuacion se muestra cual es la convencion en C# para nombrar properties segun la visibilidad de las mismas:
- Properties `public` tienen que seguir `PascalCase` -> `public string Name { get; set; }`
- Properties `private` y `protected` tienen que seguir `_camelCase` -> `private string _name { get; set; }`

## 9. Convencion de nombre de argumentos de una funcion
Por convencion en C# los argumentos tienen que seguir `camelCase`
```C#
public void MyFunction(string myArgument)
{
  // some logic
}
```

## 10. Convencion de nombre de clase
Por convencion en C# los nombres de las clases tienen que seguir `PascalCase`
```C#
public class MyClass
{
  // some code of class
}
```

## 11. Convencion de nombre de proyectos
Por convencion en C# los nombres de los proyectos tienen que seguir `PascalCase`. Es buena practica tambien poner como subfijo al nombre del proyecto el contexto/negocio en el cual esta situado. Esto ayuda a los desarrolladores entender la responsabilidad de dicho paquete dentro de la solucion.

Por ejemplo, si se esta trabajando en una solucion llamada `Vidly` y se tiene el proyecto para la web api, el mismo se llamaria: `Vidly.WebApi`, siendo la primera parte el contexto y la segunda el tipo de proyecto que es.

## 12. Hacer uso de LINQ para mapear
LINQ (Language Integrated Query) proporciona una sintaxis uniforme para consultar y manipular colecciones de datos. Si uno desea convertir una coleccion de datos a otra coleccion de datos, la forma mas practica es utilizando la funcion `ConvertAll` de LINQ.

```C#
// FORMA INCORRECTA
var responses = new List<Response>();
foreach(var entity in entities){
  var response = new Response(entity);
  responses.Add(response);
}
```
<p align="center">
  [Iteracion manual]
</p>

```C#
// FORMA CORRECTA
entities.ConvertAll(e => new Response(e));
```
<p align="center">
  [Iteracion con LINQ]
</p>

## 12. De string a DateTimeOffset
Cuando se desea convertir un string a un tipo de fecha (DateTimeOffset, DateTime, DateOnly, TimeOnly) es buena practica configurar el parseo indicando como debe ser el formato de esa fecha en string, la cultura a tener en cuenta, entre otros parametros. Es importante especificar estas configuraciones ya que por defecto los metodos `Parse` o `TryParse` configuraran estos aspectos tomando los valores del sistema operativo, lo cual puede causar comportamientos inesperados.

Por ejemplo si nuestra maquina usa el formato `dd-MM-yyyy`, tomara este formato como defecto para realizar la conversion. El codigo en esa maquina funcionara correctamente, pero solo funciona en ese ambiente, si ejecutamos el codigo en otra maquina donde el formato de la fecha es `MM-dd-yyyy` el codigo se comportara de forma diferente causando inconsistencias.

```C#
using System;

public DateTime Parse(string possibleDate)
{
  var isNotParsed = !DateTime.TryParse(possibleDate, out DateTime dateParsed);

  if(isNotParsed)
  {
    throw new ArgumentException("Parameter possibleDate is not a valid date");
  }

  return dateParsed;
}
```
<p align="center">
  [Evitar]
</p>

```C#
using System;
using System.Globalization;

public DateTime Parse(string possibleDate)
{
  var isNotParsed = !DateTime.TryParseExact(
    possibleDate,
    "dd-MM-yyyy",
    CultureInfo.InvariantCulture,
    DateTimeStyles.None,
    out DateTime dateParsed);

  if(isNotParsed)
  {
    throw new ArgumentException("Parameter possibleDate is not a valid date");
  }

  return dateParsed;
}
```
<p align="center">
  [Implementar]
</p>

## 13. No ser fuertemente tipado con string ('Stringly' typed)

Ser fuertemente tipado significa aplicar reglas de tipo mas estrictas para garantizar que los tipos de datos sean correctos y coherentes en toda la aplicacion.

El uso de enums en vez de strings es una practica comun para promoveer un codigo en C# fuertemente tipado.

Dado que:
1. Mejora la claridad y seguridad del codigo
2. Hay un chequeo en tiempo de compilacion
3. Nos ayuda IntelliSense y completitud automatica
4. Performance
5. Mas facil de mantener

Asi como se puede usar enums, se pueden usar clases staticas propias para preservar el valor en string si se desea.

```C#
public void DontDoThis(string employeeType)
{
  if(employeeType == "administrator")
  {
    // do something
  }
}
```
<p align="center">
  [Codigo stingly typed]
</p>

```C#
public void DoThis(Employee employee)
{
  if(employee.Type == Type.Administrator)
  {
    // do something
  }
}
```
<p align="center">
  [Codigo strongly typed]
</p>

## 14. Extensiones de metodos C# vs una libreria de mapeo
1. Simplicidad
   Codigo que necesitamos y solo que necesitamos. Las extensiones de metodos permiten un mapeo mas preciso sin una configuracion adicional.

2. Performance
  No hay reflection, no hay costos ocultos. Los mapeos directos aseguran una performance optima.

3. Leible
   El codigo cuenta una historia. Cuando alguien mas lee los mapeos, las extensiones de metodos pueden ser mas explicitos, eliminando insertidumbres de que se esta mapeando

4. Flexibilidad
   Es codigo propio. No estar limitados por las limitaciones de una libreria.

5. Debugging
   Situarse directamente en el codigo del mapeo. No hay necesidad de realizar debugs complejos en elementos internos de ninguna libreria.

Mientras que una libreria de mapeo es un increible recurso, a veces las soluciones mas straightforward pueden ofrecer una mayor claridad y eficiencia.

```C#
public List<MovieBasicInfoResponse> GetAll()
{
  var movies = _movieService.GetAll();

  return movies.ToResponse();
}
```

```C#
public static class MovieBasicInfoResponseMapping
{
  public static List<MovieBasicInfoResponse> ToResponse(this List<Movie> movies)
  {
    return movies.ConvertAll(m => new MovieBasicInfoResponse(m));
  }
}
```

## 15. Breaking changes
Un breaking change son por ejemplo:
- Eliminar o renombrar un endpoint de una API o parametros
- Cambiar el comportamiento de un endpoint existente en una API
- Cambiar los codigos de error de una API

ASP .NET Core hace facil la introduccion del versionado en una API.

```C#
[ApiController]
[ApiVersion(1)]
[ApiVersion(2)]
[Route("v{v:apiVersion}/movies")]
public sealed class MovieController(IMovieService movieService)
  : ControllerBase
{
  [MapToApiVersion(1)]
  [HttpGet]
  public List<MovieBasicInfoResponse> GetAllV1()
  {
    var movies = movieService.GetAll();

    return movies.ToResponse();
  }

  [MapToApiVersion(2)]
  [HttpGet]
  public List<MovieBasicInfoResponse> GetAllV2(
    [FromQuery] int? minStars,
    [FromQuery] int? maxStars)
  {
    var movies = movieService.GetAll(minStars, maxStars);

    return movies.ToResponse(); 
  }
}
```
## 16. Evitar la negacion
La negacion de una condicion de forma directa en un if o en en algun otro lado, suele a no ser tan visible la intencion por lo que puede llegar a generar confusiones a la hora de interpretar el codigo. Por eso siempre que se pueda evitar, las condiciones se tienen que situar en variables con un nombre nemotecnico o con un metodo.

```C#
if(!myVariable.HasValue)
{
  // do something
}
```
[Evitar preguntar en el if]

```C#
if(myVariable == null)
{
  // do something
}
```

## 17. readonly vs const
🔥 𝗰𝗼𝗻𝘀𝘁 y 𝗿𝗲𝗮𝗱𝗼𝗻𝗹𝘆 en 𝗖# comparten el objetivo en comun de prevenir que las variables sean modificadas despues de ser inicializadas, pero tienen algunas diferencias.

🔷 𝗰𝗼𝗻𝘀𝘁
- Son deifnidas en tiempo de compilacion, esto quiere decir, que su valor es sabido en tiempo de compilacion y no puede ser modificado en tiempo de ejecucion.
- Debe ser declarado con un inicializador y son implicitamente estaticos.
- Pueden ser usado solamente con tipos primitivos, integers, booleans, y strings. User-defined types, incluyendo classes, structs, y arrays, no pueden ser 𝗰𝗼𝗻𝘀𝘁.

🔷 𝗿𝗲𝗮𝗱𝗼𝗻𝗹𝘆 
- Son definidos en tiempos de ejecucion. Su valor puede ser asignado durante la inicializacion o en un constructor, pero no puede ser modificado luego.
- Pueden ser declarados con o sin un inicializador y pueden ser estaticos o no.
- Pueden ser usados con cualquier tipo de datos, incluyendo tipos de referencia.

## 18. Identidad de entidades
Toda aquella entidad que requiera una identidad, es buena practica que su primary key sea `Id`. Esto permitira la facil busqueda y rapida deteccion de estas entidades por esta propiedad.

```C#
public sealed record class User
{
  [Key]
  public string Email { get; init; }
}
```
<p align="center">
  [Marca como PK el email]
</p>

```C#
public sealed record class User
{
  public string Id { get; init; }

  public string Email { get; init; }
}
```

<p align="center">
  [La property con el nombre Id, ya es identificada como PK]
</p>

Esto hace mas visible identificar aquellas entidades con identidad para diferenciar cuales son `value-objects` y `reference-objects`.

## 19. El orden de los setup importa
A la hora de configurar los comportamientos de las dependencias en las pruebas unitarias, es importante definir un orden de configuracion para que puedan ser facilmente mantenibles, logren contar un cuento ordenado y que se relacione con la logica adyacente que se esta probando. Ese orden debe ser el orden de ejecucion, esto quiere decir que los setups de los mocks de las dependencias tienen que estar en el mismo orden de llamada en la logica que las utiliza.

Esto favorecera en encontrar mas facilmente y rapido los errores con dichas configuraciones.

```C#
public sealed class UserService(
  IRepository<User> userRepository,
  IPermissionService permissionService)
  : IUserService
{
  public List<User> GetAll(string userLoggedId)
  {
    permissionService.AssertHasPermission(PermissionKey.GetAllUsers, userLoggedId);

    var users = userRepository.GetAll();

    return users;
  }
}
```

```C#
// ------
[TestClass]
public sealed class UserServiceTests
{
  private readonly UserService _userService;
  private readonly Mock<IPermissionService> _permissionServiceMock;
  private readonly Mock<IRepository<User>> _userRepositoryMock;

  [TestInitialize]
  public void Initialize()
  {
    _permissionServiceMock = new Mock<IPermissionService>(MockBehavior.Strict);
    _userRepositoryMock = new Mock<IRepository<User>>(MockBehavior.Strict);

    _userService = new UserService(
      _userRepositoryMock.Object,
      _permissionServiceMock.Object);
  }
}

[TestMethod]
public void GetAll_WhenUserLoggedHasPermissionAndExistUsers_ShouldThrowException()
{
  var userLoggedId = Random.NextInt();

  var user = UserBuilder
    .Builder()
    .Build();

  _userRepositoryMock
  .Setup(i => i.GetAll())
  .Returns([user]);

  _permissionServiceMock
  .Setup(p => p.AssertHasPermission(PermissionKey.GetAllUsers, userLoggedId));

  var users = _userService.GetAll();

  users.Should().HaveCount(1);
  var userResult = users[0];
  userResult.Id.Should().Be(user.Id);
  userResult.Name.Should().Be(user.Name);
}
```
<p align="center">
  [Setup de mocks no siguen el orden de la logica]
</p>

```C#
[TestClass]
public sealed class UserServiceTests
{
  private readonly UserService _userService;
  private readonly Mock<IRepository<User>> _userRepositoryMock;
  private readonly Mock<IPermissionService> _permissionServiceMock;

  [TestInitialize]
  public void Initialize()
  {
    _userRepositoryMock = new Mock<IRepository<User>>(MockBehavior.Strict);
    _permissionServiceMock = new Mock<IPermissionService>(MockBehavior.Strict);

    _userService = new UserService(
      _userRepositoryMock.Object,
      _permissionServiceMock.Object);
  }
}

[TestMethod]
public void GetAll_WhenUserLoggedHasPermissionAndExistUsers_ShouldThrowException()
{
  var userLoggedId = Random.NextInt();

  var user = UserBuilder
    .Builder()
    .Build();

  _permissionServiceMock
  .Setup(p => p.AssertHasPermission(PermissionKey.GetAllUsers, userLoggedId));

  _userRepositoryMock
  .Setup(i => i.GetAll())
  .Returns([user]);

  var users = _userService.GetAll();

  users.Should().HaveCount(1);
  var userResult = users[0];
  userResult.Id.Should().Be(user.Id);
  userResult.Name.Should().Be(user.Name);
}
```
<p align="center">
  [Setup de mocks siguen el orden de la logica]
</p>

## 20. La organizacion de la capa de aplicacion deberia de evidenciar el negocio
La capa de aplicacion es donde se encuentra el core de nuestra aplicacion y de una Clean Architecture. Es donde se definen las entidades y la logica de negocio mas importante.

Es por esto que la organizacion no se deberia de centrar en conceptos tecnicos en vez de funcionalidades. Los sintomas que evidencian esto son carpetas con los siguientes nombres:

- Entities
- Enumerations
- Exceptions
- Repositories
- ValueObjects

Cual es el problema con la agrupacion por tipos?
Esta organizacion no evidencia nada sobre el negocio. Tambien involucra poca cohesion en cada carpeta, ya que los elementos involucrados no se relacionan entre ellos.

Para resolver esto hay que reorganizar la estructura involucrando en la misma carpeta conceptos relacionados.

Los beneficios de este enfoque son:

- Mejorar la cohesion
- Bajo acoplamiento entre carpetas no relacionadas

  ```
  |
  |--Core
  |--Entities
  |--Enumerations
  |--Exceptions
  |--Repositories
  |--Services
  |--ValueObjects
  ```
  <p align="center">
    [Agrupacion por tipo]
  </p>

```
|
|--Events
|--Firendships
|  |
|  |--Friendship.cs
|  |--FriendshipRequest.cs
|  |--FriendshipService.cs
|  |--IFriendshipRepository.cs
|--Invitations
|--Users
```
<p align="center">
  [Agrupacion por negocio]
</p>

## 21. Mejorando la performance con una llamada a la base
Cada llamada al metodo `SaveChanges` significa una ida a la base de datos. Eso quiere decir que si tenemos la llamada al `SaveChanges` dentro de un loop estaremos impactando en la base constantemente hasta que el loop termine. Para mejorar esto, lo correcto es hacer que EFCore traquee las entidades en memoria y una vez terminado, se impacte una unica vez a la base.

```C#
using var context = new ApplicationDbContext();

var users = GetUsers();

users.ForEach(user => {
  context.Users.Add(user);

  context.SaveChanges();
});
```
<p align="center">
  [EFCore inmediatamente que trackea un usuario (cuando se llama a context.Users.Add) tambien se impacta en la bd]
</p>

```C#
using var context = new ApplicationDbContext();

var users = GetUsers();

users.ForEach(user => {
  context.Users.Add(user);
});

context.SaveChanges();
```

<p align="center">
  [EFCore trackea todos los usuarios y luego impacta en la bd]
</p>
