[Volver - Main](https://github.com/IngSoft-DA2/DA2-Tecnologia)

# 🚀 Guía Visual: Creación de un Pull Request (PR) en GitHub

Un **Pull Request (PR)** es la herramienta fundamental para proponer, discutir y validar cambios en el código. Permite que cualquier miembro del equipo comparta sus avances, reciba feedback, y solo después de revisión, integre los cambios a la rama principal.

---

## 🧩 Conceptos clave

- **Source branch:** Rama con los cambios (la tuya).
- **Target branch:** Rama destino (normalmente `develop` o `main`).
- **Draft PR:** Un PR en *borrador* indica que aún no está listo para revisión o merge. Puedes convertirlo a *Ready for review* cuando esté listo.
- **PR abierto:** Permite seguir agregando commits a la rama fuente y se actualiza automáticamente el PR.

---

## ✨ ¿Por qué usar PRs?

- Permiten revisión colaborativa y feedback.
- Protegen la rama principal de errores.
- Activan pipelines automáticos de test y análisis.
- Facilitan el seguimiento y la documentación de cambios.

---

## 📝 Pasos para crear un Pull Request

### 1. Ver sugerencia de GitHub

GitHub suele sugerir crear un PR si tu rama tiene cambios respecto a la rama principal:

<p align="center">
  <img src="./images/image.png">
</p>
<p align="center">
  <em>Notificación de crear PR</em>
</p>

---

### 2. Acceder a la sección Pull Requests

Si no aparece la sugerencia, ve a la barra superior y haz click en <strong>Pull Request</strong>.

<p align="center">
  <img src="./images/image-1.png">
</p>
<p align="center">
  <em>Sección Pull Request</em>
</p>

---

### 3. Click en <code>New pull request</code>

Aquí puedes ver sugerencias o iniciar un nuevo PR manualmente si no aparece la rama que deseas.

<p align="center">
  <img src="./images/image-2.png">
</p>
<p align="center">
  <em>Inicio en la opción de Pull Request</em>
</p>

---

### 4. Elegir ramas de origen y destino

- Selecciona la rama con tus cambios (“source branch”) y la rama destino (“target branch”).
- Elige ramas diferentes y asegúrate que haya cambios pendientes de merge.

<p align="center">
  <img src="./images/image-3.png">
</p>
<p align="center">
  <em>Selección de ramas</em>
</p>

---

### 5. Revisa los cambios a mergear

- GitHub mostrará el historial de commits y archivos afectados.
- Si todo está correcto, haz click en <strong>Create pull request</strong>.

<p align="center">
  <img src="./images/image-4.png">
</p>
<p align="center">
  <em>Lista de cambios y botón de creación</em>
</p>

---

### 6. Completa el formulario del PR

- Escribe un título claro y una descripción detallada. Personaliza o elimina los campos entre [ ] si corresponde.
- Indica el estado inicial del PR: *Draft* (borrador) o *Ready for review* (listo para revisión).

<p align="center">
  <img src="./images/image-5.png">
</p>
<p align="center">
  <em>Formulario de creación del PR</em>
</p>

<p align="center">
  <img src="./images/image-6.png">
</p>
<p align="center">
  <em>Descripción del PR</em>
</p>

<p align="center">
  <img src="./images/image-9.png">
</p>
<p align="center">
  <em>Estado: Draft o Ready For Review</em>
</p>

---

### 7. Monitorea las GitHub Actions

- Al crear el PR, se ejecutarán automáticamente los checks (tests, análisis de código, etc).
- Puedes ver el estado directamente en la página del PR.

<p align="center">
  <img src="./images/image-10.png">
</p>
<p align="center">
  <em>Estado de las GitHub Actions</em>
</p>

---

### 8. Revisa el estado y detalles del PR

- Podrás ver si el PR está abierto, cerrado o mergeado.
- Cada PR tiene un identificador único.

<p align="center">
  <img src="./images/image-11.png">
</p>
<p align="center">
  <em>Estado e identificador del PR</em>
</p>

---

## ✅ Buenas prácticas al trabajar con Pull Requests

- **Revisa y mergea PRs de tus compañeros:** No merges tus propios PRs. Es ideal que otro miembro del equipo revise y acepte los cambios para fomentar la revisión cruzada y mejorar la calidad del código.
- **Contribuye en los PRs de otros:** Si detectas mejoras o correcciones, comenta o sube sugerencias directamente en el PR de tu compañero.
- **Haz descripciones claras y completas:** Detalla qué problema resuelve el PR y qué cambios realizaste. Deja registro de decisiones técnicas.
- **Actualiza el estado del PR:** Usa el modo *Draft* para trabajos en progreso y cámbialo a *Ready for review* solo cuando esté listo para revisión.
- **Responde al feedback:** Participa activamente en la discusión del PR, responde dudas y realiza los cambios sugeridos.
- **Verifica los checks:** Antes de solicitar el merge, asegúrate de que todas las acciones automáticas (tests, análisis) pasen correctamente.

---

## ❌ Malas prácticas a evitar

- **Mergear tus propios PRs:** Evita aprobar y mergear tus propios cambios sin revisión de al menos un compañero.
- **Ignorar comentarios o sugerencias:** No ignores el feedback, incluso si parece menor; todo aporte suma a la calidad del proyecto.
- **Subir muchos cambios no relacionados en un solo PR:** Intenta que cada PR tenga un objetivo claro y cambios acotados.
- **No describir el PR:** No dejes la descripción vacía o con texto por defecto. Explica siempre el propósito del PR.
- **No mantener actualizado tu branch:** Si hay cambios recientes en la rama base, actualiza tu branch antes de pedir el merge.

---

## 📚 Lecturas recomendadas

- [¿Qué es un Pull Request?](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/proposing-changes-to-your-work-with-pull-requests/about-pull-requests)
- [Cómo crear un Pull Request](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/proposing-changes-to-your-work-with-pull-requests/creating-a-pull-request?platform=windows)

---

> 💡 Recuerda: Trabajar con PRs fomenta la colaboración, mejora la calidad del código y documenta la historia del proyecto. ¡Aprovecha el proceso y apóyate en tu equipo!
