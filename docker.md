[🔙 Volver - Main](https://github.com/IngSoft-DA2/DA2-Tecnologia/tree/main?tab=readme-ov-file#repositorio-de-clase)

# Docker

# 🧭 ¿Qué es Docker?

## 🔍 Intro

**Docker es una herramienta que nos permite crear, ejecutar y administrar contenedores.**

Un **contenedor** es como una **caja cerrada** que tiene todo lo que una aplicación necesita para funcionar: el código, programas adicionales, configuraciones, versiones exactas de librerías, etc.

Todo eso queda **compactado dentro del contenedor**. Así, la aplicación siempre se comporta igual, sin importar dónde se ejecute.

---

## 💻 ¿Qué es el *host*?

Cuando hablamos de **host**, nos referimos a la **computadora física** donde estamos ejecutando Docker. Puede ser:

- Tu notebook o PC de escritorio,
- La compu de un compañero,
- Un servidor en la nube.

> 🧠 Pensalo así:  
> El contenedor es como una caja con una aplicación adentro. El host es la mesa donde apoyás la caja para abrirla.  
> La caja se puede mover de una mesa a otra, pero siempre necesita una para estar apoyada.

---

## 📦 ¿Qué es un contenedor?

Un contenedor es:

- **Ligero** (no consume muchos recursos).
- **Rápido** (se inicia en segundos).
- **Aislado** (no molesta ni depende de otras apps).
- **Reproducible** (funciona igual donde sea).

> 🧠 Analogía:  
> Como un contenedor de barco: lo que lleva adentro no cambia si va en camión, tren o barco.  
> Docker hace lo mismo con tus aplicaciones: van “cerradas” y listas para funcionar.

---

## 🛠️ ¿Qué problema resuelve Docker?

¿Te pasó alguna vez esto?

- “En mi compu anda, pero en la del profe no.”
- “Funciona en casa, pero en clase da error.”
- “Tuve que instalar mil cosas para que el proyecto corra.”

Docker evita todo eso.

### ✅ ¿Por qué?

Porque **todo lo que necesita una aplicación ya viene adentro del contenedor**: versiones de Java, Node.js, bases de datos, configuraciones, librerías específicas… ¡todo!

### 📌 Beneficios:

- **No necesitás instalar nada en tu compu (el host)**.
- No te llenás de programas que después no usás.
- Evitás que una instalación rompa otra cosa que ya tenías.
- No sufrís las complicaciones de instalar tecnologías difíciles o con muchos pasos.
- Podés **prender y apagar dependencias** como bases de datos o servidores con un simple comando.
- Nada queda “pegado” a tu compu: **todo vive dentro del contenedor**.
- Si algo se rompe, eliminás el contenedor y lo recreás limpio.

---

## 🖥️ ¿La arquitectura del host importa?

Sí, **la arquitectura del host puede afectar cómo se crean o ejecutan los contenedores**.

Los tipos más comunes de procesador son:

- **x86_64 / amd64**: la mayoría de las PCs.
- **ARM**: muchos celulares, tablets y Mac con chip M1/M2.

Entonces:

- Si tu compu es x86_64, usás imágenes para esa arquitectura.
- Si tu compu es ARM, necesitás imágenes ARM, o Docker intentará emular la otra arquitectura (a veces funciona, a veces no).

> 📌 Muchos contenedores populares ya están preparados para funcionar en varias arquitecturas.

---

## 💡 ¿Por qué es importante aprender Docker?

- Porque podés **compartir proyectos sin complicaciones**.
- Porque **no tenés que instalar nada complicado en tu compu**.
- Porque **evitás errores por versiones, configuraciones o sistemas distintos**.
- Porque las **empresas lo usan todos los días** en sus proyectos reales.
