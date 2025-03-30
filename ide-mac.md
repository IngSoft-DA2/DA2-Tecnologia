[Volver - Main](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/main#preparaci%C3%B3n-del-ambiente-local) -> [Volver - IDE](https://github.com/IngSoft-DA2/DA2-Tecnologia/blob/main/ide.md#instalaci%C3%B3n-ide)

# Visual Studio Code

## Introducción

**Visual Studio Code (VS Code)** es un editor de código fuente desarrollado por **Microsoft**, diseñado para ser ligero, rápido y altamente extensible. A diferencia de **Visual Studio**, que es un entorno de desarrollo integrado (IDE) completo, **VS Code** es un editor de código que proporciona herramientas esenciales para programar sin el peso de un IDE tradicional.

VS Code es una de las herramientas más populares entre los desarrolladores debido a su compatibilidad con múltiples lenguajes de programación, su ecosistema de extensiones y su integración con sistemas de control de versiones como **Git**.

## Ventajas y Desventajas

### Ventajas de Visual Studio Code

- **Ligero y rápido**: Consume menos recursos que un IDE completo como Visual Studio.
- **Extensible**: Cuenta con una amplia variedad de extensiones para personalizar su funcionalidad.
- **Compatibilidad con múltiples lenguajes**: Soporta JavaScript, Python, C#, Java, Go, entre otros.
- **Terminal integrada**: Permite ejecutar comandos sin salir del editor.
- **Depurador integrado**: Incluye herramientas para depurar código en distintos lenguajes.
- **Integración con Git**: Facilita el control de versiones y la colaboración en proyectos.
- **Soporte multiplataforma**: Disponible en Windows, macOS y Linux.
- **Configuración flexible**: Permite personalizar la experiencia de desarrollo según las necesidades del usuario.
- **Atajos de teclado personalizables**: Mejora la productividad al permitir configuraciones de accesos rápidos.

### Desventajas de Visual Studio Code

- **No es un IDE completo**: Carece de herramientas avanzadas como IntelliTrace y herramientas de arquitectura.
- **Requiere extensiones para funcionalidades avanzadas**: A diferencia de Visual Studio, muchas características deben agregarse manualmente.
- **Menos optimizado para grandes proyectos**: Puede volverse más lento en proyectos de gran escala comparado con un IDE robusto.
- **Dependencia de extensiones**: Algunas funcionalidades críticas dependen de terceros, lo que puede generar incompatibilidades en ciertas versiones.

## Descarga e Instalación

### Paso 1: Descargar Visual Studio Code

1. Dirígete al sitio web oficial de [VS Code](https://code.visualstudio.com/).
2. Haz clic en el botón **Download**.
3. Selecciona la versión correspondiente a tu sistema operativo:
   - **Windows** (32-bit o 64-bit)
   - **macOS**
   - **Linux** (deb, rpm o tar.gz)

### Paso 2: Instalar Visual Studio Code

1. Abre el archivo descargado.
2. Sigue los pasos indicados en el asistente de instalación.
3. Durante la instalación, activa las siguientes opciones recomendadas:
   - Agregar la opción "Abrir con VS Code" en el menú contextual del explorador de archivos.
   - Agregar VS Code al **PATH** para poder ejecutarlo desde la terminal con `code .`.
4. Una vez instalado, abre **Visual Studio Code**.

### Paso 3: Configurar Visual Studio Code

1. Al abrir VS Code por primera vez, puedes personalizar la interfaz seleccionando un tema.
2. Como se mencionó anteriormente, VS Code es un editor de texto que debe configurarse adecuadamente para acercarse a la experiencia de un IDE.
3. Para ello, se debe importar el perfil preparado [.NET](https://github.com/IngSoft-DA2/DA2-Tecnologia/blob/main/.NET-profile), el cual incluye todos los plugins necesarios y los [settings](https://github.com/IngSoft-DA2/DA2-Tecnologia/blob/main/settings.json) de VS Code, estableciendo configuraciones comunes para todos los integrantes del repositorio.

> [!IMPORTANT]
> El archivo `settings.json` debe ubicarse dentro de una carpeta llamada `.vscode` en la raíz del repositorio.

4. Para importar el perfil:
   - Descarga el perfil desde el enlace proporcionado.
   - Haz clic en el icono de configuración (⚙️) en la esquina inferior izquierda de VS Code.
   - Se abrirá un menú de configuración donde podrás acceder a la sección **Perfiles**.
   - Busca la opción para importar un perfil y selecciona el archivo descargado.
   - Asegúrate de que, al trabajar en un proyecto en .NET, el perfil importado esté activo.
   - Este perfil permitirá acceso rápido a la creación de proyectos, clases y otros elementos relacionados con .NET.

### Paso 4: Verificar la Instalación

1. Abre **Visual Studio Code**.
2. Presiona `Ctrl + Shift + P` (o `Cmd + Shift + P` en macOS) para abrir la **Command Palette**.
3. Escribe `.NET` y verifica que aparezcan opciones relacionadas con la tecnología.
4. Si las opciones aparecen correctamente, significa que la instalación y configuración fueron exitosas.

## Recursos Adicionales

- [Documentación oficial de Visual Studio Code](https://code.visualstudio.com/docs)
- [Extensión C# Dev Kit](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit)
