[🔙 Indice](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/angular?tab=readme-ov-file#indice) → [🏠 Main](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/main?tab=readme-ov-file#da2-tecnologia--dise%C3%B1o-de-aplicaciones-2)

# 🌐 Inicios en Páginas Web

Antes de sumergirnos en Angular y las SPAs (Single Page Applications), es fundamental entender cómo funcionaban las aplicaciones web antes de su aparición. Así podrás comprender mejor la evolución y ventajas de los enfoques modernos.

La principal alternativa previa eran las **MPAs** (Multi-Page Applications) 🗂️, donde la estructura de la aplicación se componía de múltiples archivos HTML independientes. Cada vez que el usuario interactuaba con la aplicación, el navegador debía solicitar una nueva página al servidor, recargando el contenido por completo.

---

<p align="center">
  <img src="./images/image.png">
</p>
<p align="center"><em>🔄 Comunicación entre cliente y servidor</em></p>

---

En su momento, este método era el estándar y funcionaba acorde a las necesidades de la época. Sin embargo, tenía varias desventajas. Imagina que navegas por una tienda online construida como una MPA: cada vez que cambias de página (por ejemplo, de "inicio" a "productos"), el navegador descarga y renderiza una página nueva, generando un proceso de recarga constante.

---

<p align="center">
  <img src="./images/image-1.png">
</p>
<p align="center"><em>🌍 Navegación en una página MPA</em></p>

---

## ⚙️ ¿Cómo funcionan las MPAs?

En las MPAs, la mayor parte de la lógica y el procesamiento de datos ocurre **del lado del servidor** 🖥️. Cuando el usuario solicita una página, el servidor ejecuta scripts para buscar datos, procesa los templates y arma el HTML que luego envía al navegador.

Cada interacción, sin importar qué tan pequeña (un simple click en un enlace, por ejemplo), provoca la reconstrucción y recarga total de la página. Esto puede resultar en una experiencia fragmentada y menos fluida para el usuario 😕.

---

## 🚀 Mejoras y transición

Para mejorar la experiencia, los desarrolladores comenzaron a implementar técnicas como **AJAX** (Asynchronous JavaScript and XML) ⚡, que permite actualizar secciones de la página de manera asincrónica, evitando el recargo completo.

Aunque la arquitectura MPA fue efectiva durante mucho tiempo, eventualmente perdió popularidad con la llegada de las **SPAs** (Single Page Applications), que ofrecen una experiencia mucho más dinámica y fluida 💫.

---

## 📝 Características principales de las MPAs

- 🔄 **Recarga completa de páginas:** Cada interacción que requiera nuevo contenido implica un refresco total del sitio web.
- 🖥️ **Renderización del lado del servidor (SSR):** El servidor genera el HTML dinámicamente antes de enviarlo al navegador, integrando los datos en los templates.
- ⏳ **Comunicación sincrónica:** El navegador debe esperar la respuesta del servidor antes de continuar con la interacción.
- 🚧 **Experiencia fragmentada:** Los recargos completos generan tiempos de espera visibles y efectos visuales que pueden resultar molestos para el usuario.

---

## 🛠️ Tecnologías utilizadas en las MPAs

- 📄 **HTML/CSS:** Estructura y estilos de las páginas web.
- 💻 **JavaScript:** Para interactividad básica y mejoras progresivas.
- 🧑‍💻 **Lenguajes del lado del servidor:** PHP, ASP.NET, Java, Python, Ruby (muy populares para generar contenido dinámico).
- 🗄️ **Bases de datos relacionales:** MySQL, PostgreSQL, SQL Server (almacenamiento y gestión de datos).

---

> Las MPAs sentaron las bases de la web moderna, pero la evolución hacia SPAs permitió crear aplicaciones más rápidas, interactivas y agradables para los usuarios 👨‍💻🚀.
