[🔙 Indice](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/ef-core#indice) -> [🏠 Main](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/main?tab=readme-ov-file#-temas-y-ejemplos-de-c%C3%B3digo)

# ⚡️ Entity Framework Core (EF Core)

**Entity Framework Core (EF Core)** es un moderno ORM (Object Relational Mapper) para .NET. Permite interactuar con bases de datos relacionales (SQL Server, PostgreSQL, MySQL, SQLite, entre otros) usando objetos y clases de C# en lugar de escribir código SQL manualmente. EF Core traduce las operaciones del código a comandos SQL y gestiona el mapeo entre las entidades del modelo y las tablas de la base de datos.

EF Core es la evolución de Entity Framework clásico, con mejoras de rendimiento, soporte multiplataforma (.NET Core, .NET 5/6/7/8), y una arquitectura modular y ligera.  
Se utiliza en aplicaciones web, APIs, escritorio, móviles y microservicios, entre otros escenarios.

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

## 🏗️ ¿Qué es el DbContext?

`DbContext` es el componente central de EF Core.  
Representa una sesión con la base de datos y se encarga de:

- Gestionar la conexión a la base de datos.
- Realizar consultas y modificaciones.
- Detectar cambios en las entidades.
- Aplicar operaciones en la base de datos (insert, update, delete).
- Manejar el seguimiento de entidades y la sincronización con el modelo del dominio.

En la práctica, el `DbContext` es la "puerta de entrada" a toda la funcionalidad de EF Core, tanto para consultas como para escritura.

---

## ⏳ ¿Cuándo se abre y cierra la conexión a la base de datos?

EF Core sigue el principio de **"conexión tardía y cierre temprano"** ("late open, early close").  
Esto significa que la conexión física a la base de datos **solo se abre cuando realmente se necesita** (cuando se ejecuta una operación contra la base) y **se cierra inmediatamente después** de terminar la operación.  
Esto optimiza el uso de recursos y permite que el pool de conexiones de ADO.NET gestione la reutilización eficiente de conexiones.

### 🔍 Consultas de datos

- **Definición del query:** Cuando defines una consulta, por ejemplo con LINQ, la conexión aún **no está abierta**.
    ```csharp
    var query = context.Products.Where(p => p.Price > 10);
    // La conexión aún no está abierta
    ```
- **Ejecución/Enumeración:** La conexión se **abre justo antes** de ejecutar la consulta, por ejemplo al llamar a `.ToList()`, `.ToArray()`, o al iterar con `foreach`.
    ```csharp
    var products = query.ToList();
    // La conexión se abrió antes de ToList() y se cerró justo después
    ```

### 💾 Guardar cambios

- La conexión se **abre** al llamar a `SaveChanges()` o `SaveChangesAsync()`.  
- EF Core inicia una transacción implícita, ejecuta todas las operaciones, y **cierra la conexión** una vez que la transacción se confirma o se revierte.

### 🔄 Control manual de la conexión

- Puedes abrir y cerrar la conexión manualmente si necesitas realizar varias operaciones consecutivas que deban compartir la misma conexión (por ejemplo, procesamiento batch).
    ```csharp
    context.Database.OpenConnection();
    try
    {
        // ... múltiples operaciones ...
    }
    finally
    {
        context.Database.CloseConnection();
    }
    ```
- En este caso, eres responsable de la gestión del ciclo de vida de la conexión.

### 🛡️ Transacciones explícitas

- Si usas `context.Database.BeginTransaction()`, la conexión física se **abre** y se mantiene abierta hasta que la transacción se confirme (`Commit`) o se revierta (`Rollback`).

### 🏊‍♂️ Pool de conexiones

- La conexión física real es gestionada por el **pool de conexiones de ADO.NET**.  
- Cuando EF Core "cierra" la conexión, en realidad la devuelve al pool para su reutilización en futuras operaciones, evitando el costo de crear nuevas conexiones cada vez.

> 📚 Más información:  
> - [SQL Server Connection Pooling - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/framework/data/adonet/sql-server-connection-pooling)
> - [DbContext Lifetime: Scoped vs Transient (Google Search)](https://www.google.com/search?q=efcore+dbcontext+should+have+transient+lifetime+.net)

---

## 🔄 Ciclo de vida del DbContext: ¿Scoped, Transient o Singleton?

El ciclo de vida correcto del `DbContext` es **crítico** para asegurar la eficiencia, seguridad y consistencia de la aplicación.

- **Scoped (recomendado):**  
  En aplicaciones web (ASP.NET Core), lo más común es registrar el `DbContext` como Scoped (`AddDbContext<T>()` por defecto), lo que significa que se crea una instancia por cada request HTTP.  
  Esto asegura que todas las operaciones realizadas durante una petición compartan el mismo contexto, permitiendo transacciones y seguimiento de cambios correcto.

- **Transient:**  
  El `DbContext` también puede ser registrado como Transient (una instancia nueva cada vez que se requiere).  
  Esto puede ser útil para tareas en background, automatismos o escenarios donde se necesita un contexto completamente independiente.  
  Sin embargo, **no se recomienda en aplicaciones web para manejar requests** porque puede causar problemas de concurrencia, duplicación de conexiones y pérdida de seguimiento de entidades y transacciones.
  > 📚 Ver más sobre este tema: [¿DbContext debe ser Transient?](https://www.google.com/search?q=efcore+dbcontext+should+have+transient+lifetime+.net)

- **Singleton:**  
  Registrar el DbContext como Singleton es usualmente **incorrecto** y peligroso, ya que puede causar que múltiples threads compartan la misma instancia, generando errores de concurrencia y corrupción de datos.

> ⚡ **Conclusión:**  
> Usa **Scoped** para la mayoría de los escenarios en aplicaciones web.  
> Considera **Transient** solo para casos de uso muy específicos y aislados, asegurándote de entender las implicancias y riesgos.

---

## 🧩 Integración con Dependency Injection (DI)

EF Core se integra perfectamente con el sistema de inyección de dependencias de .NET.

Ejemplo en ASP.NET Core:

```csharp
services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString)); // Scoped por defecto
```

Esta configuración asegura que cada request reciba su propio `DbContext` y que todos los repositorios y servicios que lo utilicen compartan la misma instancia, permitiendo transacciones seguras y seguimiento de cambios.

---

## ⚠️ Buenas prácticas y consideraciones

- **No compartas instancias de DbContext entre threads.**
- **No uses DbContext como singleton en aplicaciones multiusuario.**
- **Dispose el DbContext correctamente (usando DI, esto ocurre automáticamente).**
- **Usa un DbContext por unidad de trabajo (request, operación, o transacción lógica).**
- **Entiende cuándo la conexión a la base está realmente abierta y cerrada para evitar fugas de recursos o bloqueos innecesarios.**

---

## 📝 Resumen

EF Core simplifica el acceso a los datos ofreciendo un ORM flexible y potente que abstrae muchas de las complejidades al trabajar con bases de datos relacionales. Sin embargo, como ingeniero, es fundamental conocer los límites de la abstracción y saber cuándo conviene operar más cerca del motor de base de datos para obtener el máximo rendimiento y flexibilidad.

> ⚡️ ¡Usa EF Core y los ORM para acelerar el desarrollo, pero no olvides tu conocimiento de base de datos para llevar tus aplicaciones al siguiente nivel! 🚀

---

## 📚 Más información

- [Documentación oficial de EF Core](https://learn.microsoft.com/en-us/ef/core/)
- [DbContext Lifetime: Scoped vs Transient](https://www.google.com/search?q=efcore+dbcontext+should+have+transient+lifetime+.net)
- [Proveedores de bases de datos en EF Core](https://learn.microsoft.com/en-us/ef/core/providers/?tabs=dotnet-core-cli)
- [Migraciones con EF Core](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli)
- [SQL Server Connection Pooling - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/framework/data/adonet/sql-server-connection-pooling)
