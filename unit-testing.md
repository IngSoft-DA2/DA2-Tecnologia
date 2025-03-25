[Volver - Pruebas Unitarias](https://github.com/IngSoft-DA2/DA2-Tecnologia/blob/unit-testing/README.md)

# Pruebas

Las pruebas son un aspecto fundamental en el desarrollo de software de calidad. En base a un set de pruebas aseguran que el código desarrollado funciona como es de esperar y es libre de bugs.

Existen varios tipos de pruebas, cada una con un propósito diferente para validar aspectos específicos del sistema.

<p align="center">
<img src='./images/image-29.png'>
</p>
<p align="center">
[Pirámide de testing]
</p>

# Pruebas End-To-End (E2E)
Se enfocan en evaluar el sistema en su totalidad, desde que se aprieta un botón de guardar en la UI, hasta que impacta la infomación en la base de datos. Simulan flujos enteros de uso como los realizaría un usuario real. De esta forma se aseguran que los requerimientos del negocio se cumplen desde el cliente a través de la UI hasta el Servidor y luego la base de datos.

Ya que consisten en probar todo el sistema en conjunto, su configuración es extensa, y son costos en cuanto a tiempo y recursos. Dado esto, se utilizan en la menor medida posible y solo con el fin de testear flujos críticos.

# Pruebas de integración

Estas pruebas se concentran en probar la interacción entre diferentes módulos/capas del sistema. Verifica que módulos individuales funcionen correctamente en grupo. Este tipo de pruebas pueden ser realizadas en diferentes niveles del sistema.

Si bien abarcan menos que los tests E2E, las mismas requieren de igual manera un nivel de configuración grande ya que prueban la comunicación entre varias capas del sistema.

Al igual que las pruebas E2E no suelen ser usadas de forma exhaustiva ya que el consumo de las mismas es demasiado grande. Se suelen utilizar para probar cosas puntuales.

# Pruebas unitarias

Son el primer nivel de pruebas que involucran probar de forma individual componentes de forma aislada a otros componentes. Están enfocadas en verificar la correctitud de trozos de código chicos y aislados. Los desarrolladores usualmente escriben pruebas unitarias en conjunto con el código que desarrollan, asegurandose que cada componente se comporta de la manera que espera.

Estas pruebas deberan solamente probar código que esté al alcance del desarrollador y solo un componente a la vez.

Este tipo de pruebas nos ayudarán a probar nuestro código sin la necesidad de probar las dependencias al mismo tiempo, esto ayudará a los desarrolladores a encontrar errores ubicados únicamente en la porción de código que se está probando.

Es por estas razones que son la base de la pirámide de testing, por sus características nos permiten avanzar de a pequeños pasos asegurandonos que cada paso que damos al codificar está testeado y funciona como la lógica del negocio lo requiere. Para tener un código seguro y que luego no debamos realizar cambios tardíos, inoportunos y costosos, es escencial un gran set de tests unitarios.

**Características de los test unitarios según Clean Code:**

En el libro "Clean Code: A Handbook of Agile Software Craftmanship" se indica que los tests unitarios deben cumplir *FIRST*.

 - *Fast*: La ejecución se realizará rápidamente, de manera que no interfiera con el flujo de programación. Una vez que se implementa un método el desarrollador debe poder tener rápidamente el resultado de los tests para asegurarse que no agregó ningún bug y todo funciona correctamente, o por el contrario, corregir de forma temprana los errores.

- *Independent*: Los tests tienen que ser independientes entre sí. El éxito de un test no puede depender de el éxito de los tests anteriores. Justamente la idea de los tests unitarios es probar trozos pequeños de código para poder indentificar exactamente que no funciona correctamente, si los tests son dependientes al fallar un test fallan los tests siguientes y no podremos identificar con exactitud qué funciona y qué no.

- *Repeatable*: El desarrollador debe poder repetirlos las veces que quiera, y cada vez que los repita para un caso A siempre deben retornar un resultado B. Deben ser consistentes con la información que brindan.

- *Self-Validating*: Se deben validar a sí mismos, recordemos debemos tener un gran set de tests unitarios, y una validación manual nos quitaría las ventajas de velocidad y seguridad que los tests unitarios nos ofrecen.  Esto se realiza a través de assertions, los cuales son declaraciones que verifican el comportamiento esperado de la prueba unitaria. Estas declaraciones comparan el resultado actual con lo esperado.

- *Timely*: Este punto refiere a que los tests unitarios se realicen de manera oportuna. Como nombramos anteriormente, una base de tests unitarios grande y concisa nos permite asegurarnos de que nuestro código funciona correctamente y evita que a los meses de trabajo encontremos bugs que sean costosos de solucionar y que pongan en riesgo el funcionamiento de nuestro sistema. En base a esto es fundamental que los test se realicen de forma oportuna, en conjunto al desarrollo del código, no luego. En nuestro caso utilizamos Test-Driven-Development (TDD) que nos obliga a crear el código conducido por los tests que creamos de forma previa, asegurando ser oportuno.


**Otras características fundamentales son:**

- *Aislamiento*: las pruebas deberán de probar componentes de forma aislada a sus dependencias

- *Diseñadas en función de test cases*: estas pruebas giran en torno a casos de prueba, los cuales son escenarios específicos o entradas diseñadas para verificar un comportamiento de una unidad individual bajo diferentes condiciones. Los casos de prueba deben cubrir un set de escenarios grande para asegurar una cobertura completa

- *Mocking y Stubbing*: las dependencias externas a la porción de código son remplazadas con objetos mock o stubs para simular el comportamiento. Esto le permite a los desarrolladores controlar el ambiente en el cual la prueba es ejecutada y concentrarse solamente en probar la porción de código que corresponde probar.

- *Integración continua*: las pruebas unitarias son escenciales para la integración y entrega de código de forma continua. Estas pruebas son ejecutadas de forma automática como parte de un proceso que asegura que el código compila y está libre de bugs.

- *Seguridad al refactorear*: un set bueno de pruebas unitarias, genera una sensación de seguridad a la hora de refactorear el código. Cuando un desarrollador realiza cambios, este puede introducir errores en alguna parte del código, la forma de asegurarse de que eso no suceda, es chequeando si alguna prueba unitaria falla después de realizar los cambios.

Otro tipo de pruebas son:
- *System Testing*: evaluan la completitud e integración del producto para asegurar que cumple con las espectativas de los requerimientos. Prueba el sistema como una entidad contra los aspectos funcionales y no funcionales requeridos, incluyendo performance, escalabilidad y seguridad.

- *Acceptance Testing*: son ejecutadas para validar si el sistema cumple con los criterios de aceptación y si está listo para deployar. Típicamente involucra probar el sistema como un usuario final para asegurar que satisface los requerimientos de usuario y reglas del negocio.

- *Regression Testing*: aseguran que nuevo código no afecta a funcionalidades existentes. Incolucra volver a correr pruebas previamente ejecutadas para verificar que los cambios recientes no introdujeron nuevos defectos.

- *Performance Testing*: evalúan la estabilidad, escalabilidad y recursos usados de un sistema bajo diferentes cargas de trabajo. Ayudan a identificar cuellos de botella y aseguran que el sistema cumple con los requerimientos de performance. Algunas técnicas son load testing, stress testing y scalability testing.

- *Security Testing*: se concentra en identificar vulnerabilidades y debilidades en el sistema que pueden ser explotadas por usuarios maliciosos. Involucran técnicas como penetration testing y vulnerability scanning para evaluar la postura de seguridad del sistema y mitigar potenciales riesgos.

- *User Experience (UX) Testing*: evalúa la usabilidad y la experiencia de usuario del sistema para asegurar que es intuitiva, eficiente y disfrutable para usuarios finales. Involucra obtener feedback de usuarios reales mediante encuestas, entrevistas y sesiones de usabiliad para identificar áreas de mejoras.

## Mocks

Los mocks son objetos que simulan el comportamiento de un objeto real de forma controlada. Son usados principalmente en pruebas unitarias para aislar la unidad a probar de sus dependencias. Estos objetos imitan el comportamiento de componentes externos con los que la unidad a probar interactúa, por ejemplo: base de datos, servicios en la web u otros servicios o clases en el sistema.

Los mocks nos permiten verificar la interacción del sistema bajo prueba (SUT - system under test) con sus dependencias.

Algunas características de los mocks son:

- Simulación del comportamiento de un objeto.
- Aislamiento del código de sus dependencias
- Reducción de dependencias

Los mocks son un tipo de *test doubles*, estos son objetos que se utilizan en lugares de dependencias reales durante el testing para aislar una unidad de código específica y así controlar el comportamiento de las dependencias. Los *test doubles* le permiten al desarrollador crear ambientes controlados para las pruebas, asegurandose que las pruebas sean predecibles, repetibles e independientes a factores externos.

Algunos otros test doubles son:

- *Dummy*: son placeholder usados cuando un parámetro es requerido pero su valor es irrelevante a la prueba. Son típicamente pasados como argumentos pero nunca usados en la prueba en sí. Ayudan a satisfacer la firma de un método o constructor sin impactar en la lógica de la prueba.

- *Stub*: proveen una respuesta a la llamada de métodos durante una prueba. Simulan el comportamiento de objetos reales al retornar valores pre definidos o lanzar excepciones pre definidas. Son útiles para controlar el comportamiento de dependencias y asegurarse resultas consistentes.

- *Mock*: Son objetos pre programados con expectativas sobre las llamadas que esperan recibir durante la prueba. Le permiten a los desarrolladores especificar las interacciones esperadas entre la unidad que esta bajo prueba con sus dependencias. Pueden verificar los argumentos con los cuales son llamados los métodos, específicar un orden y frecuencia.

- *Fake*: son simplemente implementaciones de dependencias que proveen una forma alternativa a los objetos reales. Son usados típicamente en escenarios donde el objeto real no es práctico, como por ejemplo: el uso de base de datos en memoria en lugar de una base de datos en producción para pruebas. Estos sacrifican similitud por simpleza y velocidad.

- *Spy*: guardan las interacciones entre la unidad a probar con sus dependencias, permitiendo a los desarrolladores inspeccionar y verificar esas interacciones después de que la prueba fue ejecutada. Son útiles para monitorear como las pruebas interactúan con sus dependencias sin modificar su comportamiento.

Algunas ventajas de usar los diferentes test doubles son:

- Aislamiento
- Control
- Velocidad
- Flexibilidad

  ## Lecturas recomendadas
  - [Buenas practicas para pruebas unitarias - Documentación Microsoft](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices)
  - [Buenas practicas para pruebas unitarias - Medium](https://medium.com/@kaanfurkanc/unit-testing-best-practices-3a8b0ddd88b5)
