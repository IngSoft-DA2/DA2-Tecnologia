[Volver - Main](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/main#preparaci%C3%B3n-del-ambiente-local)

# IDE

**IDE** proviene de **Integrated Development Environment** (Entorno de Desarrollo Integrado), que es el acrónimo utilizado para denominar aquellos softwares que proporcionan un ambiente con las herramientas necesarias para crear software, todas ya integradas.

En este entorno se pueden encontrar:

- Compiladores
- Editores de texto
- Depuradores (debuggers)
- Herramientas de compilación automática

Todo esto está incluido en una única interfaz.

Los **IDEs** fueron diseñados para hacer que el proceso de desarrollo sea lo más sencillo posible, centralizando todo el entorno de trabajo y permitiendo el uso de una única herramienta para facilitar a los desarrolladores la tarea de escribir, compilar, depurar y probar el código.

Es por esta razón que el IDE recomendado por Microsoft es **Visual Studio**. Cuando esta herramienta se instala, configura todo lo necesario para un proyecto automáticamente. Además, proporciona otras herramientas integradas que, por ejemplo, **Visual Studio Code (VSC)** no incluiría por sí solo.

Algunas de las herramientas adicionales que ofrece Visual Studio son:

- **Debugger avanzado** con *time travel* mediante IntelliTrace.
- **Herramientas de monitoreo de rendimiento** para detectar cuellos de botella.
- **Herramientas de pruebas de carga** y herramientas SQL integradas.
- **Suite de pruebas integral** y analizador de código estático.
- **Integración superior con sistemas de control de versiones** como GIT, TFS y otros.
- **Herramientas de arquitectura y modelado** listas para usar.
- **Herramientas de refactorización mejoradas** superiores a VSC.
- **Soporte para muchos lenguajes y frameworks** desde el primer momento.
- **Herramientas Xamarin** para un desarrollo móvil fluido.
- **Emuladores móviles**.
- ¡Y mucho más!

> **Aclaración:**  
> El patrón para identificar si una herramienta es un **IDE** o no es que, si tiene todas estas herramientas integradas desde el momento de la instalación, entonces **es un IDE**.

## Instalación IDE

- [Visual Studio Enterprise para Windows](https://github.com/IngSoft-DA2/DA2-Tecnologia/blob/main/ide-windows.md)
- [Visual Studio Code para Mac o Windows](https://github.com/IngSoft-DA2/DA2-Tecnologia/blob/main/ide-mac.md)

## Instalación .NET

Para trabajar con .NET 8, es necesario descargarse el **.NET 8.0 SDK**. El SDK (Software Development Kit) es un conjunto de herramientas para compilar aplicaciones y características. Este kit de herramientas incluye todo lo necesario para crear aplicaciones .NET con esta versión.

Para instalarlo, sigue los siguientes pasos:

1. Dirígete a [**.NET 8.0 SDK**](https://dotnet.microsoft.com/download) para descargar el SDK correspondiente a la arquitectura de tu máquina.
2. Una vez descargado, ejecútalo para continuar con la instalación.
3. Una vez instalado, si abres:
   - **Visual Studio Enterprise** en Windows, podrás seleccionar esta versión para crear proyectos.
   - **Visual Studio Code**, abre una nueva terminal y ejecuta `dotnet --version`, y te debería indicar la versión recién instalada.
4. **En caso de que no se vea la versión correcta**, reinicia la máquina para que se termine de preparar el entorno y vuelve a ejecutar el paso 3.
