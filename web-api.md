[Indice](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/web-api#indice) -> [Main](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/main)

# Arquitectura Web, REST, RESTful, APIs y el Concepto de Endpoint

## 🌐 Arquitectura Web

Las arquitecturas web modernas funcionan gracias a la integración de diferentes componentes, cada uno con un rol específico pero estrechamente relacionados para ofrecer una experiencia fluida, segura y escalable. 

El patrón más común es el **cliente-servidor**, donde el cliente hace solicitudes al servidor, y este responde con datos o resultados.

### 1. Aplicación Web: el punto de partida

Una **aplicación web** es un software que los usuarios pueden utilizar a través de un navegador (como Chrome o Firefox) y que funciona gracias a una combinación de tecnologías del lado del cliente (frontend) y del servidor (backend). A diferencia de los programas tradicionales, las aplicaciones web no requieren instalación local: todo ocurre accediendo a una URL.

Ejemplos de aplicaciones web: Gmail, Facebook, herramientas bancarias online, sistemas de gestión académica, etc.

---

### 2. Servidor: el cerebro detrás de la web

Un **servidor** es una computadora (física o virtual) configurada para responder solicitudes de otros dispositivos llamados “clientes”. Su función principal es recibir peticiones, procesarlas (usando lógica de negocio, consultas a bases de datos, etc.) y devolver respuestas (normalmente páginas web, archivos o datos).

En el contexto web, los servidores más comunes ejecutan software como Apache, Nginx, Node.js, etc. El cliente casi siempre es el navegador del usuario, aunque también puede ser una app móvil u otro sistema.

---

### 3. Servicio Web: funcionalidad compartida

Un **servicio web** es una funcionalidad accesible a través de la red (normalmente por Internet) que permite que diferentes aplicaciones se comuniquen, independientemente del lenguaje o plataforma en que estén escritas. Los servicios web exponen métodos o endpoints para realizar operaciones como consultar, crear o modificar datos.

Ejemplo: Un servicio web de clima que devuelve la temperatura al recibir una solicitud HTTP, usado tanto por apps móviles como por sitios web.

---

### 4. Protocolo HTTP/HTTPS: el idioma común

- **HTTP (HyperText Transfer Protocol):**  
  Es el protocolo principal que regula cómo los clientes y servidores web se comunican. Define cómo deben estructurarse las solicitudes y respuestas, permitiendo transferir páginas HTML, archivos, imágenes, datos JSON, etc. HTTP es “sin estado”, es decir, cada solicitud es independiente.

- **HTTPS (HTTP Secure):**  
  Es la versión segura de HTTP. Usa cifrado (SSL/TLS) para proteger la información transmitida entre el cliente y el servidor, asegurando autenticidad, privacidad e integridad de los datos. Hoy en día, HTTPS es esencial para proteger información sensible (contraseñas, tarjetas de crédito, etc.) y es el estándar recomendado.

---

### 5. Servidor DNS: el traductor de nombres

El **DNS (Domain Name System)** es como la “guía telefónica” de Internet. Su función es traducir los nombres de dominio legibles por humanos (como `www.ejemplo.com`) en direcciones IP que las computadoras usan para identificarse en la red (como `192.0.2.1`).  
Cuando escribes una URL en tu navegador, el primer paso es consultar un servidor DNS para saber a qué dirección IP debe conectarse tu equipo.

---

### 6. Load Balancer: distribuyendo la carga

Un **load balancer** distribuye el tráfico de red entrante entre varios servidores para evitar que uno solo se sobrecargue, mejorar el rendimiento, la disponibilidad y la tolerancia a fallos. Si uno de los servidores deja de funcionar, el balanceador redirige el tráfico a los que siguen en línea, asegurando que la aplicación siga disponible para los usuarios.

---

### 8. API Gateway: la puerta de entrada centralizada

Un **API Gateway** es un componente central en arquitecturas modernas (como microservicios). Actúa como un “puente” o punto de entrada único para todas las solicitudes externas dirigidas a las APIs de una aplicación. Sus funciones incluyen:

- Unificar el acceso a múltiples servicios internos.
- Autenticación y autorización.
- Limitación de tráfico (rate limiting).
- Transformación de formatos de datos.
- Registro y monitoreo.

Con un API Gateway, los clientes no interactúan directamente con cada microservicio, sino que solo se comunican con el gateway, que luego enruta la solicitud al servicio adecuado.

---

**En resumen:**  
La experiencia de un usuario con una aplicación web involucra a todos estos componentes interconectados: desde que escribe una dirección en su navegador (DNS), pasando por el envío de solicitudes (HTTP/HTTPS y load balancer), la lógica del servidor y los servicios web, hasta la gestión avanzada mediante un API Gateway. Cada elemento cumple un papel clave y su integración es lo que hace posible que las aplicaciones web modernas sean seguras, eficientes y escalables.

---

> **En conjunto**, estos componentes forman la base de las aplicaciones web modernas, permitiendo que sean escalables, seguras y fáciles de mantener.

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

Pueden profundizar mas sobre el tema en [REST](https://github.com/daniel18acevedo/DA2-Tecnologia/blob/web-api/rest.md).

---

## 📡 ¿Qué es una API?

Una **API** (Application Programming Interface) es un conjunto de reglas y mecanismos que permite que dos aplicaciones se comuniquen entre sí. En el contexto web, una **API web** define cómo interactuar con los recursos del servidor usando HTTP.

**Ejemplo de API RESTful:**
- `GET /productos` → Lista todos los productos.
- `POST /productos` → Crea un nuevo producto.
- `GET /productos/42` → Obtiene el producto con id 42.
- `PUT /productos/42` → Actualiza **todo** el producto 42.
- `PATCH /productos/42` → Actualiza **parcialmente** el producto 42.
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
