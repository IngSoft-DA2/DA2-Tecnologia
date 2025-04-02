# Controller

Son las clases que juegan un rol fundamental para el procesamiento de requests HTTP y la generación de respuestas.

Estas clases contienen funciones responsables en procesar las request HTTP y retornar la respuesta correspondiente a la petición. Cada controller representa un set de endpoints asociado a un recurso, las cuales son mapeadas a funciones dentro de la clase. Estos endpoints son las funcionalidades de la web api disponibles a utilizar a través de http.

Estas clases tienen tres responsabilidades:

- Mapeo de endpoint a funciones
- Procesamiento de las requests
- Generar una respuesta

Estos elementos son nada más que clases, por convención deben estar ubicadas dentro de la carpeta ```Controllers```.

En resumen son el punto de entrada para request http hacia nuestra web api y tienen el trabajo de orquestar el procesamiento de estas requests y responderle a los clientes. Encapsulan la lógica de manejar la comunicación HTTP y son un aspecto crucial.

Al ser el punto de entrada a nuestra web api, estas clases son utilizadas por .NET Core, no serán instanciadas para utilizar en la lógica, serán instanciadas para pruebas unitarias.

Cuando una request llega a la web api, .NET Core usa la configuración de enrutamiento para determinar qué controller y qué función debería de manejar la request. Una vez que el controller y la función son identificados, el framework crea una instancia del controller e invoca la función apropiada para procesar la request.

- [Elementos de un controller](https://github.com/daniel18acevedo/DA2-Tecnologia/blob/web-api/controller-elements.md)
- [Buenas práticas](https://github.com/daniel18acevedo/DA2-Tecnologia/blob/web-api/controller-good-practices.md)
