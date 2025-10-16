[🔙 Indice](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/angular?tab=readme-ov-file#indice) → [🏠 Main](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/main?tab=readme-ov-file#da2-tecnologia--dise%C3%B1o-de-aplicaciones-2)

# 🛠️ Guía de Instalación de Angular

¿Listo para dar tus primeros pasos con Angular? 🚀 Antes de comenzar a crear aplicaciones modernas, necesitas preparar tu entorno. ¡Sigue estos pasos para instalar Angular de forma rápida y sencilla!

---

## 💻 ¿Por qué necesito Node.js para Angular?

Angular depende de **Node.js** para ejecutar herramientas de desarrollo, scripts y gestionar dependencias. Si quieres saber más sobre Node.js, su rol en frontend, ventajas y comparativas, consulta la guía completa en  
👉 [Node.js: Fundamentos, Comparativas, Instalación y su Rol en Proyectos Frontend](https://github.com/IngSoft-DA2/DA2-Tecnologia/blob/main/node-js.md).

---

## 💻 Verifica tu versión de Node.js

Angular requiere una **versión LTS de Node.js**. Para comprobar la versión instalada, abre tu terminal y ejecuta:

```CMD
node --version
```
La versión recomendada es **`20.19.5`**.  
¿No tienes esta versión? ¡No te preocupes! Aquí te mostramos cómo instalarla o actualizarla 👇.

---

## 📥 Descarga e instala Node.js

### 🤔 ¿Qué es NVM y por qué usarlo?

**NVM** (*Node Version Manager*) es una herramienta que te permite instalar, gestionar y cambiar fácilmente entre diferentes versiones de Node.js en tu computadora.

#### 🚀 Ventajas de NVM

- **Gestión de múltiples versiones:** Alterna entre varias versiones de Node.js según lo requiera cada proyecto, evitando conflictos.
- **Facilidad de actualización:** Actualizar Node.js es tan simple como ejecutar un comando, sin desinstalaciones manuales.
- **Entornos limpios:** Cada proyecto puede funcionar con la versión de Node.js que necesita, evitando incompatibilidades.
- **Ahorro de tiempo y simplicidad:** Instalar, eliminar o cambiar versiones lleva segundos.
- **Ideal para equipos y CI/CD:** Facilita la configuración homogénea en equipos y servidores.

#### ⚡ ¿Por qué es mejor que instalar una sola versión de Node.js?

Con NVM tienes **flexibilidad** y **compatibilidad**: si instalas solo una versión, si otro proyecto requiere una diferente, tendrás que desinstalar y reinstalar manualmente, lo que genera errores y pérdida de tiempo. NVM te permite satisfacer todos los requisitos de tus proyectos, manteniendo tu entorno actualizado y sin romper nada.

> **En resumen:**  
**NVM** te da control total sobre las versiones de Node.js, haciendo tu flujo de trabajo más flexible, eficiente y sin dolores de cabeza. ¡Es la mejor práctica recomendada para cualquier desarrollador de Node.js! 💯

---

### 🔄 Usando nvm en Windows

Antes de instalar **nvm (Node Version Manager)**, desinstala cualquier versión previa de Node.js para evitar conflictos.

1. Descarga nvm desde [nvm-windows](https://github.com/coreybutler/nvm-windows?tab=readme-ov-file).
2. Instala nvm y abre la terminal:

```CMD
nvm
nvm install 20.19.5
nvm use 20.19.5
```

Para ver todas las versiones instaladas por nvm:

```CMD
nvm list
```

### 🍏 Usando fnm en Windows/MacOS o nvm en MacOS

- Sigue la guía oficial en [Node.js - Package Manager](https://nodejs.org/en/download/package-manager).
- Descarga la versión recomendada usando **fnm** o **nvm** según tu sistema operativo.

### 📦 Instalador específico

- Descarga el instalador de la versión deseada desde [Node.js Installer](https://nodejs.org/en/download/prebuilt-installer).

---

## ✅ Verifica Node.js y npm

Después de instalar Node.js:

```CMD
node --version
# Esperado: 20.19.5

npm --version
```

¡Asegúrate de tener las versiones correctas antes de continuar! ✔️

---

## ⚡ Instala Angular CLI

### ¿Qué es CLI y para qué sirve Angular CLI? 🅰️🖥️

**CLI** significa **Command Line Interface** (Interfaz de Línea de Comandos). Es una herramienta que permite interactuar con un software directamente desde la terminal mediante comandos.

**Angular CLI** es la interfaz de línea de comandos oficial para Angular.  
Te permite crear, desarrollar, probar y mantener aplicaciones Angular de manera rápida y sencilla usando comandos como `ng new`, `ng serve`, `ng generate`, entre otros.

#### Ventajas de Angular CLI:
- Automatiza tareas comunes, como la creación de componentes, servicios y módulos.
- Facilita la inicialización y configuración de proyectos Angular.
- Permite ejecutar servidores de desarrollo, compilar el código y generar builds de producción.
- Mejora la productividad y homogeneidad de los proyectos.

---

Con Node.js y npm listos, instala [Angular CLI](https://v17.angular.io/cli) para crear y gestionar tus proyectos Angular desde la terminal.

```CMD
npm install -g @angular/cli
```

Verifica la instalación con:

```CMD
ng version
```

<p align="center">
<img src="./images/image-13.png"/>
</p>
<p align="center"><em>📋 Versión de Angular CLI instalada</em></p>

La versión recomendada para este entorno es **Angular CLI `18.0.1`**.

---

### 🆕 ¿Actualizar Angular CLI?

Si tienes una versión antigua, puedes:

- Instalar la última versión:

  ```CMD
  npm install -g @angular/cli@latest
  ```

- O desinstalar y volver a instalar:

  ```CMD
  npm uninstall -g @angular/cli
  npm install -g @angular/cli
  ```

¿Problemas de actualización? Elimina los archivos `ng` en:

```
C://Users/<<nombre de usuario>>/AppData/Roaming/npm
```
Y reinstala Angular CLI.

---

## 🖥️ Instala tu IDE

Puedes usar cualquier editor, pero te recomendamos [Visual Studio Code](https://code.visualstudio.com/), ¡el favorito de la comunidad! ✨

### 🔌 Plugins recomendados para VSCode

- [Angular Language Service](https://marketplace.visualstudio.com/items?itemName=Angular.ng-template)

Estos plugins mejorarán tu experiencia y productividad.

---

## 📚 Lecturas recomendadas

- [👩‍💻 Tu primera aplicación en Angular](https://v17.angular.io/tutorial/first-app)
- [🟩 Node.js: Fundamentos, Comparativas, Instalación y su Rol en Proyectos Frontend](https://github.com/IngSoft-DA2/DA2-Tecnologia/blob/main/node-js.md)

---

> ¡Ya tienes todo listo para comenzar a crear aplicaciones increíbles con Angular! 🅰️✨
