[📍 ¿Qué es un Endpoint?](https://github.com/IngSoft-DA2/DA2-Tecnologia/blob/web-api/web-api.md#-qu%C3%A9-es-un-endpoint) → [🔙 Indice](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/web-api?tab=readme-ov-file#indice) → [🏠 Main](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/main?tab=readme-ov-file#da2-tecnologia--dise%C3%B1o-de-aplicaciones-2)

# 🌟 Buenas Prácticas REST para el Diseño de APIs

Las prácticas marcadas con un asterisco (\*) son esenciales y siempre deben respetarse. El resto son recomendaciones útiles para lograr APIs más intuitivas, robustas y fáciles de mantener.

---

## 1️⃣ Endpoint/Recurso (\*)

🔗 Un **endpoint** es una dirección única dentro de una API utilizada para acceder a un recurso o ejecutar una acción específica. Técnicamente, un endpoint se identifica mediante una **URI** (Uniform Resource Identifier), que es una cadena que permite identificar un recurso en Internet.  
Dentro de las URIs existen dos conceptos: **URL** (Uniform Resource Locator) y **URN** (Uniform Resource Name):

- **URI**: Es un identificador genérico para cualquier recurso en la web.  
- **URL**: Es un tipo de URI que, además de identificar el recurso, indica cómo acceder a él (es decir, incluye el esquema/protocolo, como http, https, ftp, etc., además de la dirección concreta del recurso).  
- **URN**: Es otro tipo de URI que identifica un recurso por nombre dentro de un espacio de nombres, pero no necesariamente indica cómo localizarlo.

En la práctica, cuando hablamos de endpoints en APIs REST, generalmente nos referimos a **URLs** (por ejemplo, `https://api.misitio.com/users/1`), ya que especifican tanto la ubicación como el método de acceso al recurso.

### 📝 Reglas clave:
- **📦 Usa sustantivos, no verbos:**  
  - ✅ `/dogs`, `/users`, `/sessions`  
  - ❌ `/getAllLeashedDogs`, `/getHungerLevel`
- **🔤 Nombres en plural y minúscula:**  
  - ✅ `/admins`, `/dogs`  
  - ❌ `/persons`, `/animals`
- **🪝 Relaciona recursos con jerarquía:**  
  - ✅ `/owners/1/dogs`  
  - ❌ `/users/1/dogs/2`
- **❓ Oculta la complejidad usando query params:**  
  - ✅ `/dogs?leashed=true`
- **🚫 No uses verbos en la URI:**  
  - ❌ `/getAllLeashedDogs`
- **📏 Limita a 3 niveles de profundidad:**  
  - ✅ `/owners/1/dogs/5`
  - ❌ `/a/b/c/d/e`

> Es recomendable mantener las URIs (y por ende las URLs) intuitivas, simples, concretas y evitar nombres abstractos.

### 💡 Ejemplo de implementación:
```http
GET /users/42/dogs?leashed=true
```
Obtiene todos los perros con correa del usuario 42.

---

## 2️⃣ Verbos HTTP (\*)

REST utiliza los verbos HTTP para definir la acción sobre el recurso. Los principales son:

- **GET:** Obtener recursos (idempotente)
- **POST:** Crear recursos (no idempotente)
- **PUT:** Reemplazar recursos (idempotente)
- **PATCH:** Modificar parcialmente recursos (no siempre idempotente)
- **DELETE:** Eliminar recursos (idempotente en la mayoría de los casos, pero puede variar)

---

### ⚡ ¿Qué es la idempotencia y por qué es importante?

La idempotencia es una propiedad clave en las APIs REST y se refiere a que el resultado de ejecutar una operación una o varias veces es el mismo:  
> Un verbo es idempotente si el efecto de realizar una request es el mismo que realizar muchas requests idénticas.  
> Esto permite que los clientes puedan repetir una petición sin temor a producir efectos secundarios no deseados, lo cual resulta fundamental en entornos donde pueden ocurrir problemas de red, timeouts o reintentos automáticos.

#### 🔹 Ejemplos prácticos de idempotencia en HTTP:

- **GET, PUT y DELETE** son considerados idempotentes en la mayoría de las implementaciones:
  - **GET**: Leer un recurso varias veces no cambia el estado del servidor.
  - **PUT**: Si envías varias veces la misma actualización, el recurso queda en el mismo estado.

- **DELETE**:  
  Generalmente se considera idempotente, pero esto depende de la implementación:
  - Si el DELETE realiza un "soft delete" (por ejemplo, marca el recurso como borrado), entonces repetir la operación no cambiará el estado tras la primera vez, y la respuesta podría ser 204 (No Content) o 404 (Not Found) sin afectar el estado interno.  
  - Si el DELETE realiza un "hard delete" y realmente elimina el recurso de forma irreversible, entonces múltiples DELETE pueden no ser idempotentes si la implementación arroja un error o un estado inconsistente tras la primera eliminación.  
  - Por esto, la idempotencia de DELETE debe analizarse según la lógica de borrado implementada.

- **PUT**: También es idempotente, ya que aplicar la misma modificación varias veces deja el estado del recurso igual: una o varias requests con los mismos valores no generan variaciones secundarias.

- **POST**: No es idempotente. Realizar múltiples requests POST con la misma información creará múltiples instancias del recurso, lo que puede llevar a duplicidad de datos e inconsistencias en la persistencia.

#### 🔸 ¿Por qué es importante la idempotencia?
- Permite a los clientes reintentar operaciones seguras ante fallos de red sin temor a cambiar el estado del sistema involuntariamente.
- Facilita la confiabilidad, robustez y predictibilidad de las aplicaciones distribuidas.
- Reduce riesgos de corrupción o duplicidad de datos frente a reintentos automáticos o usuarios impacientes.
- Es una base para implementar mecanismos de retry automáticos y sistemas tolerantes a fallos.

### 💡 Ejemplo de uso:
```http
POST /products
{
  "nombre": "Teclado Mecánico"
}
```
Crea un nuevo producto.  
```http
DELETE /products/7
```
Elimina el producto con ID 7.

---

## 3️⃣ Manejo de errores (\*)

Los mensajes de error deben ser claros, útiles y consistentes, sin revelar información sensible.

✨ **Buenas prácticas y consideraciones clave:**

- Los mensajes de error deben proveer la información suficiente para que el error pueda ser resuelto en una request posterior, pero sin exponer vulnerabilidades de seguridad.
- Estos errores serán interpretados tanto por personas con rol desarrollador como por sistemas automáticos. Es importante pensar en quién recibirá el mensaje para diseñarlo de forma clara y fácil de interpretar.
- La API es una caja negra para los consumidores, por lo que tanto respuestas exitosas como de error deben ser informativas y consistentes.
- Un buen manejo de errores permite adoptar metodologías como **test-first** y **test-driven development**, facilitando el desarrollo robusto.
- Es fundamental cumplir ciertas reglas para lograr un diseño de errores efectivo:

  - **🔢 Uso correcto de códigos de estado HTTP:**  
    Utiliza códigos que representen claramente el tipo de error ocurrido. Existen más de [70 códigos HTTP](https://en.wikipedia.org/wiki/List_of_HTTP_status_codes), pero no es necesario usarlos todos. Lo ideal es seleccionar un set representativo de cada familia y los más conocidos, como:
    - 200 OK  
    - 201 Created  
    - 204 No Content  
    - 400 Bad Request  
    - 401 Unauthorized  
    - 403 Forbidden  
    - 404 Not Found  
    - 409 Conflict  
    - 500 Internal Server Error

  - **📦 Estructura de error consistente:**  
    Una vez definida la estructura del error, debe mantenerse igual para todos los errores, facilitando el manejo en los clientes. Incluye siempre campos como `code`, `message` y, si es necesario, `details`.

### 💡 Ejemplo de respuesta de error:
```json
{
  "code": 404,
  "message": "Usuario no encontrado",
  "details": "No existe un usuario con el ID provisto."
}
```

---

## 4️⃣ Funcionalidades no relacionadas a recursos (\*)

🔢🌐 En ocasiones, existen funcionalidades en una API que no están asociadas directamente a un recurso, sino que representan una acción o cálculo sobre datos. Ejemplos de esto pueden ser conversiones de moneda, cálculos financieros, traducciones de lenguaje, entre otros.

Estas funcionalidades, a diferencia de los recursos tradicionales, responden con un **resultado** y no con un recurso propiamente dicho.  
En estos casos, es válido utilizar **verbos** en la URI (por ejemplo: `/convert`, `/translate`), pero se recomienda que estos verbos sean lo más simples, claros y directos posible, evitando frases largas o descripciones complejas.

### 💡 Ejemplo:
```http
GET /convert?from=EUR&to=CNY&amount=100
```
Convierte 100 euros a yuanes.

> 📑 Es fundamental documentar estos endpoints detalladamente, explicando qué parámetros aceptan y cuál es el resultado esperado, ya que, al no ser estándar, los usuarios de la API pueden no saber cómo utilizarlos correctamente.

---

## 5️⃣ Versionado

El **versionado** permite cambiar la API sin afectar a los clientes existentes.

El versionado es la práctica de gestionar distintas versiones de los endpoints de una API para poder introducir cambios de manera segura, manteniendo la compatibilidad hacia atrás (backward compatibility) para los clientes que ya usan la API. A medida que la API evoluciona, pueden realizarse cambios en los cuerpos de las requests y responses, así como en los propios endpoints.  
Para evitar que los clientes existentes se vean afectados por estos cambios, el versionado introduce un control explícito sobre ellos, permitiendo que los clientes sigan funcionando correctamente sin verse impactados por los nuevos comportamientos.

Además, el versionado facilita una transición gradual hacia nuevas versiones, permitiendo un rollout progresivo de los cambios. Esto ofrece una forma clara de interacción entre los cambios de la API y los clientes, brindando estabilidad y previsibilidad.  
Es esencial manejar el versionado con cuidado para evitar problemas como la fragmentación de la API, confusiones en el uso y un aumento innecesario en los esfuerzos de mantenimiento.

### 🔢 Formas de versionar:
- En la URI: `/v1/products`
- En query params: `/products?version=1`
- En headers: `Accept: application/vnd.example.v1+json`

### 💡 Ejemplo:
```http
GET /v2/products
```
Obtiene productos usando la versión 2 de la API.

---

## 6️⃣ Respuestas parciales

Permite que el cliente solicite solo ciertos campos del recurso para optimizar el tráfico y el procesamiento. Esto puede ser una ventaja para mejorar la eficiencia del envío de datos y reducir procesamiento tanto del lado del servidor como del cliente.

Las respuestas parciales tratan sobre retornar únicamente un set de propiedades solicitadas para la response, en vez de retornar todas las propiedades programadas a retornar.  
La forma de implementar respuestas parciales puede variar, por ejemplo usando query parameters donde se especifican los campos a incluir.

### 💡 Ejemplo:
```http
GET /users/42?fields=name,email
```
Solo retorna el nombre y el email del usuario.

> La implementación de respuesta parcial requiere una consideración sobre el diseño de la API y las necesidades de los clientes. Puede mejorar la eficiencia del envío de la data y reducir procesamiento.  
> Es importante documentar claramente cómo utilizar esta funcionalidad y qué campos pueden solicitarse.

---

## 7️⃣ Paginación

La **paginación** es esencial en APIs que devuelven grandes cantidades de datos, ya que:
- 🔥 Reduce la carga en el servidor y el cliente
- 🚀 Mejora la performance y la experiencia de usuario
- 📊 Facilita el manejo y visualización de grandes colecciones

La paginación es la solución para reducir un set largo de data a uno más corto y manejable. Un objeto de paginación es una mejora significante en la performance y reducción de carga de datos innecesarios.  
Para asegurar consistencia de los datos en caso de querer filtrar y ordenar, se debe implementar estas acciones de forma previa a la paginación. La paginación debe ser el último paso antes de retornar los datos.

**¿Cómo debe ser la respuesta?**
Una respuesta paginada debe incluir:
- Un arreglo con los elementos de la página solicitada
- El número total de páginas (`totalPages`)
- El número total de elementos (`totalElements`)
- El número de la página actual (`page`)
- El tamaño de página (`pageSize`)

### 💡 Ejemplo de respuesta paginada:
```json
{
  "elements": [
    { "id": 1, "name": "Producto 1" },
    { "id": 2, "name": "Producto 2" }
  ],
  "page": 1,
  "pageSize": 2,
  "totalPages": 5,
  "totalElements": 10
}
```

### 📝 Parámetros típicos de paginación:
- `page` y `pageSize`
- `offset` y `limit`

### 🔢 ¿Cómo se calcula la paginación?
Para obtener el índice del primer elemento de una página:
```
startIndex = (page - 1) * pageSize
```
Ejemplo: Para la página 3 y pageSize de 10, el primer elemento es el (3-1)*10 = 20.

El número de páginas se calcula como:
```
totalPages = Math.ceil(totalElements / pageSize)
```

> Para asegurar consistencia de los datos en caso de querer filtrar y ordenar, se debe implementar estas acciones de forma previa a la paginación. La paginación debe ser el último paso antes de retornar los datos.

### 💡 Ejemplo de requests:
```http
GET /products?page=2&pageSize=5
```
Devuelve los productos de la página 2, 5 por página.

```http
GET /products?offset=3&limit=10
```
Devuelve desde el elemento 4, 10 elementos.

### 🧑‍💻 Ejemplo de implementación en C#:
```csharp
public Pagination<Entity> Pagination(int pageNumber, int pageSize)
{
    var elements = collection
      .Skip((pageNumber - 1) * pageSize)
      .Take(pageSize)
      .ToList();

    var totalElements = collection.Count;
    var amountOfPages = (int)Math.Ceiling((double)totalElements / pageSize);

    return new Pagination<Entity>(elements, amountOfPages, totalElements);
}
```

### 🔎 Otros ejemplos avanzados

#### Filtrado y paginación
```csharp
public Pagination<Entity> Pagination(int pageNumber, int pageSize)
{
    var elements = collection
      .Where(e => true)
      .Skip((pageNumber - 1) * pageSize)
      .Take(pageSize)
      .ToList();

    var totalElements = collection.Count;
    var amountOfPages = (int)Math.Ceiling((double)totalElements / pageSize);

    return new Pagination<Entity>(elements, amountOfPages, totalElements);
}
```

#### Filtrado, orden y paginación
```csharp
public Pagination<Entity> Pagination(int pageNumber, int pageSize)
{
    var elements = collection
      .Where(e => e.Prop == prop)
      .OrderBy(e => e.Prop)
      .Skip((pageNumber - 1) * pageSize)
      .Take(pageSize)
      .ToList();

    var totalElements = collection.Count;
    var amountOfPages = (int)Math.Ceiling((double)totalElements / pageSize);

    return new Pagination<Entity>(elements, amountOfPages, totalElements);
}
```

#### Filtrado, orden, agrupación y paginación
```csharp
public Pagination<Entity> Pagination(int pageNumber, int pageSize)
{
    var elements = collection
      .Where(e => e.Prop == prop)
      .GroupBy(e => e.Prop)
      .OrderBy(e => e.Key)
      .Select(e => new {
        Prop = e.Key,
        Amount = e.Count()
      })
      .Skip((pageNumber - 1) * pageSize)
      .Take(pageSize)
      .ToList();

    var totalElements = collection.Count;
    var amountOfPages = (int)Math.Ceiling((double)totalElements / pageSize);

    return new Pagination<Entity>(elements, amountOfPages, totalElements);
}
```

**🔗 Es fundamental implementar la paginación al final de cualquier operación de filtrado, orden o agrupamiento para asegurar la consistencia y precisión de los datos retornados.**

---

## 📚 Lecturas recomendadas

- [Diseño de API - Ebook](https://aulas.ort.edu.uy/pluginfile.php/441401/mod_resource/content/1/api-design-ebook-2012-03.pdf)

---
