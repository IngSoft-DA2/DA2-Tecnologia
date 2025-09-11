[⬅️ Volver - Pruebas Unitarias](https://github.com/IngSoft-DA2/DA2-Tecnologia/blob/unit-testing/README.md)

# 🏗️ Elementos en una Clase de Pruebas Unitarias

Una clase de pruebas unitaria encapsula una configuración predeterminada para probar una unidad de código determinada.  
Prepara el ambiente y los elementos necesarios para que las diferentes pruebas sean consistentes, aisladas y fácilmente mantenibles.

---

## 🏷️ Estructura Básica de una Clase de Prueba

Para que el framework identifique tu clase como una clase de pruebas unitarias:

- Debe ser **`public`**
- Debe tener el atributo **`[TestClass]`**

```csharp
[TestClass]
public sealed class MovieServiceTests
{
  // unit tests
}
```

Esta clase es un ejemplo para probar el comportamiento público de la clase `MovieService`.  
> 📝 **Tip:** Puedes organizar tus pruebas agrupando por clase del sistema o por comportamiento específico.

### Ejemplo: Clase separada para un comportamiento

```csharp
[TestClass]
public sealed class CreateMovieServiceTests
{
  // unit tests
}
```
Esto hace que cada clase de prueba sea más compacta, pero puede incrementar la cantidad de archivos de prueba.

---

## 🛠️ [TestInitialize]

Usa el atributo **`[TestInitialize]`** en un método para ejecutar lógica de inicialización antes de cada prueba individual:

```csharp
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
> ⚠️ No se puede inicializar campos `readonly` en este método; deben ser definidos en el constructor.

---

## 🧹 [TestCleanup]

El atributo **`[TestCleanup]`** marca un método que se ejecuta después de cada prueba, útil para limpiar el estado o liberar recursos.

```csharp
[TestClass]
public sealed class MovieServiceTests
{
  [TestCleanup]
  public void Cleanup()
  {
    // clean up logic
  }
}
```

---

## 🧪 [TestMethod]

Marca a un método como una prueba unitaria.  
Debe ser **`public`** y retornar **`void`**.

```csharp
[TestClass]
public sealed class MovieServiceTests
{
  [TestMethod]
  public void MethodToTest_WhenConditionsOfTheTest_ShouldBehaviourExpected()
  {
    // Arrange
    // Act
    // Assert
  }
}
```

### 📝 Convención de nombres

`MetodoAProbar_WhenCondicionesDeLaPrueba_ShouldResultadoEsperado`

- **section1**: nombre del método del objeto real a probar
- **section2**: condiciones de la prueba (empieza con `When`)
- **section3**: resultado esperado (empieza con `Should`)

**Ejemplo:**  
- `Create_WhenInfoIsCorrect_ShouldReturnNewId`
- `Create_WhenEmailFormatIsIncorrect_ShouldThrowEmailFormatException`

---

## 📦 [DataRow]

Permite ejecutar la misma prueba con diferentes datos, evitando duplicación de código.

```csharp
[TestMethod]
[DataRow("")]
[DataRow(null)]
public void MethodToTest_WhenConditionsOfTheTest_ShouldBehaviourExpected(string name)
{
  // Arrange
  // Act
  // Assert
}
```

---

## 🏷️ [TestCategory]

Agrupa pruebas o clases bajo una categoría, facilitando el filtrado y la ejecución selectiva.

```csharp
[TestClass]
[TestCategory("Service")]
[TestCategory("Movie")]
public sealed class MovieServiceTests
{
  //...
}
```
- **`Service`**: agrupa pruebas de la capa de servicios/aplicación
- **`Movie`**: categoría más específica

---

> ✅ **Resumen:**  
> Aprovecha estos atributos y convenciones para escribir clases de pruebas ordenadas, legibles y mantenibles.  
> ¡Un buen diseño de pruebas acelera el desarrollo y asegura la calidad del software! 🧑‍💻🧪
