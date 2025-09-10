# Arquitectura Web, REST, RESTful, APIs y el Concepto de Endpoint

## 🌐 Arquitectura Web

La **arquitectura web** describe cómo se organizan y comunican los componentes de una aplicación web. Normalmente incluye:

- **Cliente (Frontend):** Interfaz de usuario, habitualmente una web o app móvil.
- **Servidor (Backend):** Procesa solicitudes, ejecuta la lógica de negocio y accede a los datos.
- **Base de datos:** Almacena la información persistente.
- **Red (Internet):** Medio por el cual cliente y servidor se comunican usando protocolos como HTTP/HTTPS.

El patrón más común es el **cliente-servidor**, donde el cliente hace solicitudes al servidor, y este responde con datos o resultados.

---

## 🔗 ¿Qué es REST y RESTful?

### REST (Representational State Transfer)

REST es un estilo arquitectónico para diseñar servicios de red, definido por Roy Fielding. Se basa en principios que aprovechan el protocolo HTTP.

**Principios clave de REST:**
- **Recursos:** Todo (usuarios, productos, pedidos, etc.) se representa como un recurso con una URL única.
- **Métodos HTTP:** Se usan métodos como GET, POST, PUT, DELETE para operar sobre esos recursos.
- **Representaciones:** La información puede enviarse en varios formatos (JSON, XML).

**Restricciones de REST:**
1. **Interfaz uniforme:** Todas las interacciones se realizan de manera consistente, facilitando la comprensión y evolución de las APIs.
2. **Stateless:** Cada solicitud del cliente al servidor debe contener toda la información necesaria; el servidor no almacena el estado del cliente entre peticiones.
3. **Cacheable:** Las respuestas deben indicar explícitamente si pueden ser almacenadas en caché, para mejorar la eficiencia y escalabilidad.
4. **Arquitectura cliente-servidor:** El cliente y el servidor están separados, permitiendo desarrollar y evolucionar cada uno de manera independiente.
5. **Sistema de capas:** Una arquitectura REST puede componerse de varias capas (balanceadores, proxies, servidores intermedios), donde cada capa solo conoce la interfaz inmediata.

### RESTful

Un servicio o API es **RESTful** cuando implementa correctamente los principios y restricciones de REST. No toda API HTTP es RESTful: debe respetar la semántica de los métodos y la estructura de los recursos.

---

## 📡 ¿Qué es una API?

Una **API** (Application Programming Interface) es un conjunto de reglas y mecanismos que permite que dos aplicaciones se comuniquen entre sí. En el contexto web, una **API web** define cómo interactuar con los recursos del servidor usando HTTP.

**Ejemplo de API RESTful:**
- `GET /productos` → Lista todos los productos.
- `POST /productos` → Crea un nuevo producto.
- `GET /productos/42` → Obtiene el producto con id 42.
- `PUT /productos/42` → Actualiza el producto 42.
- `DELETE /productos/42` → Elimina el producto 42.

---

## 📍 ¿Qué es un Endpoint?

Un **endpoint** es una URL específica a la que un cliente puede enviar peticiones para interactuar con un recurso o funcionalidad de un servidor.

### Definición

Un endpoint es una dirección (URL) única dentro de una API o aplicación web que expone una operación o acceso a un recurso en particular. Cada endpoint está asociado a:
- Una ruta específica (por ejemplo, `/usuarios`, `/productos/42`)
- Un método HTTP (GET, POST, PUT, DELETE, etc.)

### Ejemplo en una API RESTful

Supongamos que tienes una API para gestionar productos. Algunos endpoints típicos serían:

- `GET /productos` → Listar todos los productos.
- `POST /productos` → Crear un nuevo producto.
- `GET /productos/42` → Obtener el detalle del producto con ID 42.
- `PUT /productos/42` → Actualizar el producto con ID 42.
- `DELETE /productos/42` → Eliminar el producto con ID 42.

Cada combinación de ruta + verbo HTTP es un endpoint diferente.

### ¿Para qué sirven?

- **Definen la interfaz de comunicación** entre clientes (web, móviles, sistemas externos) y el servidor.
- **Organizan y documentan** las operaciones disponibles en una API.
- **Permiten controlar el acceso** y aplicar reglas de seguridad según el endpoint.

> Un **endpoint** es el "punto de entrada" a una funcionalidad o recurso específico de un sistema web, identificado por una URL y generalmente asociado a un método HTTP. Son la columna vertebral de cualquier API moderna.

---

## 🤖 Diferencia entre API y SDK

- **API:** Especifica *qué* operaciones se pueden realizar y *cómo* acceder a ellas (por ejemplo, los endpoints REST de una web API).
- **SDK (Software Development Kit):** Es un conjunto de herramientas, librerías y documentación que facilita el uso de una API desde un lenguaje o plataforma específica. El SDK suele envolver la API y simplificar su uso.

**Ejemplo:**  
- Google Maps tiene una *API REST* para consultar información de mapas.  
- Google provee un *SDK para Android* que usa internamente esa API y ofrece métodos más sencillos para desarrolladores móviles.

---

## ✅ Ventajas de REST y APIs Web

- **Interoperabilidad:** Distintos sistemas y lenguajes pueden interactuar fácilmente a través de HTTP.
- **Escalabilidad:** Arquitectura sin estado y orientada a recursos facilita escalar horizontalmente.
- **Flexibilidad:** Los recursos pueden representarse en múltiples formatos (JSON, XML).
- **Desarrollo desacoplado:** Frontend y backend pueden evolucionar independientemente.
- **Estándares conocidos:** Aprovecha HTTP, lo que facilita la integración y el aprendizaje.

---

## ❌ Desventajas de REST y APIs Web

- **Verbosidad:** Puede requerir muchas solicitudes para operaciones complejas.
- **Limitaciones de HTTP:** Depende de las restricciones y performance de HTTP.
- **Versionado:** Mantener compatibilidad entre versiones puede ser complejo.
- **Seguridad:** Expone endpoints en la web, por lo que requiere buenas prácticas de autenticación y autorización.

---

## 🆚 ¿Cuándo usar API vs SDK?

- Usa **API** cuando:
  - Quieres máxima flexibilidad o integración con múltiples plataformas.
  - Necesitas acceso directo a los endpoints y control total.

- Usa **SDK** cuando:
  - Quieres facilitar el desarrollo y reducir errores comunes.
  - El proveedor ofrece un SDK oficial para tu lenguaje/plataforma.

---

> **En resumen**: REST y las APIs web son la base de la comunicación moderna entre servicios. Entender sus fundamentos, restricciones y diferencias con los SDKs y endpoints es esencial para diseñar, consumir e integrar sistemas distribuidos de manera profesional.
