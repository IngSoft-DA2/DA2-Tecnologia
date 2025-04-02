# Restricciones

## 1. Interfaz uniforme

Define una interfaz (un contrato), con un set de funcionalidades para una interacción cliente-servidor. Permite una evolución de la implementacion independientemente de la evolución del cliente. Esto implica un desacoplamiento entre ambos extremos.

Para lograr esto se deben seguir los siguientes principios:

- Basarse en recursos
- Respuestas con la información necesaria para la manipulación

## 2. Comunicación Stateless

Toda la información para interpretar la solicitud, tiene que estar contenida en la request misma (en el endpoint, body, headers, query params, etc). Esto es porque el servidor no mantiene una conexión viva con el cliente.

Debido a esto, es necesario mandar la información correspondiente en la request, para que el servidor pueda identificar quien es el que hace la request para tomar acciones.

Esta restricción impacta tanto al servidor como al cliente, ya que desde el lado del servidor se debe poder identificar el propietario de la request según la información que mande y desde el lado del cliente se debe guardar la identificación a mandar en futuras interacciones con el servidor.

Esta restricción tiene la ventaja de lograr que el servidor sea mas fácil de escalar y de ser distribuido. Esto implica poder realizar redespliegues fácilmente en casos de fallo y tambien un escalado individual.

Esto es gracias a que cada request puede ser dirigida a cualquier instancia/nodo/componente ya que no se establece una conexión punto a punto para una entablar una comunicación.

<p align="center">
  <img src="images/image-15.png"/>
</p>

<p align="center">
[Escalabilidad]
</p>

<p align="center">
  <img src="images/image-16.png"/>
</p>

<p align="center">
[Multiples conexiones]
</p>

## 3. Cacheable

Algunas respuestas del servidor pueden ser cacheadas por los clientes. Para lograr esto, las respuestas con tal objetivo, deben definir algún parámetro para indicar que tal respuesta puede ser cacheada.

Esto quiere decir que si una respuesta es marcada como cacheable, el cliente va a poder persistir esa respuesta desde su lado y consumirla más rápidamente sin la necesidad de ir al servidor.

Esto se hace en aquellas respuestas que se saben que no varian con el tiempo.

Esta restricción bien utilizada puede lograr eliminar la interacción con el servidor de forma parcial o total con el objetivo de mejorar la escalabilidad y performance.

<p align="center">
  <img src="images/image-17.png"/>
</p>

## 4. Cliente-Servidor

Al tener una interfaz uniforme logramos separar los clientes de los servidores. Esto permite que los clientes no se preocupen de como los datos son almacenados a procesados, ya que es trabajo del servidor. Esto implica que el código del cliente sea más portable, ligero.

Esto tiene la misma implicancia del lado del servidor, a este no le preocupa manejar una interfaz de usuario o el estado del mismo, ya que los servidores son mas escalables.

Siempre y cuando la interfaz, el contrato, siga cumpliendose, los clientes y los servidores pueden variar.

<p align="center">
  <img src="images/image-18.png"/>
</p>

## 5. Tiered y Layered system

Esta restricción implica que un cliente no puede deducir si esta comunicandose con un servidor final o un intermediario (tier).

Los servidores intermediarios permiten mejorar la escalabilidad a traves de habilitar el balanceo de carga y caches compartidos. Cada capa también puede exigir políticas de seguridad. A su vez, se base en una arquitectura en capas.

<p align="center">
  <img src="images/image-19.png"/>
</p>