[🔙 Volver - Main](./README.md)

# 🗄️ ¿Qué es una Base de Datos en un Servidor? ¿Y qué es SQL?

## 📦 ¿Qué es una base de datos servidor?

Una **base de datos servidor** es un sistema de software especializado que gestiona, almacena y organiza datos de manera centralizada, accesible a través de una red. Este tipo de base de datos suele ejecutarse en un servidor dedicado (local o en la nube), lo que permite que múltiples aplicaciones o usuarios accedan, consulten y modifiquen la información de manera eficiente y segura.

Ejemplos populares de sistemas gestores de bases de datos (DBMS) servidores incluyen:
- **SQL Server** (Microsoft)
- **MySQL**
- **PostgreSQL**
- **Oracle Database**
- **MongoDB** (NoSQL)

---

## 🤔 ¿Qué es SQL?

**SQL** (Structured Query Language, o Lenguaje de Consulta Estructurada) es el lenguaje estándar utilizado para interactuar con bases de datos relacionales. Permite realizar operaciones como:
- Consultar datos: `SELECT`
- Insertar datos: `INSERT`
- Actualizar datos: `UPDATE`
- Eliminar datos: `DELETE`
- Crear o modificar estructuras: `CREATE`, `ALTER`

Ejemplo simple de consulta SQL:
```sql
SELECT nombre, correo FROM usuarios WHERE activo = 1;
```

---

## 📝 Ejemplos de consultas SQL (crear, actualizar, eliminar)

### Crear (INSERT)
```sql
INSERT INTO empleados (nombre, puesto, salario)
VALUES ('Juan Pérez', 'Desarrollador', 50000);
```

### Actualizar (UPDATE)
```sql
UPDATE empleados
SET salario = 55000
WHERE nombre = 'Juan Pérez';
```

### Eliminar (DELETE)
```sql
DELETE FROM empleados
WHERE nombre = 'Juan Pérez';
```

---

## 🛠️ ¿Cómo interactuar con una base de datos? (CLI y GUI)

Existen dos formas principales para interactuar con una base de datos servidor: mediante línea de comandos (CLI) o mediante una interfaz gráfica (GUI).

### 1. CLI (Command Line Interface)

La **CLI** permite ejecutar comandos SQL directamente desde una terminal o consola.  
**Ventajas:** control total, automatización, scripts.

#### Ejemplo CLI en SQL Server (usando sqlcmd):

- Conectarse a la base de datos:
  ```powershell
  sqlcmd -S localhost -U sa -P TuPassword -d NombreBD
  ```
- Ejecutar una consulta:
  ```sql
  SELECT * FROM empleados;
  GO
  ```
- Insertar datos:
  ```sql
  INSERT INTO empleados (nombre, puesto, salario) VALUES ('Ana Gómez', 'Analista', 60000);
  GO
  ```
- Actualizar datos:
  ```sql
  UPDATE empleados SET salario = 65000 WHERE nombre = 'Ana Gómez';
  GO
  ```
- Eliminar datos:
  ```sql
  DELETE FROM empleados WHERE nombre = 'Ana Gómez';
  GO
  ```

#### Ejemplo CLI en PostgreSQL:
```bash
psql -U usuario -h servidor -d basededatos
```
- Ejecutar una consulta:
  ```sql
  SELECT * FROM empleados;
  ```
- Insertar datos:
  ```sql
  INSERT INTO empleados (nombre, puesto, salario) VALUES ('Ana Gómez', 'Analista', 60000);
  ```
- Actualizar datos:
  ```sql
  UPDATE empleados SET salario = 65000 WHERE nombre = 'Ana Gómez';
  ```
- Eliminar datos:
  ```sql
  DELETE FROM empleados WHERE nombre = 'Ana Gómez';
  ```

---

### 2. GUI (Graphical User Interface)

Las **herramientas GUI** ofrecen una interfaz visual fácil de usar para gestionar bases de datos sin necesidad de escribir comandos manualmente.

