[🔙 Indice](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/web-api?tab=readme-ov-file#indice) → [🏠 Main](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/main?tab=readme-ov-file#da2-tecnologia--dise%C3%B1o-de-aplicaciones-2)

# 🕹️ Controllers en ASP.NET Core

Los **Controllers** son clases esenciales en el desarrollo de APIs con ASP.NET Core, ya que cumplen el rol fundamental de recibir solicitudes HTTP (*requests*), procesarlas y generar las respuestas adecuadas para los clientes.  
Cada controller representa un conjunto de endpoints asociados a un recurso o funcionalidad de tu aplicación, facilitando la organización y el mantenimiento del código. 🚦

---

## 🗺️ ¿Cuál es su responsabilidad?

La función principal de un controller es servir como punto de entrada para las peticiones HTTP.  
Al llegar una request a tu Web API, el framework utiliza la configuración de enrutamiento para determinar **qué controller** y **qué acción** deben manejar la solicitud.  
Una vez identificado, .NET Core instancia el controller (usando inyección de dependencias si es necesario), ejecuta el método correspondiente y envía la respuesta al cliente.

---

## 📋 Responsabilidades de un Controller

Un controller cumple tres tareas clave:

- 🔗 **Mapeo de endpoints a funciones:** Relaciona rutas HTTP (como `/api/usuarios/1`) con métodos específicos dentro de la clase.
- ⚙️ **Procesamiento de requests:** Extrae datos de la solicitud, llama a la lógica de negocio y procesa la información recibida.
- 📤 **Generación de respuestas:** Devuelve el resultado al cliente, ya sea datos, mensajes de error o confirmaciones.

Por convención, los controllers se ubican dentro de la carpeta **Controllers** del proyecto.

---

## 🧩 Elementos de un Controller

### 🏷️ Atributo `[ApiController]`

Este atributo especial marca una clase como **controller**. Generalmente se aplica a clases que heredan de **ControllerBase**, indicando que la clase es un **API controller**.

El atributo `[ApiController]` habilita varias funcionalidades automáticas:
- **Respuesta automática con código 400:** El framework valida los modelos automáticamente y retorna un 400 si falla la serialización.
- **Bindeo automático:** No es necesario especificar cómo se obtiene la información para los parámetros del método, el framework lo resuelve automáticamente.
- **Respuesta automática para errores no cacheados:** Si ocurre una excepción, retorna automáticamente un error 500.
- **Formato de errores:** Las respuestas de error siguen el formato aceptado por el cliente (JSON, XML, etc).
- **Respuestas 204 automáticas para resultados vacíos:** Si un método retorna `null`, se responde con 204 (NoContent).

En síntesis, este atributo simplifica la creación de APIs y aporta consistencia y buenas prácticas de respuesta.

---

### 🛣️ Atributo `[Route]`

El atributo `[Route]` se utiliza para definir el endpoint base a nivel de clase, o de manera individual a nivel de función dentro del controller. Permite especificar la URI asociada al controller o a sus métodos.

- **Enrutamiento a nivel de clase:** Define una ruta base para todos los métodos del controller.
- **Enrutamiento a nivel de función:** Permite agregar segmentos adicionales a la ruta para métodos específicos.
- **Soporta parámetros en ruta:** Puedes usar llaves `{}` para rutas dinámicas (ej: `{id}`).
- **Restricciones de parámetros:** Puedes limitar los valores aceptados por los parámetros en la ruta.

Esta separación permite que todos los endpoints asociados a un recurso estén encapsulados en una sola clase, mejorando la organización y el mantenimiento del código.

```csharp
[ApiController]
[Route("movies")]
public sealed class MovieController : ControllerBase
{
    // Código del controlador
}
```

---

### 🛑 Atributos `[HttpPost]`, `[HttpGet]`, `[HttpPut]`, `[HttpDelete]`

Estos atributos se utilizan a nivel de método para especificar qué función procesa cada verbo HTTP.  
La combinación de verbo HTTP y endpoint define qué función manejará cada tipo de solicitud.

---

### 🎯 Atributos de Binding

Permiten especificar explícitamente de dónde obtiene el framework los datos para los parámetros del método:

- **[FromHeader]:** La data está en el header de la solicitud.
- **[FromRoute]:** La data está en la ruta (URL).
- **[FromQuery]:** La data proviene de los parámetros de query.
- **[FromBody]:** La data está en el cuerpo de la solicitud.

```csharp
[HttpPut("{movieId}")]
public IActionResult UpdateMovie(string movieId, [FromBody] MovieUpdateRequest request)
{
    // Código del método       
}
```

---

## 🛡️ Importante

Los controllers están pensados para ser utilizados por el framework ASP.NET Core, no deben ser instanciados manualmente en la lógica de negocio, aunque sí pueden ser instanciados en pruebas unitarias para verificar su comportamiento.

---

## 🔄 Ciclo de vida de una request

1. 🌐 El cliente realiza una petición HTTP a la Web API.
2. 🧭 El sistema de enrutamiento de ASP.NET Core determina el controller y el método correspondiente.
3. 🏗️ Se instancia el controller y se ejecuta la acción.
4. 📡 Se envía la respuesta generada al cliente.

Así, los controllers orquestan el procesamiento de cada request y encapsulan la lógica de manejo de entradas/salidas, delegando la lógica compleja a otras capas de la aplicación.

---

## ✅ Buenas Prácticas para Controllers

- **Route a nivel de clase:**  
  Usa el atributo `[Route]` a nivel de clase, evitando el placeholder `[controller]`. Define rutas explícitas y descriptivas.

- **Niveles extra:**  
  Agrega niveles a la ruta base en los atributos de los verbos HTTP, por ejemplo: `[HttpGet("{id}")]` para rutas dinámicas o `[HttpGet("users")]` para subrutas fijas.

- **Controllers `sealed`:**  
  Los controllers de recursos específicos deberían ser clases selladas (`sealed`), ya que no se reutilizan ni se heredan.

- **Estado privado:**  
  El estado de los controllers debe ser privado. Los controllers solo actúan como puertas de entrada; evita exponer estado público.

- **Un recurso a la vez:**  
  Cada controller debe manejar un solo recurso y su lógica asociada, lo que simplifica el mantenimiento y el testing.

- **Respuestas automáticas:**  
  Aprovecha el manejo automático de respuestas exitosas retornando objetos simples, en vez de `IActionResult` o `ActionResult`, siempre que sea posible.

- **Sin try/catch en métodos:**  
  El manejo de errores debe ser global. Evita bloques try/catch en los métodos de los controllers.

- **Métodos simples y delegados:**  
  Los métodos de los controllers solo deben orquestar y delegar la lógica a otras capas, no implementar lógica de negocio compleja.

- **Funciones públicas:**  
  Los métodos deben ser públicos para que puedan ser invocados por el sistema de enrutamiento.

- **Uso de DTOs:**  
  Utiliza Data Transfer Objects (DTOs) para intercambiar información entre capas. Los DTOs deben ser estructuras simples y primitivas para facilitar la serialización y la evolución de tu API.

---

## 📚 Lecturas recomendadas

- [Model binding (Microsoft Docs)](https://learn.microsoft.com/en-us/aspnet/core/mvc/models/model-binding?view=aspnetcore-3.1)

---
