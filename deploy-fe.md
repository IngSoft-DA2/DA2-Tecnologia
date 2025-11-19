# 🚀 Guía para deployar una SPA Angular en IIS (para alumnos) 🎓

---

## 1. ¿Qué significa deployar una SPA Angular en IIS? 🤔

Una *Single Page Application* (SPA) como Angular funciona diferente a las aplicaciones tradicionales con páginas múltiples:

- IIS **solo sirve archivos estáticos**: `index.html`, `.js`, `.css`, imágenes, etc. 🗂️
- Toda la navegación se maneja **del lado del navegador** usando *Angular Router* 🔀.
- IIS **no entiende** rutas como `/clientes` o `/productos/10`; solo conoce archivos y carpetas. ❌📁

Por eso, el deploy consiste en:

1. 🛠️ Compilar Angular para producción → genera una carpeta `dist`.
2. 📤 Copiar esa carpeta a IIS.
3. 🖥️ Configurar un sitio que sirva esos archivos.
4. 🔁 Hacer que IIS reescriba todas las rutas a `index.html` para que Angular pueda encargarse.

---

## 2. Preparar el proyecto Angular (Environments) 🧭

### 2.1 Carpeta `environments` 📁

En el proyecto Angular vas a encontrar o crear:

- `environment.ts` → archivo base con valores *placeholder* (este va al repositorio). ✅  
- `environment.prod.ts`, `environment.qa.ts`, etc. → entornos reales (estos **no deben ir** al repositorio). 🔒

Ejemplo de `environment.ts`:

```ts
export const environment = {
  production: false,
  apiBaseUrl: "https://placeholder.api"
};
```

Ejemplo de `environment.prod.ts`:

```ts
export const environment = {
  production: true,
  apiBaseUrl: "https://api.midominio.com"
};
```

> 💡 Buenas prácticas: Mantener los valores reales fuera del repo evita leaks de claves/URLs privadas.

### 2.2 Ignorar entornos reales en Git 🛡️

En `.gitignore`:

```gitignore
environment.*.ts
```

Así mantenemos la buena práctica de que los valores reales no se suben al repositorio. ✅

---

## 3. Configuración en `angular.json` ⚙️

Angular permite reemplazar archivos durante el build dependiendo del entorno:

```jsonc
"configurations": {
  "production": {
    "fileReplacements": [
      {
        "replace": "src/environments/environment.ts",
        "with": "src/environments/environment.prod.ts"
      }
    ],
    "optimization": true,
    "outputHashing": "all",
    "sourceMap": false,
    "aot": true
  }
}
```

Esto significa que al compilar con `--configuration=production`, Angular **automáticamente usa** `environment.prod.ts`. 🔁

---

## 4. Scripts en `package.json` 📦

```jsonc
"scripts": {
  "start": "ng serve",
  "build": "ng build",
  "build:prod": "ng build --configuration=production"
}
```

Para compilar:

```bash
npm run build:prod
```

Esto genera la carpeta `dist/tu-app`. ✅

---

## 5. Generar el build final de Angular 🏗️

Pasos:

1. Ir a la carpeta del proyecto. 📂  

2. Ejecutar el build:

   ```bash
   npm run build:prod
   ```

3. Verificar que existe:

```
dist/
  tu-app/
    index.html
    main.*.js
    styles.*.css
```

Esta es la carpeta que vas a subir a IIS. 📤

---

## 6. Instalar y preparar IIS 🖥️🔧

### 6.1 Instalar URL Rewrite Module 🔁

Debés instalar el módulo **URL Rewrite**, que permite interceptar URL y reenviarlas al archivo correcto.  
Es obligatorio para que Angular funcione correctamente cuando se hace refresh en una ruta. ⚠️

---

## 7. Crear el sitio en IIS 🏷️

1. Crear una carpeta, por ejemplo:

```
C:\inetpub\wwwroot\angular-spa
```

2. Copiar ahí el contenido de `dist/tu-app`. 📁➡️🗂️

3. En IIS:
   - Clic derecho → **Agregar sitio web**.
   - Ruta física → la carpeta que creaste.
   - Puerto → el que elijas (por ejemplo `8081`).

---

## 8. El problema del Refresh y las rutas 🔄❗

### ¿Qué sucede?

Si vas a:

```
http://localhost:8081/clientes/5
```

y tocás **F5**, o pegás esa URL en otra pestaña:

- El navegador pide esa ruta al servidor.
- IIS busca un archivo o carpeta `clientes/5`.
- Como no existe, devuelve **404**. 😢

**Angular nunca llega a cargarse**, porque el error ocurre antes.

Esto es normal en todas las SPA.

### ¿Cómo se soluciona?

Diciéndole a IIS:

> Si la URL no existe como archivo físico, devolvé `index.html`. 🧾➡️🏠

Esto se hace con **URL Rewrite**.

---

## 9. `web.config` para Angular en IIS 🗂️🔧

Crear un archivo llamado **`web.config`** dentro de `dist/tu-app` (o copiarlo después del build).

Contenido recomendado:

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <system.webServer>
    <rewrite>
      <rules>
        <rule name="Angular Routes" stopProcessing="true">
          <match url=".*" />
          <conditions logicalGrouping="MatchAll">
            <add input="{REQUEST_FILENAME}" matchType="IsFile" negate="true" />
            <add input="{REQUEST_FILENAME}" matchType="IsDirectory" negate="true" />
          </conditions>
          <action type="Rewrite" url="/index.html" />
        </rule>
      </rules>
    </rewrite>

  </system.webServer>
</configuration>
```

### ¿Qué hace esta regla? 🔍

- Si la ruta **no es archivo**  
- Y **no es carpeta**  
→ se reescribe a `index.html`. ✅

Así, aunque refresques `/clientes/2`, Angular siempre recibe el archivo principal y luego interpreta la ruta correctamente desde el navegador. 🧭

---

¡Listo! 🎉 Ahora tenés una SPA de Angular corriendo correctamente en IIS.  

```
