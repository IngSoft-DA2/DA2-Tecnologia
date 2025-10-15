[🔙 Indice](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/ef-core#indice) → [🏠 Main](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/main?tab=readme-ov-file#da2-tecnologia--dise%C3%B1o-de-aplicaciones-2)

# 📚 Patrón de Repositorio

---

## 🤔 ¿Qué problema resuelve el patrón de repositorio?

En el desarrollo de aplicaciones, especialmente aquellas que interactúan con una base de datos, suele haber una **mezcla de lógica de acceso a datos** con la lógica de negocio. Esto genera varios problemas:

- **Acoplamiento alto:** El código de acceso a datos se mezcla con el resto de la aplicación, dificultando cambios de tecnología (por ejemplo, pasar de Entity Framework a Dapper, o de una base relacional a una NoSQL).
- **Dificultad para testear:** Es más complicado hacer pruebas unitarias porque el código depende directamente de la base de datos.
- **Duplicación de código:** Las operaciones comunes (agregar, modificar, eliminar, buscar) suelen repetirse en distintos lugares de la aplicación.

---

## 🛠️ ¿Cómo lo resuelve el patrón de repositorio?

El patrón de repositorio propone **crear una capa intermedia** entre la lógica de negocio y la fuente de datos. Esta capa se encarga de todas las operaciones de acceso y manipulación de datos, exponiendo una interfaz simple y desacoplada.

Por ejemplo, en lugar de que los servicios de negocio hagan queries SQL o llamen directamente a Entity Framework, interactúan con una interfaz de repositorio:

```csharp
public interface IUserRepository
{
    User GetById(Guid id);
    IList<User> GetAll();
    void Add(User user);
    void Update(User user);
    void Delete(Guid id);
}
```

La implementación puede usar EF Core, Dapper, SQL puro, archivos, APIs externas, etc., pero el resto de la aplicación no necesita saberlo.

---

## ✅ Ventajas del patrón de repositorio

- **Desacoplamiento:** La lógica de negocio no depende de la tecnología de acceso a datos.
- **Testabilidad:** Es fácil reemplazar la implementación por mocks o fakes en los tests.
- **Centralización:** Las operaciones de acceso a datos están concentradas y reutilizables.
- **Mantenibilidad:** Cambiar el motor de base de datos o el ORM es mucho más sencillo.
- **Abstracción:** Permite definir contratos claros y consistentes para el acceso a datos.

---

## ⚠️ Desventajas y consideraciones

- **Sobrecarga innecesaria:** En proyectos pequeños, puede ser demasiado complejo y agregar código que no aporta valor.
- **Pérdida de funcionalidades avanzadas:** Puede ocultar características específicas del ORM o de la base de datos, como consultas especializadas, transacciones avanzadas, etc.
- **Duplicación de abstracciones:** Si el ORM ya provee un patrón similar (por ejemplo, DbSet en EF Core), el repositorio puede ser redundante.

---

## 🗺️ Contexto y uso

El patrón de repositorio es ampliamente usado en arquitecturas de aplicaciones empresariales (.NET, Java, Python, etc.), y suele combinarse con otros patrones como el **Unit of Work**, **Service Layer**, o **CQRS**.

No es obligatorio en todos los sistemas, pero es recomendable cuando:
- Se espera crecimiento o cambios en la tecnología de acceso a datos.
- Se requiere una arquitectura limpia y testable.
- El equipo necesita mantener la flexibilidad y la claridad en el código.

---

## 📖 Lecturas recomendadas

- [Martin Fowler: Repository Pattern](https://martinfowler.com/eaaCatalog/repository.html)
- [Microsoft Docs: Repository pattern](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/repository-pattern)

---

<p align="center">
  <b>El patrón de repositorio ayuda a construir aplicaciones más limpias, flexibles y testeables.<br>
  ¡Úsalo cuando la complejidad o la escalabilidad lo ameriten!</b>
</p>
