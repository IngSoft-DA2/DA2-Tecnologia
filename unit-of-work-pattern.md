[🔙 Indice](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/ef-core#indice) → [🏠 Main](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/main?tab=readme-ov-file#da2-tecnologia--dise%C3%B1o-de-aplicaciones-2)

# 🧩 Patrón Unit of Work

---

## 🤔 ¿Qué problema resuelve el patrón Unit of Work?

En aplicaciones que interactúan con una base de datos, es común ejecutar múltiples operaciones de escritura o actualización en una misma transacción. Sin embargo, si cada operación se ejecuta por separado, pueden surgir problemas como:

- **Inconsistencias de datos:** Si una parte de las operaciones falla, el resto puede dejar la base en un estado inesperado.
- **Manejo manual de transacciones:** El código de negocio debe ocuparse de abrir, confirmar o deshacer transacciones.
- **Acoplamiento:** El manejo de las operaciones y las transacciones queda repartido y mezclado en distintos servicios o repositorios.

---

## 🛠️ ¿Cómo lo resuelve el patrón Unit of Work?

El patrón Unit of Work propone **agrupar todas las operaciones de acceso a datos que forman parte de una misma transacción** en un único objeto coordinador. Este objeto:

1. **Registra los cambios** (agregados, modificaciones, eliminaciones) en las entidades durante el ciclo de vida de la transacción.
2. **Aplica todos los cambios juntos** al llamar a un método como `Commit()`, lo que asegura que todas las operaciones se ejecuten (o se deshagan) en bloque.

Ejemplo conceptual en C#:

```csharp
public interface IUnitOfWork
{
    IUserRepository Users { get; }
    IOrderRepository Orders { get; }
    Task CommitAsync(); // Aplica todos los cambios en una única transacción
}
```

La implementación se encarga de iniciar la transacción, coordinar los repositorios y confirmar o revertir los cambios.

---

## 👨‍💻 Ejemplo de implementación y sincronización

Supongamos que estamos usando Entity Framework Core, donde el `DbContext` ya actúa como Unit of Work.  
La instancia de contexto debe ser **la misma** para todos los repositorios y para el Unit of Work.  
Esto se logra fácilmente usando **inyección de dependencias (DI)**.

### 1. Interfaces de repositorio y Unit of Work

```csharp
public interface IUserRepository
{
    Task<User> GetByIdAsync(Guid id);
    void Add(User user);
    // Otros métodos...
}

public interface IOrderRepository
{
    Task<Order> GetByIdAsync(Guid id);
    void Add(Order order);
    // Otros métodos...
}

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    IOrderRepository Orders { get; }
    Task CommitAsync(); // Guarda todos los cambios en la base de datos
}
```

### 2. Implementaciones usando el mismo DbContext

```csharp
public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    public UserRepository(AppDbContext context) => _context = context;
    public void Add(User user) => _context.Users.Add(user);
    // Otros métodos...
}

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;
    public OrderRepository(AppDbContext context) => _context = context;
    public void Add(Order order) => _context.Orders.Add(order);
    // Otros métodos...
}
```

> ⚡ **Importante:**  
> El contexto (`AppDbContext`) debe ser la misma instancia para todos los repositorios y el Unit of Work dentro de una misma request/transacción.

### 3. Implementación del Unit of Work (repositorios inyectados)

El Unit of Work recibe los repositorios y el contexto por inyección de dependencias:

```csharp
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public IUserRepository Users { get; }
    public IOrderRepository Orders { get; }

    public UnitOfWork(
        AppDbContext context,
        IUserRepository userRepository,
        IOrderRepository orderRepository)
    {
        _context = context;
        Users = userRepository;
        Orders = orderRepository;
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
```

---

## 🧩 Configuración de inyección de dependencia

La clave para que todos usen la **misma instancia de contexto** y los repositorios estén sincronizados es la configuración del DI container.

En una app ASP.NET Core, la configuración típica en `Startup.cs` o `Program.cs` sería:

```csharp
services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString)); // Scoped por defecto

services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<IOrderRepository, OrderRepository>();
services.AddScoped<IUnitOfWork, UnitOfWork>();
```

- `AddDbContext<AppDbContext>()` registra el contexto como **Scoped** (una instancia por request HTTP).
- Los repositorios y el UnitOfWork también se registran como **Scoped**.
- Cuando el DI container resuelve los objetos, se asegura de que todos reciban **la misma instancia de AppDbContext** para la misma request.

> 📝 **Explicación:**  
> Cuando tu controlador o servicio solicita un `IUnitOfWork`, el DI container crea una instancia de UnitOfWork y le pasa los repositorios, que a su vez comparten la misma instancia de `AppDbContext`.  
> Así, todos los cambios hechos a través de los repositorios quedan en el mismo contexto y se sincronizan juntos en una sola transacción con `CommitAsync()`.

### ⚠️ ¿Pueden UnitOfWork y los repositorios ser Transient?

Sí, **pueden usarse como Transient**, pero debes asegurarte de que el contexto (`AppDbContext`) sigue siendo **Scoped** (no Transient), y que todos los repositorios y el UnitOfWork reciben exactamente la misma instancia de contexto para lograr la sincronización de los cambios.  
Si cada repositorio o UnitOfWork recibe su propia instancia de contexto (por ser Transient), la sincronización y atomicidad de la transacción se pierde.

Ejemplo de registro como Transient:

```csharp
services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString)); // Scoped por defecto

services.AddTransient<IUserRepository, UserRepository>();
services.AddTransient<IOrderRepository, OrderRepository>();
services.AddTransient<IUnitOfWork, UnitOfWork>();
```

> ⚡ **Conclusión:**  
> Lo fundamental es que **todos los objetos de una misma operación/request compartan la misma instancia de AppDbContext**. Por eso, normalmente se recomienda que UnitOfWork y los repositorios sean **Scoped** como el contexto.

---

### 4. Uso en un servicio

```csharp
public class TransactionService
{
    private readonly IUnitOfWork _unitOfWork;

    public TransactionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task RegisterUserAndOrderAsync(User user, Order order)
    {
        _unitOfWork.Users.Add(user);
        _unitOfWork.Orders.Add(order);

        // Sincroniza las operaciones: ambas se guardan juntas o ninguna se guarda
        await _unitOfWork.CommitAsync();
    }
}
```

---

## ✅ Ventajas del patrón Unit of Work

- **Consistencia transaccional:** Todas las operaciones se confirman o se deshacen juntas, manteniendo la integridad de los datos.
- **Centralización:** El manejo de la transacción está en un solo lugar.
- **Desacoplamiento:** El código de negocio no necesita gestionar transacciones directamente.
- **Testabilidad:** Es más fácil simular el comportamiento de operaciones en bloque en los tests.

---

## ⚠️ Desventajas y consideraciones

- **Complejidad adicional:** Puede resultar innecesario en aplicaciones simples o con pocas operaciones transaccionales.
- **Sobrecarga:** Si los repositorios ya gestionan transacciones por sí mismos, el Unit of Work puede ser redundante.
- **Dependencia con el ORM:** Algunos ORMs como Entity Framework ya implementan el patrón internamente (por ejemplo, el DbContext actúa como Unit of Work), por lo que crear una capa adicional puede duplicar responsabilidades.

---

## 🗺️ Contexto y uso

El patrón Unit of Work es útil cuando:

- Hay múltiples repositorios y servicios que deben participar en una misma transacción.
- La aplicación necesita garantizar que los cambios se confirmen o reviertan en bloque.
- Se busca una arquitectura limpia y coherente en el manejo de datos.

En muchos frameworks modernos, el patrón Unit of Work está implementado de forma nativa (por ejemplo, el DbContext de EF Core). Sin embargo, es común combinarlo con el patrón de repositorio para una arquitectura más robusta.

---

## 📖 Lecturas recomendadas

- [Martin Fowler: Unit of Work](https://martinfowler.com/eaaCatalog/unitOfWork.html)
- [Microsoft Docs: Implementing the unit of work pattern in ASP.NET Core](https://learn.microsoft.com/en-us/archive/msdn-magazine/2011/february/data-points-unit-of-work-versus-repository-pattern)

---

<p align="center">
  <b>El patrón Unit of Work permite gestionar transacciones complejas y mantener la integridad de los datos.<br>
  Úsalo cuando la lógica de negocio requiera coordinar múltiples cambios en una única operación.</b>
</p>
