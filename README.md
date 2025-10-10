[🏠 Main](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/main?tab=readme-ov-file#-temas-y-ejemplos-de-c%C3%B3digo)

# 🪞 Reflection en .NET

## 🎯 ¿Qué es Reflection?

**Reflection** es la habilidad de un lenguaje de inspeccionar e invocar dinámicamente a clases, métodos, atributos, etc. en tiempo de ejecución.

Para **.NET**, es la habilidad de un programa de poder **autoexaminarse** 🔍 con el objetivo de encontrar ensamblados (.dll), módulos, o información de tipos en tiempo de ejecución. A nivel de código vamos a tener clases y objetos que nos van a permitir referenciar a ensamblados, y a los tipos que se encuentren contenidos.

### 🪞 El Espejo del Código
Se dice que un programa se **refleja a sí mismo** (de ahí el término reflection), a partir de extraer metadata de sus assemblies y de usar esa metadata para ciertos fines. Ya sea para informarle al usuario o para modificar su comportamiento.

### 🚀 Posibilidades con Reflection
- 🔍 Obtener información detallada de un objeto y sus métodos
- 🏗️ Crear objetos dinámicamente 
- ⚡ Invocar métodos en tiempo de ejecución
- 🔓 Todo esto sin necesidad de referencias directas al ensamblado

### 📦 Namespace Clave: `System.Reflection`
Específicamente lo que nos permite usar Reflection es el namespace `System.Reflection`, que contiene clases e interfaces que nos permiten manejar todo lo mencionado anteriormente: ensamblados, tipos, métodos, estado, crear objetos, invocar métodos, etc.

## 🏗️ Estructura de un Assembly/Ensamblado

Un **ensamblado** es el resultado de compilar el programa, generalmente lo podremos ver como un `.dll` 📦. Es la **unidad mínima** en .NET.

### 📊 Jerarquía de Componentes
Los assemblies contienen paquetes, los paquetes contienen tipos y los tipos contienen estados. Reflection provee clases para encapsular estos elementos.

### ⚡ Capacidades Dinámicas
Como se dijo, es posible utilizar reflection para:
- 🏭 Crear dinámicamente instancias de un tipo
- 🔍 Obtener el tipo de un objeto existente
- 📞 Invocar métodos de manera dinámica
- 🎛️ Acceder a atributos de forma dinámica

<p align="center">
  <img src="./images/image-9.png"/>
</p>
<p align="center">
📐 <strong>[Estructura de un assembly]</strong>
</p>

## ✅ Ventajas de Reflection

- 🔌 **Desacoplamiento a tipos externos**
- 🔍 **Inspección de tipos de forma dinámica**

## ⚠️ Desventajas de Reflection

### 🐌 Performance Overhead
Las operaciones de reflection son generalmente **lentas** en comparación con operaciones realizadas directamente sobre tipos conocidos en tiempo de compilación. Esto es porque reflection involucra el descubrimiento de tipos en tiempo de ejecución y la inspección de metadata, que puede inferir en performance overhead, específicamente en aplicaciones performance-sensitive como aplicaciones de salud o económicas.

### 🧩 Complejidad Agregada
El código de reflection puede ser **difícil de mantener y debugguear**. Todas las tareas de reflection no son tan transparentes.

### 🚫 Falta de Seguridad por Compilación
Dado que reflection puede pasar chequeos en tiempo de compilación, los errores relacionados a mismatches o falta de miembros ocurrirán en **tiempo de ejecución** causando excepciones no esperadas.

### 🔒 Riesgos de Seguridad
El uso de assemblies por reflection propone una vulnerabilidad ya que estos assemblies pueden **inspeccionar nuestra aplicación**.

## 🎯 Conclusión

En resumen, a pesar de las desventajas, reflection sigue siendo una **herramienta poderosa** 💪, particularmente en escenarios donde la flexibilidad que trae en tiempo de ejecución y el dinamismo sobre comportamientos es esencial. Sin embargo, es importante usar reflection de forma **cautelosa** ⚠️ y considerar caminos alternativos para mitigar posibles problemas.

## 💡 Ejemplo Práctico
<img width="950" height="389" alt="image" src="https://github.com/user-attachments/assets/042341cb-a67b-4f2e-ac49-58830fa3b8d7" />

---

## 📚 Proyectos Prácticos en este Repositorio

### 🔍 1. Reflection Autoanálisis
Aprende los fundamentos de reflection explorando y analizando objetos en tiempo de ejecución. 

- 🔗 [Ejemplo 1](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/reflection/1-%20Reflection%20autoanalisis)

### 📦 2. Reflection Cargar Assembly
Descubre cómo cargar y utilizar assemblies externos dinámicamente.

- 🔗 [Ejemplo 2](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/reflection/2-%20Reflection%20cargar%20assembly)

### 🎯 3. Reflection Práctico
Implementa algoritmos de ordenamiento cargados dinámicamente usando reflection.

- 🔗 [Ejemplo 3](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/reflection/3-%20Reflection%20practico)


