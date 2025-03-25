[Volver - Main](https://github.com/IngSoft-DA2/DA2-Tecnologia)

# Creación de un PullRequest - PR

Un pull request (PR) en GitHub es un mecanismo para los desarrolladores de proponer cambios al código fuente. Le permite a los desarrolladores notificarle a los miembros del equípo que se completo una feature o el arreglo de un bug y el código asociado a esos cambios esta listo para ser revisado y potencialmente para mergear en la rama del ambiente correspondiente. En el PR se encuentra mucha información sobre los cambios a introducir como poder ver las diferencias de los archivos manipulados (agregaciones, modificaciones y eliminaciones).

> [!NOTE]
>
> - Existen dos elementos fundamentales, lo que se llama source branch (la rama que contiene los cambios) y la target branch (la rama a donde se quiere mergear los cambios).
> - Una vez creado el PR, se pueden seguir agregando commits a la head branch (rama con los cambios) y este automaticamente se verá actualizado.

## Draft PR

Cuando uno crea un PR, puede decir en ese momento si está listo para ser revisado por el resto del equípo o si es un trabajo en proceso (WIP). Los draft PRs no pueden ser mergeados, y los dueños no tienen permitido solicitar la revision de los mismos. Cuando uno considera que el trabajo en la source branch esta terminado y listo para ser revisado, se puede marcar el draft PR como ready for review. Hacer un PR ready for review solicitará revisión a cualquier colaborador del repositorio. Así cómo se puede convertir un PR de draft a ready for review, se puede realizar la acción inversa.

## Creación de un PR

La web de github detecta cuales ramas están más avanzadas que la default branch, y en el inicio sugiéren crear un PR desde aquellas ramas.

<p align="center">
  <img src="./images/image.png">
</p>

<p align="center">
  [Notificación de crear PR]
</p>

En caso de que no aparezca dicha notificación en la página principal, se podrán seguir los siguientes pasos desde la misma web también.

1. En la barra de navegacioón hacer click en `Pull Request`

<p align="center">
  <img src="./images/image-1.png">
</p>

<p align="center">
  [Opción Pull Request seleccionada]
</p>

2. En esta ventana, podemos visualizar nuevamente la notificación, pero en caso de que no aparezca o no sugiera la rama que queremos mergear, debemos de hacer click en `New pull request`.
<p align="center">
  <img src="./images/image-2.png">
</p>

<p align="center">
  [Inicio en la opción de Pull Request]
</p>

3. Una vez indicado la creación de un pull request, nos va aparecer la opción de seleccionar cual es la source branch que se quiere mergear a una target branch.
<p align="center">
  <img src="./images/image-3.png">
</p>

<p align="center">
  [Inicio en creación de un PR]
</p>

4. Una vez seleccionada una source branch diferente a la target branch y con diferentes cambios, se nos habilitara el botón para crear el PR junto con un histórico de commits a mergear y los archivos modificados.
<p align="center">
  <img src="./images/image-4.png">
</p>

<p align="center">
  [Inicio en creación de un PR]
</p>

5. Una vez indicado que se quiere crear el PR, nos aparecera un formulario para configurar cierto PR
<p align="center">
  <img src="./images/image-5.png">
</p>

<p align="center">
  [Formulario de creación del PR]
</p>

6. En este formulario se deberán de completar la siguiente información:

   - La descripción del PR. Aquellos lugares con [] se deberan de poner algun valor si aplica borrando los parentesis
    <p align="center">
     <img src="./images/image-6.png">
   </p>
   <p align="center">
     [Descripción del PR]
   </p>

   - Por ultimo, indicar en que estado el PR se encuentra, si en Draft o Ready For Review
   <p align="center">
     <img src="./images/image-9.png">
   </p>
   <p align="center">
     [Draft o Ready For Review]
   </p>

7. Una vez completado el formulario y creado el PR podremos ver que las github actions se estan ejecutando y podremos ver aquí mismo si fallan o pasan.
<p align="center">
     <img src="./images/image-10.png">
   </p>
   <p align="center">
     [Estado de las github actions]
   </p>

8. También se podrá ver el estado (abierto, cerrado o mergeado) del PR junto con su identificador
<p align="center">
     <img src="./images/image-11.png">
   </p>
   <p align="center">
     [Estado del PR con su identificador]
   </p>

## Lecturas recomendadas

- [Info sobre PRs](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/proposing-changes-to-your-work-with-pull-requests/about-pull-requests)

- [Creación de un PR](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/proposing-changes-to-your-work-with-pull-requests/creating-a-pull-request?platform=windows)
