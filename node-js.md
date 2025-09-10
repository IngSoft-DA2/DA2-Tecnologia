# 🟩 Node.js: Fundamentos, Comparativas, Instalación y su Rol en Proyectos Frontend

---

## 🚀 ¿Qué es Node.js?

**Node.js** es un entorno de ejecución para JavaScript basado en el [**motor V8 de Google Chrome**](./motor-v8.md). Permite ejecutar código JavaScript fuera del navegador, principalmente en el servidor (backend).

- **No es un framework ni un lenguaje:** Es un entorno que permite usar JavaScript en el lado del servidor.
- **Open source** y multiplataforma (Windows, Linux, macOS, ARM, etc.).
- Permite construir aplicaciones rápidas y escalables gracias a su modelo de E/S no bloqueante (asíncrono).

---

## 🛠️ ¿Para qué sirve Node.js?

- Crear servidores web y APIs REST/GraphQL.
- Automatizar tareas (scripts, CLI, herramientas de build).
- Servir aplicaciones web y móviles.
- Realizar operaciones en tiempo real (chats, juegos, notificaciones push).
- Soportar aplicaciones de microservicios y serverless.
- Proveer el entorno base para herramientas modernas de frontend (Angular, React, Vue, etc.).

---

## ⚖️ Node.js como backend: Comparativa con otras tecnologías

| Característica           | Node.js           | Java (Spring)       | .NET              | Python (Django/Flask) |
|-------------------------|-------------------|---------------------|-------------------|-----------------------|
| Lenguaje                | JavaScript        | Java                | C#                | Python                |
| Modelo de ejecución     | Asíncrono (event loop) | Multihilo tradicional | Multihilo         | Multihilo             |
| Performance             | Excelente para E/S | Muy bueno           | Muy bueno         | Bueno                 |
| Ecosistema de paquetes  | NPM (masivo)      | Maven/Gradle        | NuGet             | PyPI                  |
| Curva de aprendizaje    | Baja/Media        | Media/Alta          | Media/Alta        | Baja/Media            |
| Ideal para              | Apps en tiempo real, microservicios, APIs rápidas | Grandes sistemas empresariales | Apps empresariales, web APIs | Prototipos, scripts, ciencia de datos |

> **Nota:**  
> **E/S** significa **Entrada/Salida** (Input/Output). Se refiere a operaciones como leer archivos, acceder a bases de datos, o comunicarse por red. Node.js es especialmente eficiente en operaciones de E/S, lo que lo hace ideal para aplicaciones que requieren manejar muchas conexiones o solicitudes simultáneas.

> **Punto clave:** Node.js destaca por su rapidez en operaciones de entrada/salida, escalabilidad, y por usar el mismo lenguaje (JavaScript) en frontend y backend.

---

## 🌐 Node.js en proyectos Frontend modernos

Node.js es fundamental para el desarrollo de aplicaciones de **frontend** modernas (Angular, React, Vue, Svelte, etc.), aunque no se use en producción en el navegador:

### ¿Por qué se necesita Node.js en el desarrollo frontend?

- Las herramientas de desarrollo modernas utilizan el **CLI** (Command Line Interface, o interfaz de línea de comandos), que son programas que se ejecutan desde la terminal para facilitar tareas como crear, compilar, testear y servir proyectos.
- **npm** (Node Package Manager) es el sistema de gestión de paquetes de Node.js; permite instalar dependencias y ejecutar scripts de automatización (`npm run build`, `npm run test`, `npm run lint`, etc.).
- Permite utilizar herramientas de desarrollo como Webpack, Vite, Babel, ESLint, Prettier, entre otras.
- Hace posible el desarrollo, el hot reload, la compilación y el testing de aplicaciones frontend.

### Crear un proyecto frontend usando Node.js (generalizado)

