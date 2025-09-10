[🔙 Volver - Main](./README.md)

# 🗂️ Herramienta de Control de Versiones: Git

<p align="center">
  <img src="https://git-scm.com/images/logos/downloads/Git-Logo-2Color.png" alt="Logo de Git" width="250">
</p>

## ¿Qué es un sistema de control de versiones?

Un **sistema de control de versiones** es una herramienta que permite gestionar los cambios realizados en el código fuente de un proyecto a lo largo del tiempo. Facilita la colaboración entre varios desarrolladores y proporciona un historial completo de todas las modificaciones, permitiendo revertir errores y comparar versiones anteriores.

## 🧰 ¿Qué es Git?

**Git** es el sistema de control de versiones más utilizado actualmente. Fue creado por Linus Torvalds en 2005 y es ampliamente popular gracias a su rapidez, eficiencia y capacidad de trabajo distribuido.

### Ventajas de usar Git

- 🕒 **Historial completo:** Puedes ver todos los cambios realizados en cualquier archivo del proyecto.
- 🤝 **Colaboración sencilla:** Múltiples personas pueden trabajar en el mismo proyecto sin sobrescribir el trabajo de los demás.
- ⚡ **Velocidad y eficiencia:** Git es rápido incluso en proyectos muy grandes.
- 🛡️ **Seguridad:** Todos los cambios quedan registrados y protegidos frente a pérdidas accidentales.

---

## 📝 Comandos básicos de Git (Cheat Sheet)

A continuación, se detallan los comandos más usados y esenciales para comenzar a trabajar con Git desde la terminal:

| Comando                             | Descripción                                                    |
|-------------------------------------|----------------------------------------------------------------|
| `git init`                          | Inicializa un nuevo repositorio Git local                      |
| `git clone <url>`                   | Clona un repositorio remoto a tu máquina local                 |
| `git status`                        | Muestra el estado de los archivos (cambios, ramas, etc)        |
| `git add <archivo>`                 | Agrega archivos al área de preparación (staging)               |
| `git add .`                         | Agrega todos los archivos modificados                          |
| `git commit -m "mensaje"`           | Guarda los cambios en el historial de versiones                |
| `git pull`                          | Descarga y fusiona cambios del repositorio remoto              |
| `git push`                          | Sube tus cambios confirmados al repositorio remoto             |
| `git log`                           | Muestra el historial de commits                                |
| `git branch`                        | Lista las ramas existentes                                     |
| `git checkout <rama>`               | Cambia a la rama indicada                                      |
| `git checkout -b <rama>`            | Crea y cambia a una nueva rama                                 |
| `git merge <rama>`                  | Fusiona la rama indicada con la rama actual                    |
| `git remote -v`                     | Muestra las URLs de los repositorios remotos                   |
| `git reset --hard`                  | Revierte todos los cambios no confirmados                      |
| `git cherry-pick <hash_commit>`     | Aplica el commit indicado de otra rama a la rama actual        |
| `git rebase <rama>`                 | Reaplica tus commits sobre la punta de la rama indicada        |

> 🎯 **Sugerencia:**  
> Aprende y practica estos comandos para tener un control total sobre tus proyectos y tu código fuente.

---

## 🔍 Conceptos clave en Git

Git utiliza varios conceptos fundamentales para la gestión del código y la colaboración:

### 🏠 Base (Base branch)
La **base** de una rama suele referirse a la rama principal sobre la cual se crea una nueva rama de trabajo. Por ejemplo, al crear una rama para una nueva funcionalidad desde `main`, se dice que `main` es la base de esa rama.

### 🌎 Remote
Un **remote** es una versión de tu proyecto que está alojada en internet o en otra red. Permite colaborar con otros usuarios y compartir los cambios de manera centralizada. El remote más común es el repositorio en GitHub, GitLab, etc.

### 🏷️ Origin
**origin** es el nombre por defecto que Git otorga al remote principal de tu repositorio, normalmente el repositorio original de donde clonaste tu copia local. Puedes tener varios remotes, pero `origin` suele ser el principal.

### 🔝 HEAD
**HEAD** es un puntero que indica la rama o commit actual sobre el que estás trabajando. Cuando haces cambios o commits, se aplican donde apunta `HEAD`.

### 🌿 Branch (Rama)
Una **branch** o rama es una línea de desarrollo independiente. Permite trabajar en nuevas características, correcciones o experimentos sin afectar la rama principal (como `main` o `master`). Puedes combinar ramas usando `merge`, aplicar cambios específicos entre ramas con `cherry-pick` o actualizar tu rama usando `rebase`.

---

## ⚡ Ejemplos prácticos: merge, rebase y cherry-pick

### 🔀 `git merge`

El comando `merge` se utiliza para integrar los cambios de una rama en otra.  
Por ejemplo, si tienes una rama `feature` y quieres traer sus cambios a `main`, estando en `main` ejecutas:
```
git checkout main
git merge feature
```
Esto creará un nuevo commit de merge con los cambios combinados de ambas ramas.  
**Ventaja:** Conserva el historial de ambas ramas y muestra cuándo se unieron.

---

### 🥞 `git rebase`

El comando `rebase` se utiliza para "reaplicar" los commits de tu rama sobre la punta de otra rama, reescribiendo el historial.  
Por ejemplo, si tu rama `feature` quedó desactualizada respecto a `main`, puedes hacer:
```
git checkout feature
git rebase main
```
Esto mueve todos los commits de `feature` y los pone como si se hubieran hecho después del último commit de `main`.  
**Ventaja:** El historial es más lineal y fácil de leer, pero reescribe el historial de la rama.

---

### 🍒 `git cherry-pick`