#### 👑 Enfatizando: SQL Server Management Studio (SSMS)

<p align="center">
  <img src="https://licendi.com/media/wysiwyg/microsoft_sql_server_management_studio.png" alt="SQL Server Management Studio" width="600">
</p>

**SQL Server Management Studio (SSMS)** es la herramienta gráfica oficial para administrar y consultar bases de datos SQL Server.  
Permite:
- Escribir y ejecutar instrucciones SQL fácilmente.
- Crear y modificar tablas de forma visual.
- Realizar respaldos y restauraciones.
- Administrar permisos de usuario y seguridad.
- Visualizar y editar datos en tablas sin escribir SQL.

**Otras GUIs populares:**  
- **DBeaver** (multiplataforma, soporta muchos motores)
- **pgAdmin** (PostgreSQL)
- **phpMyAdmin** (web, para MySQL/MariaDB)
- **TablePlus**

---

## 💻 Instalar una base de datos: local vs. contenedor

Existen dos formas comunes de tener una base de datos disponible para desarrollo o pruebas en tu máquina:

### Opción 1: Instalar directamente en tu máquina local

**Ventajas:**
- Acceso inmediato y permanente a la base de datos.
- Integración total con otras aplicaciones instaladas localmente.
- Útil para ambientes de desarrollo que requieren acceso constante.

**Desventajas:**
- Puede generar conflictos de versiones si tienes que trabajar con diferentes bases o versiones.
- Ocupa recursos y almacenamiento de forma permanente.
- Más difícil de limpiar o resetear el entorno.
- La desinstalación puede dejar residuos en el sistema.
- **El proceso de instalación puede ser problemático o tedioso, ya que implica múltiples pasos, configuraciones y posibles errores de compatibilidad dependiendo del sistema operativo.**

### Opción 2: Ejecutar la base de datos en un contenedor (ej: Docker)

**Ventajas:**
- Rápida instalación y eliminación sin dejar residuos.
- Puedes tener múltiples versiones y tipos de base de datos corriendo en paralelo.
- Fácil de compartir configuraciones (docker-compose, Dockerfile) entre equipos.
- El entorno es reproducible, ideal para equipos y CI/CD.
- Puedes levantar y bajar la base de datos solo cuando la necesitas.

**Desventajas:**
- Requiere instalar Docker o una tecnología de contenedores.
- Puede ser más lento el acceso a archivos pesados si no se configura bien.
- Algunas herramientas GUI pueden requerir configuración adicional para conectarse al contenedor.

**Ejemplo de comando para levantar SQL Server en Docker:**  
(Consulta también la guía: [`sql-with-docker.md`](../../@IngSoft-DA2/DA2-Tecnologia/files/sql-with-docker.md))
```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=TuPassword123" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
```

> En resumen: Para desarrollo, usar contenedores suele ser más práctico y limpio. Para producción, normalmente se usan servidores dedicados o servicios gestionados.

---

## 🟢 Buenas prácticas al interactuar con bases de datos

- Usa cuentas de usuario con permisos restringidos (no siempre admin).
- Realiza respaldos (backups) periódicamente.
- Mantén tus datos y contraseñas seguros.
- Para producción, nunca ejecutes comandos peligrosos (`DROP`, `DELETE` sin `WHERE`, etc.) sin respaldo.
- Documenta tus estructuras y consultas importantes.

---

## 📚 Recursos útiles

- [Tutorial básico de SQL](https://www.w3schools.com/sql/)
- [Descargar SQL Server Management Studio](https://aka.ms/ssms)
- [Documentación oficial de SQL Server](https://learn.microsoft.com/en-us/sql/sql-server/)
- [DBeaver: Cliente universal de bases de datos](https://dbeaver.io/)

---

> 💡 **Sugerencia:**  
> Si eres principiante, comienza usando una GUI como SQL Server Management Studio para familiarizarte con la estructura de las bases de datos, luego aprende a usar la CLI para tener mayor control y automatización.
