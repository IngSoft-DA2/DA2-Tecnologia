# DA2-Tecnologia – Diseño de Aplicaciones 2

Bienvenido/a al repositorio oficial de apoyo para la materia **Diseño de Aplicaciones 2** (DA2). Este espacio está pensado para centralizar materiales, guías y ejemplos de código que te acompañarán durante el curso.

---

## 📚 ¿Qué encontrarás aquí?

- **Guías de ambiente de desarrollo:** Para que puedas configurar tu equipo y herramientas rápidamente.
- **Ejemplos de código:** Recursos prácticos y ramificaciones (branches) específicas para cada tema tratado en clase.
- **Material teórico y enlaces útiles:** Lecturas recomendadas, cheatsheets y ayuda extra para profundizar en los conceptos.
- **Apoyo para el obligatorio:** Todo lo necesario para que avances en la entrega obligatoria de la materia.

---

## 🛠️ Preparación del ambiente local

Antes de comenzar, sigue cada una de estas guías para asegurarte de tener un entorno funcional:

- [Herramienta de desarrollo](https://github.com/daniel18acevedo/DA2-Tecnologia/blob/main/ide.md)
- [Docker](https://github.com/daniel18acevedo/DA2-Tecnologia/blob/main/docker.md)
- [Herramienta de control de versiones (Git)](./control-version-git.md)
- [Base de datos (SQL Server)](./sql-server.md)
- [Postman (testing de APIs)](./client-http.md)
- [Node.js](node-js.md)

---

## 📺 Canales recomendados

- [Amantin Band](https://www.youtube.com/@amantinband)
- [Milan Jovanovic](https://www.youtube.com/@MilanJovanovicTech)

---

## 📖 Material Teórico

Se recopila una serie de lecturas y recursos recomendados para profundizar en los principales estilos de arquitectura de software aplicados en el mundo .NET y en el desarrollo de aplicaciones empresariales modernas. Los materiales cubren Domain Driven Design (DDD), Clean Architecture, Onion Architecture, Layered Architecture, así como recursos adicionales para entender cómo aplicar estos patrones y sus ventajas. Estas lecturas son útiles tanto para desarrolladores que buscan buenas prácticas como para arquitectos de software que desean tomar decisiones informadas sobre la estructura de sus aplicaciones.

## ¿Por qué son útiles estos recursos?
- **Claridad Estructural:** Permiten comprender cómo organizar el código para mantenerlo mantenible y escalable.
- **Buenas Prácticas:** Refuerzan principios sólidos de diseño y desarrollo.
- **Casos Prácticos:** Incluyen artículos y ejemplos aplicados en .NET y ASP.NET Core.
- **Visión Crítica:** Algunos materiales discuten ventajas y desventajas, ayudando a elegir el enfoque correcto para cada caso.

### Biblografia
- **Design Patterns: Elements of Reusable Object-Oriented Software**
- **Clean Architecture: A Craftsman's Guide to Software Structure and Design (Robert C. Martin Series)**
- **Learning UML 2.0: A Pragmatic Introduction to UML**
- **Domain-Driven Design: Tackling Complexity in the Heart of Software**
- **Software Architecture in Practice (SEI Series in Software Engineering)**
- **Unit Testing Principles, Practices, and Patterns**
- **Clean Code: A Handbook of Agile Software Craftsmanship**

<details>
<summary><strong>Domain Driven Design (DDD)</strong></summary>

**Resumen:**  
El Diseño Guiado por el Dominio (DDD) es una metodología que centra el desarrollo de software en el conocimiento profundo del dominio del negocio, facilitando la colaboración entre expertos técnicos y de negocio. DDD promueve la creación de modelos ricos en significado, una separación clara entre las distintas capas del sistema y la evolución constante del software junto al negocio.

**Utilidad:**  
Estos recursos te ayudarán a entender cómo el diseño guiado por el dominio puede mejorar la alineación entre el software y los procesos de negocio, facilitando la colaboración con expertos del dominio y la evolución de la aplicación.

**Lecturas:**
- [Domain-Driven Design - Martin Fowler](https://martinfowler.com/bliki/DomainDrivenDesign.html)
- [Principios, beneficios y elementos de DDD (Español)](https://medium.com/@jonathanloscalzo/domain-driven-design-principios-beneficios-y-elementos-primera-parte-aad90f30aa35)
- [Implementando Clean Architecture y DDD en .NET Core](https://medium.com/vx-company/implementing-clean-architecture-ddd-style-in-net-core-3bc3899f5978)
- [DDD en aplicaciones ASP.NET Core](https://enlabsoftware.com/development/domain-driven-design-in-asp-net-core-applications.html)
- [Qué es un Domain Service y cuándo usarlo](https://www.linkedin.com/posts/milan-jovanovic_what-is-a-domain-service-and-when-do-you-activity-7110219840128245760-XImd?utm_source=share&utm_medium=member_desktop)
</details>

<details>
<summary><strong>Clean Architecture</strong></summary>

**Resumen:**  
Clean Architecture es una propuesta para organizar el código de forma que la lógica de negocio quede aislada de frameworks, UI, y detalles de infraestructura, facilitando el testeo, la mantenibilidad y la independencia tecnológica. Propone capas concéntricas donde el dominio y los casos de uso ocupan el centro.

**Utilidad:**  
Te permitirá conocer cómo separar responsabilidades en tu código para lograr aplicaciones mantenibles, testables y flexibles ante cambios de requerimientos o tecnología.

**Lecturas:**
- [Clean Architecture en .NET Core](https://www.bytehide.com/blog/clean-architecture-net-core)
- [Enfoque práctico a Clean Architecture en C#](https://maherz.medium.com/a-practical-approach-to-clean-architecture-in-c-net-13fe27ea23b1)
- [Por qué Clean Architecture es ideal para sistemas complejos](https://www.linkedin.com/posts/milan-jovanovic_why-is-clean-architecture-great-for-complex-activity-7105508795883098112-EnpB?utm_source=share&utm_medium=member_desktop)
- [Clean Architecture: Principios y ventajas](https://www.linkedin.com/posts/milan-jovanovic_clean-architecture-activity-7101811343980150784-5BOp?utm_source=share&utm_medium=member_desktop)
</details>

<details>
<summary><strong>Onion Architecture</strong></summary>

**Resumen:**  
Onion Architecture es un patrón que busca proteger el núcleo de la aplicación (el dominio) rodeándolo de capas que dependen hacia adentro, logrando así independencia respecto a frameworks o mecanismos externos. Favorece la mantenibilidad y la facilidad para realizar pruebas unitarias.

**Utilidad:**  
Ofrecen una visión sobre cómo proteger el núcleo de la aplicación y lograr independencia respecto a frameworks, facilitando el mantenimiento y pruebas.

**Lecturas:**
- [Introducción a Onion Architecture (Jeffrey Palermo)](https://jeffreypalermo.com/2008/07/the-onion-architecture-part-1/)
- [Onion Architecture explicada (Medium)](https://medium.com/the-software-architecture-chronicles/onion-architecture-79529d127f85)
- [Onion Architecture en sistemas C#](https://www.clarity-ventures.com/articles/c-onion-based-architecture#:~:text=Onion%20architecture%20is%20a%20software,strong%20and%20cohesive%20system%20core.)
- [Onion Architecture en ASP.NET Core](https://code-maze.com/onion-architecture-in-aspnetcore/)
</details>

<details>
<summary><strong>Layered Architecture</strong></summary>

**Resumen:**  
La arquitectura en capas es uno de los patrones más tradicionales y ampliamente adoptados en el desarrollo de software empresarial. Se basa en dividir la aplicación en capas bien definidas (por ejemplo: presentación, lógica, acceso a datos), permitiendo separar responsabilidades y facilitar el mantenimiento.

**Utilidad:**  
Ayudan a comprender la tradicional separación de responsabilidades en capas (presentación, lógica de negocio, datos) y cómo aplicar este patrón en proyectos reales.

**Lecturas:**
- [Arquitectura en capas con ASP.NET Core y EF Core](https://medium.com/aspnetrun/layered-architecture-with-asp-net-core-entity-framework-core-and-razor-pages-53a54c4028e3)
- [Comparativa Onion vs Three Layer Architecture](https://medium.com/swlh/onion-architecture-vs-three-layer-59a9ba2c6e02)
- [El patrón de arquitectura en capas](https://medium.com/kayvan-kaseb/the-layered-architecture-pattern-in-software-architecture-324922d381ad)
</details>

<details>
<summary><strong>Vertical Slice Architecture</strong></summary>

**Resumen:**  
Vertical Slice Architecture propone organizar el código por funcionalidades o "slices" verticales (casos de uso), en lugar de por capas técnicas. Cada slice abarca desde la entrada (por ejemplo, un endpoint) hasta el dominio y la persistencia, permitiendo que cada funcionalidad evolucione de forma independiente.

**Utilidad:**  
Descubrirás una alternativa moderna a la organización tradicional por capas, enfocada en separar la lógica de negocio por caso de uso o funcionalidad. Esto facilita la mantenibilidad, la escalabilidad y la entrega incremental de funcionalidades, especialmente útil para equipos ágiles y proyectos que requieren adaptación constante.

**Lecturas:**
- [Vertical Slice Architecture](https://www.youtube.com/watch?v=dQdXHRkePr8)
- [The problem with Clean Architecture: Vertical Slices](https://medium.com/design-microservices-architecture-with-patterns/the-problem-with-clean-architecture-vertical-slices-111537c0ffcb#:~:text=In%20a%20Vertical%20Slice%20architecture,are%20delivered%20over%20the%20web.)
</details>

<details>
<summary><strong>Lecturas adicionales y recursos generales</strong></summary>

<details>
<summary><strong>Event Driven Design:</strong></summary>

**Resumen:**
Event Driven Design Architecture propone una arquitectura donde los componentes del sistema interactúan reaccionando a eventos que indican la ocurrencia de acciones relevantes.

**Utilidad:**
Es útil para crear sistemas flexibles y escalables, permitiendo que los componentes se comuniquen de forma desacoplada y reaccionen eficientemente a eventos relevantes en tiempo real.

**Lecturas:**
-[Cómo usarlo](https://www.youtube.com/watch?v=BimfDeDV4yU)
-[Diferencia de tipos de eventos](https://www.youtube.com/watch?v=K806a-rWE2g)
</details>


**Resumen:**  
Estos recursos complementan los estilos arquitectónicos principales, presentando guías y reflexiones de expertos sobre patrones, buenas prácticas y decisiones de diseño que afectan a la arquitectura de aplicaciones modernas.

**Utilidad:**  
Complementan los conceptos, presentan discusiones críticas sobre los patrones y muestran ejemplos prácticos y recomendaciones de expertos reconocidos.

**Lecturas:**
- [Guía de arquitecturas comunes de aplicaciones web en Azure (.NET)](https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures)
- [Principios de Layering (Martin Fowler)](https://martinfowler.com/bliki/LayeringPrinciples.html)
- [Presentación, Dominio y Capa de Datos - Martin Fowler](https://martinfowler.com/bliki/PresentationDomainDataLayering.html)
</details>

---

## 🚀 Temas y Ejemplos de Código

### Configuración y Buenas Prácticas

- [Creación del repositorio](https://github.com/daniel18acevedo/DA2-Tecnologia/tree/repo-creation)
- [Github Self Hosted Runner](https://github.com/IngSoft-DA2/DA2-Tecnologia/blob/main/github-self-hosted-runner-docker.md)
- [Configuración de repositorio](https://github.com/daniel18acevedo/DA2-Tecnologia/tree/repo-configuration)
- [Creación de Pull Requests](https://github.com/daniel18acevedo/DA2-Tecnologia/tree/pr-creation)
- [Prácticas Clean Code](https://github.com/daniel18acevedo/DA2-Tecnologia/blob/main/clean-code.md)
- [Cheat sheet](https://github.com/daniel18acevedo/DA2-Tecnologia/blob/main/cheat-sheet.md)

### Primera Parte: .NET WebAPI

- [Web API](https://github.com/daniel18acevedo/DA2-Tecnologia/tree/web-api)
- [Pruebas unitarias e integración](https://github.com/daniel18acevedo/DA2-Tecnologia/tree/unit-testing)
- [Inyección de dependencias](https://github.com/daniel18acevedo/DA2-Tecnologia/tree/dependency-injection)
- [Entity Framework Core](https://github.com/daniel18acevedo/DA2-Tecnologia/tree/ef-core)
- [Filters](https://github.com/daniel18acevedo/DA2-Tecnologia/tree/filters)
- [Reflection](https://github.com/daniel18acevedo/DA2-Tecnologia/tree/reflection)

### Segunda Parte: SPAs y Angular

- [Angular](https://github.com/daniel18acevedo/DA2-Tecnologia/tree/angular)

---

> **Nota:** El código para cada tema se encuentra en su propia branch, facilitando la búsqueda y el estudio independiente de cada tópico.

## ⚠️ Aclaración Importante

El código presente fue desarrollado en contexto de clase, priorizando la comprensión y la practicidad. **No todo el código debe tomarse como referencia de producción**; su principal objetivo es didáctico.

---

## 🔗 Links de Interés

- [Puerto por defecto cambiado en Docker](https://learn.microsoft.com/en-us/dotnet/core/compatibility/containers/8.0/aspnet-port)
- [Comparación case-insensitive de strings](https://github.com/npgsql/efcore.pg/issues/1498)
- [Creación de links simbólicos (npm link)](https://github.com/nwheels-io/NuLink)
- [Const vs Readonly en C#](https://josipmisko.com/posts/c-sharp-const-vs-readonly)
- [Sintaxis de C#](https://www.tutorialspoint.com/csharp/index.htm)
- [LINQ](https://learn.microsoft.com/en-us/dotnet/csharp/linq/)
- [Expresiones Lambda](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-expressions)

---

## 👨‍🏫 Docentes

- 👾 Daniel Acevedo
- 👾 Federico Gonzalez

---

¡Esperamos que este repositorio te ayude a sacarle el máximo provecho al curso!