El comando `cherry-pick` permite aplicar un commit específico de otra rama en la rama actual, sin traer todos los commits intermedios.  
Por ejemplo, si tienes un commit útil en `feature` y lo quieres en `main`:
```
git checkout main
git cherry-pick <hash_commit>
```
Esto copia solo ese commit a `main`.

---

## ⚡ ¿Qué son los conflictos en Git?

Un **conflicto** ocurre cuando Git no puede fusionar automáticamente los cambios realizados en diferentes ramas porque afectan las mismas líneas de un archivo o archivos incompatibles. Los conflictos suelen surgir al hacer un `merge`, `rebase` o incluso un `cherry-pick`.

### ¿Qué acciones puedo tomar ante un conflicto?

1. **Detectar el conflicto:**  
   Git marcará los archivos en conflicto y te indicará cuáles necesitan ser resueltos.

2. **Revisar y editar:**  
   Abre los archivos en conflicto y busca las secciones marcadas con `<<<<<<<`, `=======` y `>>>>>>>`. Decide qué cambios conservar o integra ambos de manera coherente.

3. **Marcar como resuelto:**  
   Una vez resueltos los conflictos, marca los archivos como resueltos con:
   ```
   git add <archivo>
   ```

4. **Continuar el proceso:**  
   Si estabas haciendo un `merge`, continúa con:
   ```
   git commit
   ```
   Si estabas haciendo un `rebase`, usa:
   ```
   git rebase --continue
   ```
   Si era un `cherry-pick`, simplemente realiza el commit.

5. **En caso de error:**  
   Si necesitas descartar el merge/rebase/cherry-pick y volver al estado anterior:
   ```
   git merge --abort
   git rebase --abort
   git cherry-pick --abort
   ```

> 💡 **Consejo:** Resuelve los conflictos lo antes posible y comunícalo a tu equipo si es necesario.

---

## 🔗 ¿Qué es un cliente Git?

Aunque Git funciona principalmente desde la terminal, existen **clientes de Git** con interfaz gráfica que simplifican el uso para quienes prefieren no usar comandos de texto.

### Ejemplo: GitHub Desktop

<p align="center">
  <img src="https://github.blog/wp-content/uploads/2021/03/multiple-commits.gif" alt="Uso de GitHub Desktop" width="600">
</p>

[GitHub Desktop](https://desktop.github.com/) es un cliente de Git gratuito que facilita el manejo de repositorios y la colaboración en proyectos, especialmente con GitHub.

#### Principales funciones de GitHub Desktop

- 📥 Clonar repositorios desde GitHub fácilmente.
- 📝 Realizar cambios y ver diferencias entre versiones de archivos.
- ✅ Hacer commits y sincronizar con el repositorio remoto.
- 🌳 Gestionar ramas (*branches*) y resolver conflictos de fusión.
- 🚀 Subir (*push*) y bajar (*pull*) cambios con un clic.

---

## 🚀 ¿Cómo empezar con Git y GitHub Desktop?

1. **Instala Git:**  
   Descárgalo desde su [sitio oficial](https://git-scm.com/downloads) y sigue las instrucciones para tu sistema operativo.

2. **Instala GitHub Desktop:**  
   Descárgalo desde [aquí](https://desktop.github.com/) e instálalo.

3. **Inicia sesión con tu cuenta de GitHub:**  
   Así podrás clonar repositorios de GitHub y colaborar fácilmente.

4. **Clona un repositorio existente o crea uno nuevo:**  
   Puedes gestionar tus proyectos desde la aplicación sin necesidad de usar la terminal.

---

## 🟩 Buenas y 🟥 malas prácticas con Git según GitFlow

### 🟩 Buenas prácticas

- **Usa ramas para cada funcionalidad, hotfix o feature**: Sigue el modelo de ramas propuesto por GitFlow (`main`, `develop`, `feature/*`, `release/*`, `hotfix/*`).
- **Hacer commits pequeños y frecuentes**: Facilita el seguimiento y la revisión de cambios.
- **Escribe mensajes de commit claros y descriptivos**: Un mensaje breve y preciso ayuda a entender el propósito del cambio.
- **Actualiza tu rama antes de hacer merge**: Haz `git pull origin main` o `develop` para evitar conflictos.
- **Elimina ramas que ya no se usen**: Mantén el repositorio limpio.
- **Haz pull requests para revisión**: Permite que otros revisen tu código antes de fusionarlo.
- **Resuelve los conflictos con cuidado y comunica si es necesario**.

### 🟥 Malas prácticas

- **Trabajar directamente en `main` o `master`**: Puede introducir errores en producción.
- **Commits grandes y poco frecuentes**: Dificultan el seguimiento de cambios y la resolución de errores.
- **No usar mensajes descriptivos en los commits**: Mensajes como "arreglo" o "cambios" no ayudan a identificar el propósito.
- **Ignorar conflictos y forzar merges**: Puede llevar a errores difíciles de rastrear.
- **No actualizar la rama antes de hacer merge**: Aumenta la probabilidad de conflictos.
- **No eliminar ramas viejas**: El repositorio se vuelve difícil de mantener.
- **Subir archivos innecesarios o de configuración local**: Usa `.gitignore` para evitarlo.

---

## 📚 Recursos útiles

- [Documentación oficial de Git](https://git-scm.com/doc)
- [Guía rápida de GitHub Desktop](https://docs.github.com/es/desktop)
- [Más comandos de Git (cheat sheet oficial)](https://education.github.com/git-cheat-sheet-education.pdf)

---

> 💡 **Sugerencia:**  
> Aunque los clientes gráficos como GitHub Desktop facilitan el trabajo, es recomendable aprender los comandos básicos de Git para tener un mayor control y comprensión del flujo de trabajo.
