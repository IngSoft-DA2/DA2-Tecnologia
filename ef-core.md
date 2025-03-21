[Volver - EF Core](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/ef-core)

# Entity Framework Core (EF Core)

Entity Framework core (EF Core) es un framework de object-relational mapping (ORM) open-source desarrollado por Microsoft. Es una versión más ligera, extensible y multiplataforma de Entity Framework, diseñada para trabajar en proyectos de .NET. EF Core le permite a los desarrolladores trabajar con base de datos relacionales usando objetos fuertemente tipados en .NET. EF Core es una abstracción a la complejidad de interactuar directamente con una base de datos.

Este framework resuelve muchas interacciones de bajo nível con una base de datos, esto es una ventaja para los desarrolladores, ya que les permite crear, mantener e interactuar con una base de datos en unos simples pasos.

Algunos puntos fundamentales sobre ef core:

- **Database first**: Ef core permite a los desarrolladores generar las clases que se relacionarán con las tablas a partir de una base de datos. Esto quiere decir, qué a partir de una base de datos (tablas, columnas, PK, FK) se creé automaticamente un contexto para la conexión, clases y properties respectivamente.

- **Code first**: Alternativamente, los desarrolladores pueden comenzar con clases del dominio y hacer uso de herramientas de migración para generar el esquema de base de datos basandose en esas clases.

- **Soporte LINQ**: EF Core soporta LINQ (Language Integrated Query), lo cuál permite a los desarrolladores escribir queries usando una sintaxis en C# en vez de queries crudas en SQL. Esto hace que realizar queries sea mas natural e intuitivo.

- **Compatibilidad multi plataforma**: Esta diseñado para trabajar en multiples plataformas, incluyendo Windows, Linux y macOS. Esto hace que sea una opción acorde a la hora de crear aplicaciones que necesitan ser corridas en diferentes sistemas operativos.

- **Soporte de migraciones**: EF Core incluye herramientas para gestionar los cambios que sufre el esquema de la base de datos. Los desarrolladores pueden crear y aplicar migraciones para actualizar el esquema de la base de datos a lo largo que la aplicación evoluciona.

- **Mejoras de performance**: EF Core esta diseñado para ser mas rápido y mas eficiente que su versión anterior, Entity Framework 6. Logra esto mediante varias optimizaciones, como una mejor compilación de queries y reducción de overhead.

- **Soporte para diferentes motores de base de datos**: EF Core soporta un gran número de motores de base de datos, alguno de ellos son: SQL Server, SQLite, MySQL, PostgreSQL, y otros más. Esto le permite al desarrollador usar el motor de base de datos que prefiera sín cambiar ningún código en su aplicación.

- **Integración de inyección de dependencia**: EF Core interactua muy facilmente con el built-in contenedor de servicios de ASP.NET Core, haciendo que sea fácil manejar el cíclo de vída de los contextos de la base de datos y otros componentes de EF Core.

- **Extensibilidad**: EF Core esta diseñado para ser extensible, esto le permite a los desarrolladores customizar su comportamiento según las especificaciones que requieran. Esto puede ser logrado por convenciones, configuraciones o extendiendo metodos.

- **Ejecución de queries async**: EF Core soporta ejecuciones asincronas, que le permite a la aplicación ejecutar operaciones de forma asíncrona contra la base de datos, esto mejora la escalabilidad de la aplicación.

En resumen, EF Core simplifica el acceso a datos al proveer un ORM flexible y poderoso que abstrae muchas complejidades a la hora de trabajar con base de datos relacional. Este ORM puede ayudar a mejorar la productividad del equípo por la gran abstracción que ofrece.
