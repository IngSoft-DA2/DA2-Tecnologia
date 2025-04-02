# Elementos de un controller

## Atributo ApiController

Es un atributo especial para marcar una clase como **controller**. Este atributo es típicamente aplicado a clases que heredan de **ControllerBase**, esto indicaría que la clase es un **API controller**.

Este atributo afecta de la siguiente manera en las clases:

- **Respuesta automática con código 400:** .NET Core automáticamente genera validaciones en los modelos. Esto quiere decir, que si la serialización de la request a un modelo falla, el framework retorna una respuesta 400 con detalles sobre el error.

- **Bindeo automático de la información:** No es necesario especificar explícitamente desde dónde se tiene que sacar la información para la serialización en los parámetros de las funciones. Esto quiere decir que no es necesario el uso de atributos como [FromBody], [FromQuery], el framework infiere desde dónde proviene la data.

- **Respuesta automática para errores no cacheados:** Cuando una excepción ocurre durante el procesamiento de la request, este atributo, genera automáticamente una respuesta con código de error 500 con los detalles de la excepción.

- **Formato de errores:** Las respuestas de error son retornadas en el formato especificado (JSON, XML) según el valor en el header Accept.

- **Respuestas 204 automáticas para resultados vacíos:** Si una función retorna null, el atributo automáticamente lo convierte a una respuesta 204 (NoContent), esto simplifica el manejo de escenarios donde la api puede o no incluir contenido en la respuesta.

En resumen este atributo simplifica un montón de tareas relacionadas con la api, haciendo más fácil la creación de una api y permitir una consistencia. De igual manera que provee comportamientos por defecto, uno puede customizar estos para ciertos aspectos específicos para nuestra api en particular.

## Atributo Route

Es usado para definir el endpoint a nivel de clase como base o a nivel de función como individual en el controller. Le permite a los desarrolladores especificar cuál va a ser la URI que corresponde a un controller en particular o una funcionalidad.

Este atributo afecta de la siguiente manera en las clases o funciones:

- **Enrutamiento a nivel de clase:** Cuando se aplica a nivel de clase, se define una ruta base que aplica para todas las funciones internas del controller. Esta ruta base se combina con rutas específicas definidas a nivel de funciones.

- **Enrutamiento a nivel de función:** Cuando se aplica a nivel de función, se define un nuevo nivel a la ruta base del controller.

- **Soporta parámetros:** A las rutas se le pueden proporcionar parámetros para que los niveles sean dinámicos y no estáticos. Para lograr esto se utilizan las llaves ({parameter}) indicando el nombre del parámetro a utilizar en el código. Los valores son extraídos de la request y a nivel de código se usan las variables.

- **Condiciones:** Adicionalmente, los parámetros en la ruta pueden incluir restricciones a los valores que se proporcionen. Estas restricciones son especificadas usando una sintaxis de una línea, permitiendo la validación de tipos, regular expressions o lógica a los valores de los parámetros.

En resumen este atributo juega un rol principal en definir los endpoints permitiendole al desarrollador mapear request http a un controller o función específico. Provee una flexibilidad y control sobre la estructura de los endpoints, haciéndolo más fácil de diseñar, limpio y más intuitivo.

Una de las grandes ventajas en separar en diferentes controllers y las rutas de esta manera es que todos los endpoints que atienden un recurso están encapsulados en una misma clase. En cuestión de organización también es una ventaja esta encapsulación.


```C#
[ApiController]
[Route("movies")]
public sealed class MovieController
    : ControllerBase
{
    //código del controlador
}
```


## Atributos HttpPost, HttpGet, HttpPut, HttpDelete

Estos atributos son usados a nivel de funciones para especificar que la función es la encargada de procesar la request HTTP. La combinación de verbo HTTP + endpoint da como resultado una única función. Esto quiere decir que esta combinación no puede estar ligada a otra función al mismo tiempo, independientemente de si están en el mismo controller o no. En caso de que exista esta situación, .NET Core responderá con un error de ambigüedad, ya que no sabrá a qué función enrutar la request HTTP.

## Atributos binding

Son los atributos para especificarle al framework donde está la data.

- **FromHeader:** la data está en el header
- **FromRoute:** la data está en el endpoint
- **FromQuery:** la data está en los query params
- **FromBody:** la data está en el body


```C#
[HttpPut("{movieId}")]
public IActionResult UpdateMovie(string movieId, [FromBody] MovieUpdateRequest request)
{
 //Código del método       
}
```

# Lecturas recomendadas
- [Model binding](https://learn.microsoft.com/en-us/aspnet/core/mvc/models/model-binding?view=aspnetcore-3.1)
