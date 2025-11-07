# 🧭 Introducción al Deployment ✨

## ¿Qué significa “deployar”? 🚀

El término **deployar** (del inglés *to deploy*) hace referencia al proceso de **tomar una aplicación lista para ejecutarse** —por ejemplo, una API, un sitio web o un servicio— y **publicarla e[...]  
En otras palabras, es el paso que convierte tu código fuente (archivos, binarios, configuraciones) en una **aplicación viva y funcional** en un servidor.

Un *deployment* puede implicar copiar archivos a un servidor, configurar un entorno de ejecución, exponer un puerto, crear un servicio del sistema operativo, o incluso automatizar todo eso dentro de [...]

---

## El servidor como entorno de ejecución 🖥️

Cuando deployamos una aplicación, **el servidor** se convierte en el espacio donde nuestro software “vive” y se ejecuta. Este servidor puede ser físico o virtual, local o en la nube, pero siempr[...]  
- **Proveer recursos de hardware** (CPU, memoria, red, almacenamiento)  
- **Ejecutar un sistema operativo** (como Windows Server o Linux)  
- **Alojar y servir aplicaciones** bajo un modelo controlado

En el caso de aplicaciones web en Windows, el servidor suele ejecutar **IIS (Internet Information Services)**, que actúa como un *host web*: escucha peticiones HTTP y las direcciona a la aplicación [...]

---

## Tipos de servidores 🗂️

- **Servidor web**: responde solicitudes HTTP(S), por ejemplo IIS, Nginx o Apache.  
- **Servidor de aplicaciones**: ejecuta lógica más compleja (por ejemplo, .NET, Node.js, Java EE).  
- **Servidor de base de datos**: gestiona y responde consultas de datos.  

En muchos entornos, varios de estos roles conviven en una misma máquina, aunque en entornos productivos se suelen separar para mejorar escalabilidad y seguridad.

---

## Cómo “instala” IIS una aplicación ⚙️

IIS funciona como un **orquestador de sitios**. Cada sitio o aplicación dentro de IIS se **asigna a un puerto** (por ejemplo, `80` o `443`), y a una **carpeta física** donde residen los archivos de [...]

1. Cuando llega una solicitud HTTP (por ejemplo `http://localhost:1234/api/users`), IIS escucha en el puerto asignado.  
2. Detecta a qué sitio o *Application Pool* pertenece esa ruta.  
3. IIS crea (si no existe) o reutiliza un proceso en segundo plano llamado **`w3wp.exe`** que ejecuta el runtime correspondiente (por ejemplo, .NET CLR).  
4. El runtime carga tu código compilado (por ejemplo, `MyApp.dll`) y lo ejecuta, devolviendo una respuesta HTTP al cliente.  

