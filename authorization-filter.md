[🔙 Indice](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/filters?tab=readme-ov-file#indice) -> [🏠 Main](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/main?tab=readme-ov-file#-temas-y-e...)

# 🛡️ Filtros de Autorización en ASP.NET Core

Los **filtros de autorización** son los primeros en ejecutarse dentro de la pipeline de filtros de ASP.NET Core y su función principal es controlar el acceso a las solicitudes (requests) antes de que cualquier lógica de negocio sea procesada.

---

## 🚦 ¿Cómo funcionan?

- **Prioridad máxima:** Se ejecutan antes que cualquier otro filtro o middleware relacionado al controlador.
- **Control de acceso:** Deciden si una solicitud puede continuar en la pipeline o debe ser bloqueada.
- **Manejo de excepciones:** Si ocurre una excepción en este filtro, **no será manejada automáticamente** por otros filtros, por lo que debes implementar el manejo de errores dentro del filtro.

---

## 🔑 Autenticación, Tokens y Sesiones

Antes de que el filtro de autorización decida si un usuario puede acceder a un recurso, el usuario debe estar **autenticado**. Esto implica verificar la identidad del usuario, generalmente mediante credenciales y algún mecanismo de autenticación.

### 🧩 ¿Qué implica la autenticación?

- **Verificación de identidad:** El usuario provee credenciales (usuario/contraseña, token, etc.).
- **Generación de sesión o token:** Si las credenciales son válidas, el servidor genera un identificador de sesión o un token de acceso.
- **Envío del identificador:** El cliente debe enviar este identificador en cada solicitud para demostrar su identidad.

# 🔑 Tipos de autenticación en el header Authorization

El header `Authorization` se utiliza para enviar credenciales o tokens en las solicitudes HTTP con el fin de identificar y autenticar al usuario o aplicación. Existen varios esquemas y tipos de autenticación que pueden usarse en este header, cada uno con sus implicancias y características.

---

## 🏷️ Esquemas comunes del header Authorization

### 1️⃣ **Bearer (JWT, OAuth Access Token)**
- **Uso:** El más común en APIs modernas con OAuth2 y JWT.
- **Formato:**  
  ```
  Authorization: Bearer <token>
  ```
- **¿Qué implica?**  
  Cuando usas `Bearer`, estás enviando un **token de acceso** como prueba de autenticación. El servidor valida el token (por ejemplo, verificando la firma JWT, la expiración, los claims) y decide si la solicitud está autorizada.
- **Ventajas:**  
  - No requiere mantener estado en el servidor (stateless).
  - Puede incluir información sobre el usuario y permisos (claims).
  - Escalable y seguro si se usa sobre HTTPS.
- **Desventajas:**  
  - Si el token se filtra, cualquiera que lo posea puede acceder a los recursos protegidos.
- **Significado:**  
  “Bearer” significa literalmente “portador”; quien porta el token tiene acceso a los recursos según los permisos codificados en el token.
- **¿Cuándo y por qué se usa el prefijo?**  
  El prefijo `Bearer` es **necesario** porque las APIs y frameworks de autenticación suelen buscar este esquema al analizar el header.  
  Esto permite distinguir entre diferentes métodos de autenticación (por ejemplo, `Basic`, `Digest`, `ApiKey`, etc.) y aplicar la lógica adecuada.  
  Sin el prefijo adecuado, la API puede rechazar la solicitud por no poder identificar el tipo de autenticación.

#### **JWT (JSON Web Token)**
- **Tipo de Bearer Token:** Utilizado para transmitir información de usuario y/o permisos de forma segura y auto-contenida.
- **Ventajas:**  
  - Permite autenticación distribuida (microservicios, apps móviles).
  - Se puede validar sin consultar una base de datos central.
- **Formato:**  
  ```
  Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
  ```
- **Ejemplo de payload decodificado:**
  ```json
  {
    "sub": "1234567890",
    "name": "John Doe",
    "iat": 1516239022,
    "role": "admin"
  }
  ```

---

### 2️⃣ **Basic**
- **Uso:** Autenticación básica HTTP, principalmente para pruebas o sistemas simples.
- **Formato:**  
  ```
  Authorization: Basic <base64(usuario:contraseña)>
  ```
- **¿Qué implica?**  
  El cliente envía el usuario y contraseña codificados en base64. El servidor los descodifica y verifica.
- **Desventajas:**  
  - Poco seguro si no se usa HTTPS, ya que las credenciales pueden ser interceptadas.
  - No escalable ni recomendado para APIs públicas.
- **¿Por qué el prefijo?**  
  El prefijo `Basic` indica explícitamente el esquema usado, permitiendo al backend aplicar la verificación correcta.

---

### 3️⃣ **Digest**
- **Uso:** Mejor que Basic, agrega hash y nonce para evitar ataques de repetición.
- **Formato:**  
  ```
  Authorization: Digest username="usuario", realm="realm", nonce="...", uri="/", response="hash", ...
  ```
- **¿Qué implica?**  
  Utiliza hash criptográfico y parámetros adicionales para que la contraseña no viaje directamente en la red.
- **¿Por qué el prefijo?**  
  El prefijo `Digest` es necesario para que el servidor procese la autenticación bajo ese esquema.

---

### 4️⃣ **API Key**
- **Uso:** Suele usarse en el header Authorization o en un header personalizado.
- **Formato:**  
  ```
  Authorization: ApiKey <api_key>
  ```
- **¿Qué implica?**  
  El cliente envía una clave secreta única, el servidor la valida y otorga acceso.
- **Ventajas:**  
  Simple de implementar.
- **Desventajas:**  
  Menos flexible y seguro que los tokens Bearer; no incluye información de usuario ni permisos.
- **¿Por qué el prefijo?**  
  El prefijo `ApiKey` puede ser útil para distinguir la autenticación tipo API Key de otros tipos en el backend, aunque muchas APIs permiten el uso de headers personalizados como `X-API-Key`.

---

### 5️⃣ **OAuth 2.0**
- **Uso:** Protocolos avanzados de autorización, generalmente usan Bearer como tipo de token.
- **Formato:**  
  ```
  Authorization: Bearer <access_token>
  ```
- **¿Qué implica?**  
  El token puede ser emitido por un proveedor externo y tener scopes/claims específicos. Permite flujo de delegación, Single Sign-On, y otros casos avanzados.

---

### 6️⃣ **SAML (Security Assertion Markup Language)**
- **Uso:** Autenticación federada entre organizaciones (SSO).
- **Formato:**  
  El token SAML suele transmitirse en XML y no directamente en el header Authorization, sino en POST o cookies.
- **¿Qué implica?**  
  Permite a una organización autenticar usuarios en varios servicios.

---

## 🧾 Ejemplo comparativo

```http
// Bearer token (JWT u OAuth2)
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6...

// Basic Auth
Authorization: Basic YWRtaW46cGFzc3dvcmQ=

// API Key
Authorization: ApiKey 123456abcdef

// Digest Auth
Authorization: Digest username="user", realm="app", ...
```

---

## 🚩 Seguridad y recomendaciones

- **Siempre usa HTTPS** para proteger las credenciales y tokens en tránsito.
- **Bearer tokens** deben manejarse con cuidado: si un atacante obtiene el token, puede acceder a los recursos hasta que expire o se revoque.
- **Basic y Digest** son obsoletos para APIs modernas: prefiera Bearer o API Key para sistemas sencillos.
- **API Keys** son útiles para identificar aplicaciones o usuarios, pero no para autenticación fuerte.
- **OAuth y SAML** permiten integración avanzada y SSO en sistemas empresariales.
- **El prefijo es esencial** para que el servidor distinga y aplique correctamente la lógica de autenticación correspondiente.

---

## 📦 Ejemplo de uso del header Authorization

El **header Authorization** es la forma estándar para enviar credenciales o tokens en una solicitud HTTP. Ejemplo con un token Bearer:

```http
GET /api/profile HTTP/1.1
Host: example.com
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

El filtro de autorización recibe la solicitud, extrae el token del header, lo valida y determina si el usuario tiene acceso al recurso solicitado.

---

## 🧑‍💻 Implementación de un filtro de autorización personalizado

Para crear un filtro custom de autorización, implementa la interfaz `IAuthorizationFilter` y el método `OnAuthorization`.

```csharp
public sealed class AuthorizationFilterAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // Lógica de autenticación y autorización
        // Ejemplo: Extraer el token del header y validar claims/roles/permisos
        var authHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
        if (authHeader != null && authHeader.StartsWith("Bearer "))
        {
            var token = authHeader.Substring("Bearer ".Length).Trim();
            // Validar el token...
        }
        else
        {
            // Bloquear acceso por falta de token
            context.Result = new UnauthorizedResult();
        }
    }
}
```

---

## 📌 Cómo aplicar el filtro en Controllers y Actions

Puedes utilizar el filtro tanto a nivel de clase (controller) como a nivel de método (action):

```csharp
[ApiController]
[Route("endpoints")]
[AuthorizationFilter] // Aplica el filtro al controlador completo
public sealed class CustomController : ControllerBase
{
    [HttpGet]
    [AuthorizationFilter] // Aplica el filtro solo a esta acción
    public void MyAction()
    {
        // Acción protegida por el filtro de autorización
    }
}
```

---

## 🧐 Buenas prácticas

- **Maneja las excepciones dentro del filtro:** Usa `try-catch` en tu lógica para evitar errores no controlados que puedan terminar la request abruptamente.
- **Haz la lógica clara:** Asegúrate de que las reglas de autorización sean fáciles de entender y mantener.
- **Combina con roles y claims:** Puedes aprovechar los mecanismos de roles y claims que ASP.NET Core provee para hacer tu filtro más flexible y seguro.
- **Utiliza HTTPS:** Los tokens deben transmitirse siempre por canales seguros para evitar exposición.
- **Expiración y revocación:** Asegúrate de manejar la expiración y posible revocación de tokens.

---

## 📚 Material de lectura

- [Authorization en ASP.NET Core (Documentación Oficial)](https://learn.microsoft.com/en-us/aspnet/core/security/authorization/introduction?view=aspnetcore-8.0)
- [Autenticación con JWT en ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/jwt?view=aspnetcore-8.0)
- [Bearer Authentication RFC6750](https://datatracker.ietf.org/doc/html/rfc6750)
- [Tipos de tokens | Google Cloud](https://cloud.google.com/docs/authentication/token-types?hl=es-419)
- [Autenticación y autorización en APIs REST](https://www.tutorialesprogramacionya.com/apirestya/tema12.html)
- [4 Métodos de Autenticación Para API REST](https://nubecolectiva.com/blog/4-metodos-de-autenticacion-para-api-rest/)
- [API Keys vs OAuth Tokens vs JWT](https://slashmobility.com/blog/2021/02/api-keys-oauth-tokens-vs-jwt/)
- [¿Qué es un token API? Guía rápida — Wallarm](https://lab.wallarm.com/what/que-es-un-token-api-guia-rapida/?lang=es)
---

> Los filtros de autorización son tu primera línea de defensa para proteger los endpoints de tu aplicación. ¡Úsalos sabiamente! 🛡️🚀
