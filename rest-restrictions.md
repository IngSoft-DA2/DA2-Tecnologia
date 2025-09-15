[🔗 ¿Qué es REST y RESTful?](https://github.com/IngSoft-DA2/DA2-Tecnologia/edit/web-api/web-api.md#-qu%C3%A9-es-rest-y-restful) → [🔙 Indice](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/web-api?tab=readme-ov-file#indice) → [🏠 Main](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/main?tab=readme-ov-file#da2-tecnologia--dise%C3%B1o-de-aplicaciones-2)

# 🚦 Restricciones REST

Las arquitecturas REST imponen una serie de **restricciones** que aseguran sistemas robustos, escalables y fáciles de evolucionar. A continuación, se describen cada una de ellas, con explicación ampliada, emojis y ejemplos de implementación o uso.

---

## 1️⃣ Interfaz Uniforme

🔗 Una **interfaz uniforme** define un contrato claro entre cliente y servidor. Permite que ambos evolucionen de manera independiente, siempre que la interfaz se respete.

**Principios clave:**
- 📦 Basarse en **recursos**: cada entidad significativa del sistema (usuarios, productos, pedidos) se modela como un recurso accesible mediante una URL única.
- 📋 Las respuestas incluyen toda la información necesaria para manipular los recursos, usando formatos estándar como JSON o XML.
- 🚦 Uso consistente de métodos HTTP (GET, POST, PUT, DELETE) y códigos de estado (200 OK, 404 Not Found, etc.)

**Ventajas:**  
- Facilita la comprensión y el consumo de la API por parte de cualquier cliente.
- Permite el desarrollo desacoplado y la evolución independiente del cliente y el servidor.

**Ejemplo de uso:**
```http
GET /productos/123
```
Respuesta:
```json
{
  "id": 123,
  "nombre": "Teclado Mecánico",
  "precio": 79.99,
  "stock": 25
}
```
Aquí, cada recurso tiene su propia URL (`/productos/123`), y la respuesta contiene toda la información relevante para ese producto.

---

## 2️⃣ Comunicación Stateless

🗃️ **Stateless** significa que cada petición del cliente debe contener toda la información necesaria para ser procesada.  
El servidor **no guarda el estado** de la sesión del cliente entre solicitudes.

**¿Cómo se logra?**
- Toda la información relevante viaja en cada request: tokens de autenticación, datos de contexto, etc.
- El servidor puede procesar cualquier petición de forma aislada, sin depender de información previa.

**Ventajas:**
- 🚀 Escalabilidad: cualquier instancia puede atender la petición, facilitando balanceo de carga.
- 🔄 Redespliegue y recuperación simples ante fallos, ya que no hay sesiones persistentes.
- 🌐 Distribución eficiente y tolerancia a fallos.

**Ejemplo de implementación:**
```http
GET /perfil
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```
Cada request contiene el token de autenticación en el header, permitiendo que el servidor identifique al usuario sin necesidad de mantener una sesión.

<p align="center">
  <img src="images/image-15.png" alt="Escalabilidad"/>
</p>
<p align="center">
  <em>Escalabilidad</em>
</p>
<p align="center">
  <img src="images/image-16.png" alt="Multiples conexiones"/>
</p>
<p align="center">
  <em>Múltiples conexiones</em>
</p>

---

## 3️⃣ Cacheable

🗄️ Las respuestas del servidor pueden ser **cacheadas** por el cliente para reducir la necesidad de repetir solicitudes idénticas.

**¿Cómo funciona?**
- ✅ Las respuestas incluyen cabeceras (headers) como `Cache-Control` o `ETag` que indican si pueden ser almacenadas en caché y por cuánto tiempo.
- ⚡ El cliente puede reutilizar estas respuestas, acelerando el acceso y reduciendo carga al servidor.
- 🚫 Las respuestas que no deben ser cacheadas deben indicar explícitamente que no lo son.

**Ventajas:**
- 🚦 Mejora la **performance** y la experiencia de usuario.
- 📈 Aumenta la **escalabilidad** y reduce el tráfico innecesario hacia el servidor.

**Ejemplo de implementación:**
```http
GET /noticias
Cache-Control: public, max-age=600
```
Esto indica que la respuesta puede almacenarse en caché durante 10 minutos (600 segundos). El cliente puede mostrar la misma noticia sin pedirla nuevamente al servidor durante ese periodo.

<p align="center">
  <img src="images/image-17.png" alt="Cache"/>
</p>

---

## 4️⃣ Cliente-Servidor

🧑‍💻 ⇄ 🖥️ El modelo separa la interfaz de usuario (**cliente**) de la gestión de datos y lógica (**servidor**).

**¿Por qué es importante?**
- El cliente se enfoca en la experiencia de usuario, mientras que el servidor se encarga del almacenamiento, procesamiento y lógica de negocio.
- Permite que ambos evolucionen de manera independiente mientras respeten la interfaz (API).
- Facilita el desarrollo multiplataforma: diferentes tipos de clientes (web, móvil, IoT) pueden consumir los mismos servicios.

**Ejemplo de implementación:**
- Una aplicación móvil, una web SPA y un sistema de administración pueden consumir la misma API RESTful:

```http
GET /usuarios/42
```
Todos obtienen la representación del usuario con ID 42, aunque la forma de mostrarla varía según el cliente.

<p align="center">
  <img src="images/image-18.png" alt="Cliente-Servidor"/>
</p>

---

## 5️⃣ Sistema en Capas (Tiered / Layered System)

🏗️ Un cliente **no sabe** si está comunicándose con el servidor final o con un intermediario (por ejemplo, un proxy, un balanceador de carga o una caché compartida).

**Propósitos y ventajas:**
- ⚖️ Permiten balancear la carga entre múltiples servidores y mejorar la escalabilidad.
- 🛡️ Implementan políticas de seguridad y filtrado en capas intermedias (firewalls, gateways).
- 🧩 Facilitan la modularidad y el mantenimiento del sistema, agregando o eliminando capas según sea necesario sin afectar a los clientes.

**Ejemplo de uso:**
- Un API Gateway recibe todas las solicitudes, realiza autenticación y luego redirige la petición al microservicio correspondiente:
```http
Cliente → API Gateway → Servicio de Productos → Base de Datos
```
El cliente solo conoce la URL del gateway y no sabe si hay intermediarios o cuántas capas existen detrás.

<p align="center">
  <img src="images/image-19.png" alt="Sistema en capas"/>
</p>

---

> 📝 **Resumen:**  
> Las restricciones REST (interfaz uniforme, stateless, cache, cliente-servidor y sistema en capas) son la base para construir APIs web modernas, eficientes y escalables.