En ese sentido, **IIS no interpreta tu código fuente directamente**: lo **hospeda** dentro de un proceso de aplicación, manejando la comunicación entre el sistema operativo, el framework (.NET, PHP[...]

---

## Puertos y aislamiento 🔌

Cada aplicación que se ejecuta en IIS **usa un puerto** (por ejemplo `80` para HTTP o `443` para HTTPS).  
Esto permite que varias aplicaciones coexistan en un mismo servidor, ya que cada una escucha en un puerto o dominio diferente.  

Sin embargo, **no pueden compartir exactamente el mismo puerto y host sin un balanceo o configuración especial** (como *bindings* o *host headers*).

Este concepto es muy similar a lo que ocurre en Docker: cada contenedor tiene su propio puerto interno, y el sistema hace un *mapping* hacia el puerto público.  
Por eso se dice que **Docker abstrae el servidor físico** y ofrece un “mini servidor” aislado para cada aplicación.

# ⚙️ Qué es un Application Pool en IIS 🔒

## Introducción 🔍

En **IIS (Internet Information Services)**, un *Application Pool* —o “grupo de aplicaciones”— es **el contenedor lógico donde se ejecuta una o varias aplicaciones web**.  
Podés pensar en él como una **caja de arena de ejecución** (*runtime sandbox*) que provee a las aplicaciones un entorno aislado dentro del servidor.

Cada *Application Pool* tiene su propio **proceso de trabajo** (generalmente `w3wp.exe`) que corre bajo una identidad del sistema y con configuraciones específicas: versión del framework, límites d[...]  
De esta forma, si una aplicación falla o se bloquea, no afecta a las demás que están corriendo en el mismo IIS pero dentro de otros *Application Pools*.

---

## Rol del Application Pool 🛡️

Su función principal es **aislar y administrar la ejecución** de las aplicaciones.  
Cuando creás un sitio en IIS, tenés que asociarlo a un *Application Pool*. A partir de ese momento:

- IIS **lanza un proceso independiente** (`w3wp.exe`) para ese pool.  
- Dentro de ese proceso, se **carga el runtime** correspondiente (por ejemplo .NET CLR o .NET Core Hosting Bundle).  
- El pool **mantiene la aplicación viva** (según configuración de “idle timeout”, reciclado o “always running”).  
- Si ocurre un error o fuga de memoria, IIS puede **reciclar el proceso** sin detener todo el servidor.  

Esto otorga **estabilidad y seguridad**: un problema en una app no derriba todo el servidor, ni puede acceder directamente a la memoria de otra.

---

## Arquitectura y funcionamiento 🧩

```
┌───────────────────────────────┐
│ IIS (Administrador global)    │
│                               │
│  ┌──────────────────────────┐ │
│  │ Application Pool A       │ │
│  │ (w3wp.exe #1)            │ │
│  │  └─ App1 (api.empresa.com)│ │
│  │  └─ App2 (panel.empresa.com)│
│  └──────────────────────────┘ │
│                               │
│  ┌──────────────────────────┐ │
│  │ Application Pool B       │ │
│  │ (w3wp.exe #2)            │ │
│  │  └─ App3 (intranet.local)│ │
│  └──────────────────────────┘ │
└───────────────────────────────┘
```

Cada pool tiene su propio proceso y configuración.  
Por ejemplo, el *Application Pool A* podría usar .NET 8, mientras que el *B* usa .NET Framework 4.8, y ambos coexistir perfectamente.

---

## Configuraciones clave de un Application Pool ⚙️

1. **Runtime / CLR version**  
   Define qué versión del framework usará la aplicación (.NET Framework, .NET Core, o ninguna).  

2. **Identity**  
   Es la cuenta del sistema bajo la que corre el proceso (`ApplicationPoolIdentity`, `NetworkService`, o un usuario personalizado).  
   Esto controla los permisos de acceso a archivos, bases de datos o servicios.

3. **Recycling**  
   IIS puede reiniciar automáticamente el proceso cada cierto tiempo, a determinada hora o cuando alcanza ciertos límites de memoria.  
   Este reciclado limpia fugas de memoria o estados inconsistentes sin necesidad de reiniciar el servidor completo.

4. **Idle Timeout**  
   Si la aplicación no recibe solicitudes durante un tiempo, el proceso puede “dormirse” para liberar recursos.  
   Se puede desactivar con la opción *AlwaysRunning* para APIs o sistemas críticos.

5. **Maximum Worker Processes (Web Garden)**  
   Permite que el mismo pool ejecute varios procesos en paralelo (generalmente no recomendado salvo escenarios específicos).

---

## Analogía con Docker 🐳

- **En IIS**, el servidor aloja varias aplicaciones, y cada una se ejecuta dentro de su propio *Application Pool*.  
- **En Docker**, cada aplicación vive dentro de su propio contenedor, con su propio entorno, dependencias y puerto expuesto.  

Podemos entender un *Application Pool* como un **contenedor ligero dentro del propio IIS**:

| Concepto                     | IIS (Application Pool)                            | Docker (Container)                        |
|------------------------------|---------------------------------------------------|-------------------------------------------|
| Entorno de ejecución         | w3wp.exe + configuración IIS                      | Proceso aislado dentro de un contenedor   |
| Aislamiento                  | Memoria y seguridad por proceso                   | Aislamiento total por namespace y FS      |
| Configuración                | Desde IIS Manager (runtime, identity, recycle)    | Desde Dockerfile o `docker-compose.yml`   |
| Reinicio controlado          | Recycling                                         | Restart policies                          |
| Imagen base / runtime        | Versión de .NET configurada                       | Imagen base (`mcr.microsoft.com/dotnet`)  |

En ambos casos hay una misma idea: **aislar y controlar la ejecución del código** para asegurar estabilidad, reutilización y facilidad de mantenimiento.
---

## Buenas prácticas ✅

- Crear **un pool por aplicación** en entornos productivos.  
- Usar **ApplicationPoolIdentity** (no administrador) para mayor seguridad.  
- Configurar **reciclado automático diario** (por ejemplo, 03:00 AM).  
- En APIs o sistemas críticos, activar **AlwaysRunning + Preload Enabled** para evitar tiempos de arranque.  
- Separar pools por framework (.NET Framework vs .NET Core).

---

# 🚀 Guía: Compilar y Publicar una Web API .NET en IIS (local) 🧰

## 1. Preparar Windows para IIS 🪟

### 1.1. Activar IIS ✅
En Windows:

1. Abrí **“Activar o desactivar las características de Windows”**
2. Marcá **Internet Information Services**
3. Dentro de IIS, asegurate de incluir:
   - **Web Management Tools** → **IIS Management Console**
   - **World Wide Web Services** → **Application Development Features**
     - .NET Extensibility (si usás .NET Framework)
     - ISAPI Extensions
     - ISAPI Filters
   - **Security** → **Request Filtering**

Luego de instalar, probá abriendo [http://localhost](http://localhost): deberías ver la página de inicio de IIS.

---

## 2. Preparar IIS para aplicaciones .NET modernas 🧩

### 2.1. Instalar el **.NET Hosting Bundle** ⬇️
Descargá e instalá el **ASP.NET Core Hosting Bundle** desde la página oficial de descargas de .NET correspondiente a tu versión (por ejemplo, .NET 8).  
Este paquete:

- instala el runtime de ASP.NET Core,  
- agrega el **ASP.NET Core Module** a IIS,  
- permite que IIS funcione como *reverse proxy* hacia tu aplicación.

> 🔎 Sin este paso, IIS mostrará errores tipo `502.5 – Process Failure`.

### 2.2. Habilitar “Static Content” 📁
En **Windows Features → Internet Information Services → World Wide Web Services → Common HTTP Features**, activá **Static Content**.  
Esto permite que tu app sirva archivos como Swagger, favicon, etc.

---

## 3. Compilar y publicar la Web API (.NET CLI) 🛠️

Desde la carpeta raíz del proyecto (donde está el `.csproj`):

```bash
dotnet publish -c Release -o ./publish
```

Explicación:
- `-c Release` → compila en modo optimizado.  
- `-o ./publish` → coloca los archivos listos para deploy en esa carpeta.

Esto genera una estructura como:

```
publish/
  MyApi.dll
  web.config
  appsettings.json
  ...
```

> ⚠️ El archivo `web.config` es esencial: le indica a IIS cómo iniciar el módulo ASP.NET Core que cargará tu API.

### 3.1 Qué es el `web.config` 📄

El archivo **`web.config`** es un archivo XML que se coloca en la raíz de la aplicación publicada.  
En aplicaciones **ASP.NET Core**, no controla directamente la configuración de la app (eso lo hace `appsettings.json`), sino que **le indica a IIS cómo iniciar y manejar el proceso de la aplicación[...]  

Su función principal es:
- Indicar a IIS **qué módulo usar** (el *ASP.NET Core Module*).  
- Definir **cómo iniciar el proceso `dotnet MyApi.dll`**.  
- Manejar el redireccionamiento entre IIS y el proceso real de la app.

Ejemplo típico:

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <system.webServer>
    <handlers>
      <add name="aspNetCore" path="*" verb="*" modules="AspNetCoreModuleV2" resourceType="Unspecified" />
    </handlers>
    <aspNetCore processPath="dotnet" arguments="MyApi.dll" stdoutLogEnabled="false" hostingModel="InProcess" />
  </system.webServer>
</configuration>
```

En resumen:  
➡️ `web.config` **no es parte de tu código .NET**, sino parte de **cómo IIS conecta las peticiones HTTP con el proceso .NET real**.  
Sin este archivo, IIS no sabría cómo iniciar ni a qué proceso enviar las solicitudes.

---

## 4. Instalar la aplicación en IIS 🗂️

### 4.1. Copiar los archivos publicados
Copiá todo el contenido de la carpeta `publish` a una ubicación permanente, por ejemplo:

```
C:\inetpub\wwwroot\<my-business>\<my-api>
```

> <my-business> y <my-api> cambiar por nombres nemotecnicos

---

### 4.2. Crear el sitio en IIS 🧭

1. Abrí **IIS Manager** (`inetmgr` desde Inicio).  
2. En el panel izquierdo, hacé clic derecho en **Sites → Add Website…**  
3. Completá los campos:
   - **Site name**: `MyApi` (escribir uno mas acorde al negocio)  
   - **Physical path**: `C:\inetpub\wwwroot\<my-business>\<my-api>`  
   - **Port**: por ejemplo `8080` o `5000` (si el 80 ya lo usa otro sitio)  
4. Guardá.

> ✅ IIS creará automáticamente un **Application Pool** con el mismo nombre del sitio.  
> Para APIs en .NET moderno, dejá el *Application Pool* con **“No Managed Code”** (esto es por defecto).

### Por qué el *Application Pool* debe ser “No Managed Code” 🧠

Cuando publicás una aplicación moderna (.NET 6/7/8), **IIS no ejecuta tu código directamente**.  
En lugar de eso, funciona como un **reverse proxy** que redirige las solicitudes al proceso `dotnet.exe` que levanta tu aplicación.

Por eso:

- El runtime de tu app **no usa el CLR clásico de IIS (.NET Framework)**.  
- Toda la ejecución la maneja el **.NET Core runtime** (fuera del control de IIS).  
- IIS solo necesita escuchar y pasar las peticiones.

Configurar el *Application Pool* como **“No Managed Code”** le indica a IIS que **no debe intentar cargar el runtime de .NET Framework**, porque la app se ejecutará **fuera del pipeline tradicion[...]  

Si lo dejaras como “.NET CLR v4.0”, IIS intentaría usar el antiguo motor de ASP.NET Framework, causando posibles conflictos o errores como `500.30 – ANCM Failed to Start`.

---

### 4.3. (Opcional) Permisos de carpeta 🔐
Si tu aplicación necesita escribir logs o archivos, otorgá permisos:

1. Click derecho en `C:\inetpub\apis\MyApi` → **Propiedades → Seguridad → Editar**  
2. Agregá: `IIS AppPool\MyApi`  
3. Concedé permisos **Read & Execute** o **Write** según necesidad.

---

### 4.4. Probar el sitio ✅

Abrí el navegador e ingresá:

```
http://localhost:8080/swagger
```

(o la ruta correspondiente de un get)

Si aparece un error tipo `500.30 – ANCM In-Process Start Failure`, verificá:

- que esté instalado el **Hosting Bundle**,  
- que la versión de .NET coincida,  
- o revisá el **Event Viewer → Windows Logs → Application**.

---

## 5. Configuraciones opcionales recomendadas ⚖️

### 5.1. Mantener la app siempre activa 🔁

En el **Application Pool → Advanced Settings**:
- **Start Mode = AlwaysRunning**

En el **Sitio → Advanced Settings**:
- **Preload Enabled = True**

Así la API no “duerme” cuando no recibe tráfico.

---

## 6. Resumen rápido (checklist) ✅

| Paso | Acción | Descripción |
|------|--------|--------------|
| ✅ 1 | Activar IIS | Desde “Características de Windows” |
| ✅ 2 | Instalar .NET Hosting Bundle | Habilita el módulo ASP.NET Core |
| ✅ 3 | Revisar que el modulo AspNetCoreV2 se instalara | En la seccion modulos de la raiz de iis, debera estar este modulo |
| ✅ 4 | Publicar API | `dotnet publish -c Release -o ./publish` |
| ✅ 5 | Chequear web.config | Revisar que use el modulo AspNetCoreV2 |
| ✅ 6 | Copiar archivos a IIS | `C:\inetpub\<my-business>\<my-api>` |
| ✅ 7 | Chequear application pool este en modo No managed code | No managed code |
| ✅ 8 | Crear Website | Configurar nombre, puerto y path |
| ✅ 9 | (Opcional) Dar permisos | `IIS AppPool\wwwroot\<my-business>\<my-api>` |
| ✅ 10 | Probar en navegador | `http://localhost:puerto/swagger` |
