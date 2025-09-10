[🔙 Volver - Main](./README.md)

# 🌐 Clientes HTTP, Postman y Pruebas de Integración en APIs Web

---

## 🚀 ¿Qué es un Cliente HTTP?

Un **cliente HTTP** es una herramienta que permite enviar peticiones (requests) y recibir respuestas (responses) a través del protocolo HTTP(s). Esencial para interactuar, probar o consumir APIs web (REST, SOAP, GraphQL, etc.), ya sea desde aplicaciones, scripts o herramientas dedicadas.

**Ejemplos de clientes HTTP:**
- Interfaces gráficas: Postman, Insomnia, Paw
- Línea de comandos: curl, httpie
- Archivos de prueba: `.http` en proyectos .NET (REST Client), archivos `.rest`, etc.

---

## 🧪 ¿Qué son las pruebas de integración?

Las **pruebas de integración** validan que diversos componentes de un sistema funcionen correctamente juntos.  
En el contexto de APIs y servicios web, estas pruebas permiten:
- Verificar que los endpoints expongan los datos esperados.
- Que la comunicación entre servicios (por ejemplo, una API y su base de datos) sea correcta.
- Detectar errores en la interacción completa, más allá de pruebas unitarias aisladas.

**¿Por qué hacer pruebas de integración con un cliente HTTP?**
- Simulan cómo interactuaría un consumidor real (otra aplicación, usuario, etc.).
- Permiten automatizar la verificación de endpoints.
- Ayudan a documentar y compartir ejemplos de uso de la API.

---

## 🧰 ¿Qué es Postman?

**Postman** es una de las herramientas más populares y completas para trabajar con APIs y realizar pruebas HTTP.

### ¿Para qué sirve?
- Enviar peticiones HTTP (GET, POST, PUT, DELETE, PATCH, etc.) y ver las respuestas.
- Probar y depurar endpoints de APIs.
- Automatizar pruebas de integración y regresión.
- Compartir y documentar APIs con equipos de desarrollo.

### ¿Qué se puede hacer con Postman?
- Crear y gestionar colecciones de peticiones.
- Organizar pruebas en Workspaces (espacios de trabajo).
- Definir entornos (environments) para cambiar variables fácilmente (por ejemplo, URL base, tokens, etc.).
- Scriptar pre-request y test scripts en JavaScript.
- Visualizar y analizar respuestas, encabezados, estados, tiempo de respuesta, etc.
- Generar documentación automática y compartirla.
- Automatizar pruebas y flujos con "Collections Runner" y monitorización.

---

## 🗂️ Ambientes (Environments) en Postman

### ¿Qué son los ambientes?
Un **ambiente (environment)** en Postman es un conjunto de variables reutilizables que permiten cambiar fácilmente valores en las peticiones, como la URL base, credenciales, tokens, etc.  
Esto permite ejecutar la misma colección de peticiones en diferentes contextos (desarrollo, test, producción, etc.) sin modificar manualmente cada request.

### Tipos de ambientes
- **Global Environment:** Variables que están disponibles para todas las colecciones y peticiones.
- **Collection Environment:** Variables específicas para una colección determinada.
- **Local Variables (Scope):** Definidas y utilizadas dentro de una sola petición.

### ¿Cómo crear un ambiente?
1. Haz clic en el icono de "Engranaje" (⚙️) en la esquina superior derecha y selecciona "Manage Environments".
2. Haz clic en "Add" para crear un nuevo ambiente.
3. Define el nombre del ambiente (por ejemplo: `dev`, `prod`) y agrega las variables necesarias (por ejemplo: `base_url`, `token`).
4. Guarda el ambiente.
5. Selecciona el ambiente desde la barra superior antes de ejecutar tus requests.

### ¿Cómo usar variables de ambiente en las requests?
Utiliza doble llave para referenciar variables:  
`{{nombre_variable}}`

**Ejemplo de uso:**
```http
GET {{base_url}}/usuarios
Authorization: Bearer {{token}}
```
Cuando ejecutas la petición, Postman reemplaza `{{base_url}}` y `{{token}}` por los valores definidos en el ambiente seleccionado.

**Consejo:**  
Puedes definir variables como URL base, API keys, IDs dinámicos, etc. Así, puedes cambiar de entorno fácilmente y compartir colecciones sin exponer datos sensibles.

---

## ⚡ Scripts en Postman: Pre-request y Tests

### ¿Qué son los scripts en Postman?
Postman permite agregar **scripts personalizados en JavaScript** que se ejecutan antes de la petición (**Pre-request Scripts**) o después de recibir la respuesta (**Test Scripts**).

### Utilidad de los scripts

