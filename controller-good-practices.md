# Buenas prácticas

## Route a nivel de clase
El uso del atributo [Route] debería ser a nivel de clase y sin el uso del placeholder. Cuando se crea un controller se crea de la siguiente manera: [Route("[controller]")] el cual se debería de modificar de la siguiente forma [Route("resource")] siendo "resource" el recurso que el controller estará atendiendo. Este resource debe ser completamente en minúscula y en plural.

## Niveles extra
Para especificar niveles extra al base, se deberá de hacer en los atributos de los verbos http. Por ejemplo: ``[HttpGet("{id}")]`` o ``[HttpGet("users")]``, el primer ejemplo, indica un nivel adicional dinámico, y el segundo ejemplo, indica un nivel adicional estático. Podríamos mezclar estos ejemplos para alcanzar un tercer nivel, por ejemplo en un controlador con ruta ``movies`` podría tener: ``[HttpGet("{moiveId}/actors")]``.

## Controller sealed
Los controllers específicos de recursos son clases que no se reúsan lógica, no se deberían de poder heredar entre ellos.

## Estado privado
El estado de los controllers no debería de ser algo que en la api se quiera utilizar, son puertas al mundo solamente. Para evitar complejidad en el uso, los estados que guarden deben de ser privados y readonly.

## Manejar un recurso a la vez
Los controllers no deben de manejar múltiples recursos al mismo tiempo. Solo pueden estar ligados a un recurso y a una lógica del negocio. Esto limita la complejidad y el mantenimiento de los mismos.

## Retorno de respuestas exitosas automáticas
Dado que el manejo de respuestas exitosas es automática, las funciones dentro de los controllers deberán de retornar objetos que no sean del tipo IActionResult o ActionResult.

## Inexistencia de try y catch
El manejo de error deberá ser de forma global, esto implica que no se deberá de tener bloques de try y catch en las funciones de los controllers.

## Extensión de las funciones
Los controllers como son un enrutamiento a la lógica de negocio correspondiente, estas no deberán de contener ningún tipo de lógica, solo la llamada correspondiente a la lógica encargada de atender dicha solicitud. Esto porque la capa de api es de bajo nivel (tecnología), esta podría ser cambiada por otra y si contiene lógica de negocio, se perdería, o se duplicaría.

## Funciones públicas
Para que se pueda hacer el enrutamiento de la request http a la función correspondiente, estos deben ser públicos para ser invocados.

## DTOs

Los Data Transfer Objects son estructuras definidas utilizadas en el envío de información entre capas. Estas estructuras son muy útiles para definir qué información es requerida a enviar a una capa adyacente. 

Los objetos de estas estructuras se categorizan por ser dummy, ya que lo único que guardan en su interior es puramente estado y no comportamiento. Dado esta característica, es muy común encontrarlos definidos como ``struct`` o ``sealed record class``, dependiendo que se quiera hacer con ellos.

Para una web-api los tipos de los estados de los dtos, deben de ser primitivos para poder tener un control sobre la serialización a tipos específicos de nuestro dominio. Por ejemplo, si se quiere exponer una fecha, la misma en el dto deberá de ser string, y con una lógica interna se parseará al tipo de fecha correspondiente teniendo cuenta el formato y el caracter separador.

Una regla importante entre los dtos es intentar de que las dependencias sean con otros dtos, así la evolución de las diferentes estructuras manejadas se puede hacer de forma independiente y a su propio ritmo.