1. **Instalar Node.js**  
   Consulta la [sección de instalación](#instalar-nodejs-local-vs-con-nvm-node-version-manager) para instrucciones detalladas.

2. **Instalar la CLI del framework deseado**  
   - **Angular:**  
     ```bash
     npm install -g @angular/cli
     ```
   - **React:**  
     ```bash
     npm create vite@latest my-app -- --template react
     # o usando create-react-app (menos recomendado hoy)
     npx create-react-app my-app
     ```
   - **Vue:**  
     ```bash
     npm create vite@latest my-app -- --template vue
     # o usando Vue CLI
     npm install -g @vue/cli
     vue create my-app
     ```
   - **Svelte:**  
     ```bash
     npm create vite@latest my-app -- --template svelte
     ```

3. **Crear un nuevo proyecto y ejecutar el servidor de desarrollo**  
   ```bash
   # Ejemplo general
   npm install
   npm run dev
   # o
   npm start
   ```

---

## 💾 Instalar Node.js: Local vs. con NVM (Node Version Manager)

### Instalación directa/local

- Descarga el instalador desde [nodejs.org](https://nodejs.org/).
- Ejecútalo y Node.js + npm estarán disponibles globalmente.
- **Desventajas:**  
  - Solo puedes tener una versión de Node.js instalada a la vez.
  - Actualizar/desinstalar puede ser problemático.
  - Puede haber conflictos si diferentes proyectos requieren diferentes versiones.

### Instalación usando NVM (Node Version Manager)

**NVM** permite instalar y gestionar múltiples versiones de Node.js en la misma máquina de forma sencilla.

#### Ventajas de usar NVM:
- Cambia de versión de Node.js fácilmente según el proyecto.
- Instala, desinstala o actualiza versiones de Node.js sin afectar el sistema.
- Ideal para equipos y proyectos con requerimientos de versiones diferentes.

#### Cómo instalar NVM y Node.js según el sistema operativo:

| Sistema operativo         | Instalación NVM                                                                                      |
|--------------------------|------------------------------------------------------------------------------------------------------|
| **Linux/macOS (Intel/ARM)** | Ejecuta:<br><br>`curl -o- https://raw.githubusercontent.com/nvm-sh/nvm/v0.39.7/install.sh \| bash`<br>`source ~/.bashrc` <em>(o en Mac con zsh: `source ~/.zshrc`)</em><br>`nvm install --lts`<br>`nvm use --lts` |
| **Windows (Intel/Snapdragon)** | Descarga el instalador desde [nvm-windows releases](https://github.com/coreybutler/nvm-windows/releases). Tras instalarlo, en la terminal ejecuta:<br><br>`nvm install lts`<br>`nvm use lts` |
| **Mac con chip Apple Silicon (M1/M2/M3)** | Igual que macOS, asegurándote de tener instalado Rosetta si necesitas compatibilidad.                                           |
| **Snapdragon (ARM64 para Windows)** | Usa el instalador de nvm-windows o instala Node.js para ARM desde nodejs.org, pero NVM sigue siendo recomendable.                 |

> **Nota:**  
> Con NVM puedes ejecutar comandos como `nvm install 18`, `nvm use 18` o `nvm ls` para cambiar o ver versiones instaladas.

---

## 🟢 Resumen: ¿Qué método conviene?

- **NVM (recomendado):**  
  - Flexibilidad total, ideal para desarrolladores y entornos multi-proyecto.
  - Fácil de actualizar y desinstalar versiones.
  - Menos problemas de permisos y rutas.
- **Instalación local directa:**  
  - Simplicidad, útil para principiantes o máquinas dedicadas a un solo proyecto.
  - Menor flexibilidad para proyectos con requerimientos de versiones distintas.

---

## 📚 Recursos útiles

- [Documentación oficial de Node.js](https://nodejs.org/)
- [NVM para Linux/macOS](https://github.com/nvm-sh/nvm)
- [NVM para Windows](https://github.com/coreybutler/nvm-windows)
- [Angular CLI](https://angular.io/cli)
- [Vite (React, Vue, Svelte, etc.)](https://vitejs.dev/)
- [Comparativa de frameworks backend](https://www.geeksforgeeks.org/top-10-backend-frameworks-for-web-development-in-2024/)

---

> 💡 **Sugerencia:**  
> Para desarrollo profesional, **usa NVM** siempre que sea posible para evitar conflictos y mantener tus proyectos actualizados y portables.