- **Pre-request Scripts:**  
  - Generar tokens dinámicamente (JWT, OAuth, etc.).
  - Establecer o modificar variables antes de enviar la petición.
  - Crear datos aleatorios para pruebas (ej: nombres, emails, etc.).
  - Encadenar peticiones (guardar valores de una response y usarlos en la siguiente request).

- **Test Scripts:**  
  - Validar que la respuesta tenga el status esperado (`pm.response.code`).
  - Verificar el contenido de la respuesta (body, headers, etc.).
  - Guardar datos de la respuesta en variables de ambiente para usarlas en otras requests.
  - Automatizar aserciones y condiciones de éxito/fallo.

### Ejemplo de Pre-request Script

```javascript
// Generar un número aleatorio y guardarlo como variable de entorno
pm.environment.set("random_user", "user_" + Math.floor(Math.random() * 1000));
```

### Ejemplo de Test Script

```javascript
// Verificar que el status sea 200 OK
pm.test("Status code is 200", function () {
    pm.response.to.have.status(200);
});

// Guardar un token de respuesta para siguientes requests
let jsonData = pm.response.json();
pm.environment.set("token", jsonData.token);
```

**¿Por qué es útil?**
- Permite automatizar flujos de prueba complejos sin salir de Postman.
- Reduce la repetición manual y errores humanos.
- Facilita la integración y validación de APIs en distintas etapas del desarrollo.

---

## 🧩 Elementos clave en Postman

- **Workspace:**  
  Espacio de trabajo colaborativo donde puedes agrupar colecciones, entornos y recursos relacionados a un proyecto o equipo.

- **Collection (Colección):**  
  Conjunto organizado de peticiones HTTP. Permite estructurar pruebas, flujos y documentación de una API.

- **Environment (Entorno):**  
  Conjunto de variables reutilizables para una colección de peticiones.  
  - **Global Environment:** Variables accesibles desde cualquier colección/petición.
  - **Collection Environment:** Variables específicas de una colección.

- **Headers:**  
  Datos adicionales enviados en la petición HTTP (por ejemplo, autenticación, tipo de contenido, etc.)

- **Verbos HTTP:**  
  Métodos que definen la acción de la petición, como GET (leer), POST (crear), PUT/PATCH (actualizar), DELETE (eliminar).

---

## 🔄 Comparación: Postman vs. archivo `.http` en un proyecto .NET

### Postman
- Interfaz gráfica amigable y potente.
- Permite ejecutar, guardar y organizar peticiones en colecciones y entornos.
- Facilita la colaboración, documentación automática y compartir flujos completos.
- Permite scripting avanzado (pre-request y tests) en JavaScript.
- Exporta e importa colecciones fácilmente.
- Ideal para equipos y APIs complejas.

### Archivos `.http` en .NET (REST Client)
- Son archivos de texto plano que contienen una o varias peticiones HTTP, interpretadas por extensiones como "REST Client" en VS Code o integradas en el IDE.
- Permiten escribir, ejecutar y depurar peticiones HTTP directamente desde el editor de código.
- Pueden versionarse fácilmente junto al código fuente.
- Menos amigables visualmente, pero muy útiles para documentación y pruebas rápidas.
- No ofrecen características colaborativas, scripting ni reportes automáticos como Postman.

#### Ejemplo de archivo `.http`:
```http
GET https://api.ejemplo.com/usuarios/123
Authorization: Bearer {{token}}
```

---

## 📝 Resumen

- Los **clientes HTTP** son fundamentales para interactuar y probar APIs.
- **Postman** es una herramienta gráfica integral para gestionar, probar y documentar APIs.
- Los **ambientes** y **scripts** en Postman permiten flexibilidad, automatización y robustez en las pruebas.
- Las **pruebas de integración** con clientes HTTP verifican que los servicios funcionen correctamente en conjunto.
- Los **archivos `.http`** en .NET son una alternativa ligera y versionable para pruebas rápidas y documentación, aunque con menos funciones avanzadas que Postman.

---

## 📚 Recursos útiles

- [Postman Docs](https://learning.postman.com/docs/)
- [Guía de ambientes en Postman](https://learning.postman.com/docs/sending-requests/managing-environments/)
- [Postman Scripts Reference](https://learning.postman.com/docs/writing-scripts/intro-to-scripts/)
- [Comparativa: Postman vs. REST Client](https://github.com/Huachao/vscode-restclient)
- [Pruebas de integración en .NET](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices#integration-tests)
- [HTTPie (cliente CLI)](https://httpie.io/)
- [curl (cliente CLI)](https://curl.se/)

---

> 💡 **Sugerencia:**  
> Utiliza Postman para exploración, colaboración y pruebas avanzadas; emplea archivos `.http` para ejemplos rápidos y documentación que viaje junto al código.
