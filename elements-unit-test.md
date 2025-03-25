[Volver - Pruebas Unitarias](https://github.com/IngSoft-DA2/DA2-Tecnologia/blob/unit-testing/README.md)

# Elementos en una clase de pruebas unitarias

Una clase de pruebas unitaria encapsula una configuración predeterminada para probar una unidad de código determinada. La misma prepara un ambiente y elementos a usar para que las diferentes pruebas unitarias puedan reutilizarlos, dejando a las mismas la responsabilidad de definir los diferentes escenarios de prueba.

Para que una clase sea determinada por el framework como una clase de pruebas unitarias, la misma deberá de ser `public` y contener el atributo `[TestClass]`. Estas condiciones de la clase le permiten al framework identificar las pruebas disponibles para ejecutarlas.

```C#
[TestClass]
public sealed class MovieServiceTests
{
  // unit tests
}
```

Esta clase de prueba, es un ejemplo de una clase que quiere probar el comportamiento público de la clase `MovieService`. Esta es una forma de organización de la prueba, tiene la ventaja y desventaja de que todo lo relacionado a este `service` está encapsulado en una clase sola. Independientemente de si la clase tiene muchos comportamientos a probar, esta clase de prueba definirá varios casos de uso haciendola una clase muy extensa en donde trabajar. Una forma de resolver esta problemática es hacer uso de las `regions`.

Otra opcion de marco de trabajo podría ser tener una clase de prueba por comportamiento a probar. Siguiendo este camino podría existir lo siguiente:

```C#
[TestClass]
public sealed class CreateMovieServiceTests
{
  // unit tests
}
```

Esta clase hace referencia a probar el comportamiento `Create` de la clase `MovieService`. Este camino hace que cada clase de prueba sea mas compacta pero incrementa en cantidad las clases de prueba a mantener.

## TestInitialize

Es el atributo que se le puede dar a un método que se ejecutará previamente a cada prueba individualmente. Este atributo nos permite definir un espacio para la inicialización de elementos previamente a cada prueba. Esto permite respetar la independencia entre las pruebas.

Dado que es un método con un atributo, en este no podremos inicializar los estados de la clase que hagan uso de la palabra clave `readonly`. Dichos estados deberán ser definidos en el constructor de la clase de prueba o en la misma línea de declaracion.

```C#
[TestClass]
public sealed class MovieServiceTests
{
  [TestInitialize]
  public void Initialize()
  {
    // initialize logic
  }
}
```

## TestCleanup

Es el atributo que se le puede dar a un método que se ejecutará posteriormente a cada prueba individualmente. Este atributo nos permite definir un espacio para borrar estado de la prueba o en otros elementos para respetar la independencia entre las pruebas.

```C#
[TestClass]
public sealed class MovieServiceTests
{
  // some code

  [TestCleanup]
  public void Cleanup()
  {
    // clean up logic
  }
}
```

## TestMethod
Es el atributo que se le da a un método de prueba. Sirve para que el framework identifique las pruebas a ejecutar. La visibilidad de dicha prueba debe de ser `public` y el retorno `void`.

```C#
[TestClass]
public sealed class MovieServiceTests
{
  // some code
  [TestMethod]
  public void MethotToTest_WhenConditionsOfTheTest_ShouldBehaviourExpected()
  {
    // Arrange
    // Act
    // Assert
  }
}
```
El nombre de la prueba se divide en tres secciones: `section1_section2_section3`. Las mismas se refieren lo siguiente:
- `section1`: nombre del método del objeto real que se quiere probar.
- `section2`: condiciones de la prueba, empieza la sección con la palabra `When` seguido de las condiciones.
- `section3`: resultados esperados en la prueba, empieza la sección con la palabra `Should` seguido del resultado.

Por ejemplo 
- El nombre de una prueba para crear un usuario con información correcta sería: `Create_WhenInfoIsCorrect_ShouldReturnNewId`
- El nombre de una prueba para crear un usuario y el email tiene formato inválido sería: `Create_WhenEmailFormatIsIncorrect_ShouldThrowEmailFormatException`

## DataRow
Existen algunas condiciones donde queremos ejecutar la misma prueba, es decir, la misma sección de `Arrange`, `Act` y `Assert` pero solo variando cierta data en el `Arrange`. Para evitar duplicar las pruebas, se puede utilizar el atributo `DataRow` para leer estos valores desde parámetros del método de prueba sin necesidad de hardcodearlos en la prueba misma. La forma de utilizar dicho atributo es la siguiente:
```C#
[TestMethod]
[DataRow("")
[DataRow(null)
public void MethodToTest_WhenConditionsOfTheTest_ShouldBehaviourExpected(string name)
{
  //Arrange
  //Act
  //Assert
}
```

## TestCategory
Tanto a las pruebas como a las clases que encapsulan pruebas las podemos agrupar dentro de una categoría para filtrar aquellas pruebas que queremos ejecutar. Para realizar esto usamos el atributo `TestCategory` indicando el nombre de la categoría, y estas las podemos filtrar en el explorador de pruebas de Visual Studio. Dicho atributo lo podemos usar de la siguiente manera:

```C#
[TestClass]
[TestCategory("Service")
[TestCategory("Movie")]
public sealed class MovieServiceTests
{
  //...
}
```
En este código se crearon dos categorías, la categoría `Service` para agrupar todas las pruebas relacionadas con la capa de aplicación y servicios, y la otra categoría `Movie` que es mas específica relacionado a la entidad `Movie`. Las mismas estan ordenadas de lo mas genérico (modular) a algo más específico (concreto).
