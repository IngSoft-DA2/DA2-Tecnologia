[⬅️ Volver - Pruebas Unitarias](https://github.com/IngSoft-DA2/DA2-Tecnologia/blob/unit-testing/README.md) > [⬅️ Volver - Configuración Pruebas Unitarias](https://github.com/IngSoft-DA2/DA2-Tecnologia/blob/unit-testing/config-unit-test-project.md)

# 🛠️ Configuración de Proyecto de Prueba Unitaria en Visual Studio

A continuación, se describe cómo agregar los paquetes recomendados **Moq** y **FluentAssertions** a tu proyecto de pruebas usando Visual Studio.  
¡Sigue estos pasos para potenciar la calidad y la legibilidad de tus pruebas! 🚀

---

## 🎭 Moq

1. **Haz click derecho** sobre el proyecto de prueba  
   → Selecciona **Manage NuGet Packages...**  
   ![Manage NuGet Packages opción](./images/image-17.png)

2. **Busca `Moq`** en la barra de búsqueda  
   ![Buscar Moq](./images/image-18.png)

3. **Selecciona e instala** la última versión.  
   - Asegúrate que la fuente del paquete sea `nuget.org`  
   ![Instalar versión](./images/image-19.png)

4. **Verifica la instalación:**  
   - El paquete debe aparecer listado en la sección `Packages` del proyecto de prueba  
   ![Chequear instalación](./images/image-20.png)

---

## ✨ FluentAssertions

> **Recomendación:**  
> Agrega **FluentAssertions** para obtener una sintaxis más expresiva y legible en tus assertions.  
> Esto mejorará la calidad y mantenibilidad de tus pruebas unitarias.

1. **Haz click derecho** sobre el proyecto de prueba  
   → Selecciona **Manage NuGet Packages...**  
   ![Manage NuGet Packages opción](./images/image-17.png)

2. **Busca `FluentAssertions`** en la barra de búsqueda  
   ![Buscar FluentAssertions](./images/image-21.png)

3. **Selecciona e instala** la última versión.  
   - Asegúrate que la fuente del paquete sea `nuget.org`  
   ![Instalar versión](./images/image-22.png)

4. **Verifica la instalación:**  
   - El paquete debe aparecer listado en la sección `Packages` del proyecto de prueba  
   ![Chequear instalación](./images/image-23.png)

---

¡Listo!  
Ya tienes integrados los paquetes recomendados para escribir **pruebas unitarias profesionales y efectivas** en tu proyecto. 💡
