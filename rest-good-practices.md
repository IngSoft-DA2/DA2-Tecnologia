# Buenas practicas a seguir

Las marcadas con \* se deberan de intentar respetar siempre, el resto son reglas que se usan en la practica y es bueno tener conocimiento de las mismas.

## 1. Endpoint/Rescurso (\*)

Un endpoint es una URI (Uniform Resource Identifier) especificaen un web service o web API que es usado para acceder a un recurso particular o realizar una accion especifica. Cada endpoint corresponde a una funcionalidad provista por un web service o una web api, y es accedida por request http.

Estos tienen que seguir las siguientes reglas:

- **Sustantivos ante verbos**

  - ../dogs\_ → URI para manipular perros
  - .../users →\_ URI para manipular usuarios
  - .../sessions → URI para manipular sesiones

- **Intuitiva y simple**

- **Plural ante singular**

- **En minuscula**

- **Nombres concretos ante abstractos**

1. Bien:
   1. ../_dogs_
   2. ../_admins_
2. Evitar:
   1. ../_animals_
   2. ../_persons_

- **Recursos con relaciones**

1. Bien:
   1. ../owners/1/dogs
2. Evitar:
   1. ../_users/1/dogs/2_

- **Ocultar complejidad con '?'**

- **Verbos fuera de la URI**

1. Bien:
   1. ../_dogs_?_leashed=true_
   2. ../_dogs_
2. Evitar:
   1. ../_getAllLeashedDogs_
   2. ../_getHungerLevel_

- **Hasta 3 niveles**

## 2. Verbos HTTP (\*)

Existen varios verbos http, los mas comunes a usar y que representan bien las operaciones CRUD son: POST, GET, PUT, DELETE.

La aplicacion de los diferentes verbos ante el mismo endpoint, implican funciones distintas.

Los verbos se los suele agrupar por ser idempotentes o no. Un verbo es idempotente si el efecto de realizar una request es el mismo que realizar muchas requests identicas. Por ejemplo los verbos GET, PUT y DELETE son considerados idempotentes. Esto quiere decir que esos metodos se pueden ejecutar multiples veces sin causar efectos secundarios involuntarios.

El verbo DELETE es considerado idempotente dado que si un cliente ejecuta una request con este verbo, la primera request impacta en borrar el recurso en la persistencia respondiendo un 204. Luego las request con este verbo responderan 404 por no encontrar el recurso lo cual no afectaria el estado del servidor.

El verbo PUT es considerado tambien idempotente dado que si un cliente ejecuta una request con este verbo, el estado del servidor aplicara la modificacion del recurso independientemente al estado previo, es decir, realizar una request o multiple request con los mismos valores, el estado del servidor no sufre variaciones secundarias.

El verbo POST no es considerado idempotente dado que si un cliente ejecuta una request con este verbo, el estado del servidor se modifica y crea el recurso, pero realizar multiples request con la misma informacion impactara en duplicar la data en el estado del servidor implicando en tener inconsistencias en la persistencia.

## 3. Manejo de errores (\*)

Los mensajes de error deben proveer la informacion suficiente para poder resolver el error en una request posterior y sin exponer vulnerabilidades de seguridad.

Estos errores seran interpretados por personas con rol desarrollador y por sistemas. Es importante destacar quienes van a ver los mensajes para realizar un buen diseño de estos para que sea facil de interpretar el error.

Este aspecto es importante para una API ya que la implementacion es un elemento de caja negra para quienes consumen dicha API, es por eso que las respuestas exitos como las de error deben prover la informacion suficiente y consistente.

Un buen manejo de errores le permite a los desarrolladores seguir ciertas estrategias como **test-first** y **test-driven-development**.

Las siguientes reglas se deben de cumplir para tener un buen diseño:

