[🔙 Indice](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/ef-core#indice) -> [🏠 Main](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/main?tab=readme-ov-file#-temas-y-ejemplos-de-c%C3%B3digo)

# ⚡️ Entity Framework Core (EF Core)

**Entity Framework Core (EF Core)** es un framework de mapeo objeto-relacional (**ORM**, Object-Relational Mapper) open-source desarrollado por Microsoft. Representa una evolución moderna, más ligera, extensible y multiplataforma de Entity Framework clásico.

---

## 🤔 ¿Qué es un ORM?

Un **ORM** (Object-Relational Mapper) es una herramienta que permite a los desarrolladores trabajar con bases de datos relacionales usando objetos y clases propias del lenguaje de programación, en vez de escribir consultas SQL manuales. El ORM traduce automáticamente las operaciones realizadas sobre los objetos (como insertar, actualizar, eliminar o consultar) en instrucciones SQL que la base de datos entiende.

### 🚩 Ventajas de usar un ORM

- **Abstracción:** Elimina la necesidad de escribir SQL manual en la mayoría de los casos, permitiendo trabajar con datos como si fueran objetos.
- **Productividad:** Facilita el desarrollo acelerando la creación y modificación de modelos y operaciones sobre la base de datos.
- **Mantenibilidad:** El código es más legible y fácil de mantener, ya que las operaciones sobre los datos se manejan usando el propio lenguaje de programación.
- **Seguridad:** Reduce el riesgo de errores comunes como inyecciones SQL, ya que muchas operaciones están parametrizadas y abstraídas.
- **Portabilidad:** Permite cambiar el motor de base de datos con pocos cambios en el código, ya que la lógica de acceso a datos está desacoplada.

---

## ⚠️ Desventaja: Trabajar sobre una abstracción

Aunque apoyarse completamente en un ORM como EF Core trae muchas ventajas, **opera sobre una abstracción** que puede ser una limitante para el ingeniero de software en ciertos escenarios:

- **Pérdida de control sobre SQL generado:** El ORM traduce las operaciones en código a instrucciones SQL, pero a veces el SQL generado no es el más eficiente para casos complejos. Esto puede impactar en el rendimiento, especialmente cuando se requieren consultas altamente optimizadas o específicas.
- **Complejidad de problemas avanzados:** Algunos problemas, como el manejo de transacciones complejas, uso de funciones avanzadas específicas del motor de base de datos, o la optimización de queries, pueden volverse difíciles de resolver únicamente desde la abstracción del ORM.
- **Dificultad para aprovechar características avanzadas del motor:** Features como índices especializados, triggers, vistas materializadas, procedimientos almacenados, optimizaciones de concurrencia, y tuning avanzado, quedan fuera del alcance directo o requieren "escapar" del ORM y escribir SQL manual.
- **Debugging y troubleshooting:** Cuando ocurre un error o degradación de performance, puede ser más difícil rastrear el origen exacto en el SQL generado por el ORM, comparado con escribir y analizar SQL directo.

### 💡 Recomendación

Como ingeniero, es importante **conocer y entender cómo funciona el motor de base de datos subyacente** y no depender exclusivamente de la abstracción del ORM. En proyectos complejos o de alto rendimiento, conviene:

- Revisar y optimizar el SQL generado por el ORM.
- Usar consultas SQL manuales (`FromSql`, `ExecuteSqlRaw`, etc.) cuando sea necesario.
- Combinar el uso de ORM con el conocimiento profundo de la base de datos para lograr mejores resultados.

---

## 🚀 Ventajas y características principales de EF Core

- **🔄 Database First:** Permite generar las clases del modelo a partir de una base de datos existente. Ideal cuando ya tienes una base de datos definida y necesitas integrarla en tu aplicación.
- **📝 Code First:** Puedes comenzar diseñando tus clases del dominio y utilizar herramientas de migración para crear el esquema de la base de datos automáticamente. Perfecto para proyectos donde el modelo evoluciona junto al código.
- **🔎 Soporte LINQ:** Permite escribir consultas utilizando sintaxis C# a través de LINQ (Language Integrated Query), evitando la necesidad de escribir SQL crudo y mejorando la mantenibilidad del código.
- **💻 Compatibilidad multiplataforma:** Funciona en Windows, Linux y macOS, permitiendo desarrollar aplicaciones modernas en cualquier entorno.
- **🛠️ Migraciones:** Ofrece herramientas para gestionar los cambios en el esquema de la base de datos de manera controlada y versionada. Puedes crear, aplicar y revertir migraciones fácilmente.
- **⚡ Mejoras de performance:** EF Core es más rápido y eficiente que su predecesor (Entity Framework 6), gracias a optimizaciones en la compilación de consultas y la gestión de recursos.
- **🔌 Soporte para múltiples motores de base de datos:** Compatible con SQL Server, SQLite, MySQL, PostgreSQL y muchos más. Esto le da gran flexibilidad para adaptarse a diferentes requisitos de proyectos.
- **🧰 Integración con inyección de dependencias:** Se integra de forma nativa con el contenedor de servicios de ASP.NET Core, facilitando la gestión del ciclo de vida de los contextos de base de datos y la configuración de servicios.
- **🧩 Extensibilidad:** Su arquitectura permite personalizar y extender el comportamiento de EF Core mediante hooks, interceptores y otras técnicas, adaptándose a escenarios avanzados.
- **⏳ Ejecución de queries asíncronas:** Soporta operaciones asincrónicas para consultas y actualizaciones, lo cual mejora la escalabilidad y el rendimiento de aplicaciones web y APIs.

---

## 📝 Resumen

EF Core simplifica el acceso a los datos ofreciendo un ORM flexible y potente que abstrae muchas de las complejidades al trabajar con bases de datos relacionales. Sin embargo, como ingeniero, es fundamental conocer los límites de la abstracción y saber cuándo conviene operar más cerca del motor de base de datos para obtener el máximo rendimiento y flexibilidad.

> ⚡️ ¡Usa EF Core y los ORM para acelerar el desarrollo, pero no olvides tu conocimiento de base de datos para llevar tus aplicaciones al siguiente nivel! 🚀