- **Buen manejo de codigo de error**
  Los codigos de error correctos especifican el tipo de error ocurrido. Es por eso que existen diferentes familias de errores. Existen mas de [70 codigos HTTP](https://en.wikipedia.org/wiki/List_of_HTTP_status_codes), eso no quiere decir que una api web va a utilizar cada uno de ellos, lo ideal es utilizar un set de codigos de cada familia y los mas conocidos para poder interpretar el error (200-Ok, 201-Created, 204-NoContent, 400-BadRequest, 401-Unauthorized, 403-Forbidden, 404-NotFound, 409-Conflict, 500-InternalServer).
- **Consistencia de body**
  Una vez diseñado la estructura del error, se debera de cumplir independientemente del tipo de error.

### 4. Funcionalidades no relacionadas a recursos (\*)

Existen funsionalidades que no estan relacionadas a recursos. Por ejemplo, calculos financieros o traducciones de lenguaje. Estas funcionalidades responden a un resultado y no a un recurso. En estos casos se deberia de usar verbos y no sustantivos, pero el uso de estos verbos debe ser lo mas simple posible.

Por ejemplo un endpoint para convertir de una moneda a otra, se puede tener algo de la siguiente manera:

```
/convert?from=EUR&to=CNY&amount=100
```

Es importante que todos los endpoints sean documentados correctamente, en especial estos especificando sus parametros y comportamiento. Dado que no es estandard, un usuario de la API no sabra como se comporta facilmente.

## 5. Versionado

El versionado se refiere a la practica de manejar diferentes versions de endpoints para introducir cambios en el tiempo de forma segura siendo backward compatible para clientes existentes. Mientras que una API evoluciona, cambios son introducidos a bodies de requests y de responses, endpoints cambian, para evitar que los clientes se vean afectados por estos cambios, el versionado introduce un control de estos cambios permitiendole a los clientes no verse afectados por estos nuevos comportamientos.

Hay muchas formas de implementar versionado:

1. URI: la informacion de la version a utilizar es incluida en el endpoint. Por ejemplo: https://api.example.com/v1/resource, https://api.example.com/v2/resource.
2. QueryParams: la informacion de la version es incluida como query parameter en los endpoints. Por ejemplo: https://api.example.com/resource?version=1, https://api.example.com/resource?version=2.
3. Header: la informacion de la version es incluida en un header especifico.

La forma de versionar la api depende de factores como la complejidad de la API, el nivel de control necesario sobre el versionado y preferencias del equipo de desarrollo.

El versionado ayuda a mantener backward compatibility para clientes existentes, permite un rollout gradual de los cambios y provee una forma clara de interaccion con los cambios de la API con los clientes. Es escencial manejar el versionado de forma cuidados para evitar fragmentacion, confusion y un overhead de mantenimiento.

## 6. Respuestas parciales

Las respuestas parciales trata sobre retornar unicamente un set de properties solicitadas para la response, en vez de retornar todas las properties programadas a retornar. Esto puede ser una ventaja para aquellos clientes donde no necesiten todas las properties disponibles en la response o quieren reducri la cantidad de data que se manda sobre la network, esto es un incentivo para mejorar la performance y reducir la latencia.

La forma de implementar respuestas parciales puede variar entre las API, una forma de implementarlo puede ser con el uso de query parameters, donde se especifican que parametros se quieren incluir. Por ejemplo: https://api.example.com/resource?fields=id, firstname, lastname.

La implementacion de respuesta parcial requiere una consideracion sobre el diseño de la api y las consideraciones de los clientes. Puede mejorar la eficiencia del envio de la data y reducir procesamiento de data de forma innecesaria. Sin embargo, es escencial balancear los beneficios que trae con la complejidad que puede introducir a la implementacion. Adicionalmente, debe de existir una documentacion clara para los clientes para asegurar el uso correcto y eficiente del mismo.

## 7. Paginacion

La paginacion es la solucion para reducir un set largo de data a uno mas corto y manejable. Un objeto de paginacion es una mejora significante en la performance y reduccion de carga de datos innecesarios tanto para el servidor como para el cliente. Esto le permite al cliente obtener la data de forma incremental en vez de toda en una sola llamada. Esto es importante para aquellos datasets muy largos.

La forma de implementar paginacion es con query parameters y los parametros pueden variar entre las apis. Por ejemplo: https://api.example.com/resource?page=1&pageSize=10, https://api.example.com/resource?page=2&pageSize=10. Los parametros en el ejemplo son:

- page: para indicar el numero de pagina que se quiere obtener
- pageSize: la cantidad de elementos a incluir en la pagina.

Otros parametros podrian ser:https://api.example.com/resource?offset=1&limit=10, https://api.example.com/resource?offset=2&limit=10, siendo los parametros:

- offset
- limit

El calculo para incluir de forma correcta los datos involucrados en la pagina solicitada es el siguiente:

(page-1)\*pageSize = indice del primer elemento en la pagina

La implementacion en .NET es:

```C#
public Pagination<Entity> Pagination(int pageNumber, int pageSize)
{
    // TODO: chequear que ni pageNumber ni pageSize sean 0

    var elements = collection
    .Skip((pageNumber - 1) * pageSize)
    .Take(pageSize)
    .ToList();

    var totalElements = collection.Count;
    var amountOfPages = totalElements / pageSize;

    return new Pagination<Entity>(elements, amountOfPages, totalElements);
}
```

<p align="center">
[Paginacion]
</p>

Para asegurar consistencia de los datos en caso de querer filtrar y ordenar, se debera de implementar estas acciones de forma previa a la paginacion. La paginacion debera de ser el ultimo paso antes de obtener los datos.

```C#
public Pagination<Entity> Pagination(int pageNumber, int pageSize)
{
    // TODO: chequear que ni pageNumber ni pageSize sean 0

    var elements = collection
    .Where(e => true)
    .Skip((pageNumber - 1) * pageSize)
    .Take(pageSize)
    .ToList();

    var totalElements = collection.Count;
    var amountOfPages = totalElements / pageSize;

    return new Pagination<Entity>(elements, amountOfPages, totalElements);
}
```

<p align="center">
[Filtrado y paginacion]
</p>

```C#
public Pagination<Entity> Pagination(int pageNumber, int pageSize)
{
    // TODO: chequear que ni pageNumber ni pageSize sean 0

    var elements = collection
    .Where(e => e.Prop == prop)
    .OrderBy(e => e.Prop)
    .Skip((pageNumber - 1) * pageSize)
    .Take(pageSize)
    .ToList();

    var totalElements = collection.Count;
    var amountOfPages = totalElements / pageSize;

    return new Pagination<Entity>(elements, amountOfPages, totalElements);
}
```

<p align="center">
[Filtrado, orden y paginacion]
</p>

```C#
public Pagination<Entity> Pagination(int pageNumber, int pageSize)
{
    // TODO: chequear que ni pageNumber ni pageSize sean 0

    var elements = collection
    .Where(e => e.Prop == prop)
    .GroupBy(e => e.Prop)
    .OrderBy(e => e.Key)
    .Select(e => new {
      Prop = e.Key,
      Amount = e.Count
    })
    .Skip((pageNumber - 1) * pageSize)
    .Take(pageSize)
    .ToList();

    var totalElements = collection.Count;
    var amountOfPages = totalElements / pageSize;

    return new Pagination<Entity>(elements, amountOfPages, totalElements);
}
```

<p align="center">
[Filtrado, orden, agrupacion y paginacion]
</p>

Y se debera de incluir la siguiente informacion:

- Elementos
- Cantidad de paginas
- Cantidad total de elementos

La paginacion es escencial para mejorar la escalabilidad y performance de aquellas APIs que manejan un set largo de datos. Sin embargo, es crucial considerar factores como la consistencia de datos, estrategias de cacheo y la usabilidad para los clientes de la API. Adicionalmente, una clara documentacion y consistencia en la convension de los nombres de los parametros es escencial para asegurar que los clientes puedan interactuar con la API de forma efectiva.

# Lecturas recomendadas

- [Diseño de api](https://aulas.ort.edu.uy/pluginfile.php/441401/mod_resource/content/1/api-design-ebook-2012-03.pdf)
