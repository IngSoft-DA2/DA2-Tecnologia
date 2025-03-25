[Volver - Pruebas Unitarias](https://github.com/IngSoft-DA2/DA2-Tecnologia/blob/unit-testing/README.md)

# Configuración de proyecto de prueba unitaria

El framework a utilizar para simular el comportamiento de las dependencias es `Moq`. Este debera de ser agregado a los proyectos de prueba unitaria para adquerir todas las funcionalidades del framework y crear los casos de prueba correspondientes.

`Moq` define diferentes comportamientos que determinan como un objeto `mock` se debería de comportar cuando sus métodos son invocados. Estos comportamientos le permiten a los desarrolladores especificar respuestas o acciones que el objeto mock debería de realizar durante la prueba. Estos comportamientos son:

- Strict: haciendo que se lance una excepcion en caso de que la llamada actual no cumpla con la esperada

- Loose: se retornara un valor por defecto del tipo a retornar en caso de no cumplir con lo esperado

También se deberá de instalar el paquete `FluentAssertions` el cual es una librería que provee una sintaxis mas expresiva para escribir `assertions` en las pruebas, haciendo mas legible y fácil de entender.

Algunos puntos claves sobre `FluentAssertions`:

- Sintaxis fluida: ofrece una API fluida que le permite a los desarrolladores encadenar multiples assertions juntas en una sola declaración, resultando un código de prueba mas legible y conciso. La sintaxis fluida se asemeja a un lenguaje natural, haciendo mas fácil de expresar la intención de la prueba. Por ejemplo:

```C#
someObject.Should().NotBeNull().And.BeOfType<MyClass>().And.BeEquivalentTo(expectedObject);
```

- Set amplio de assertions: provee un set de métodos de assert para varios tipos de datos y escenarios. Soporta assertions de objetos, colecciones, strings, números, exceptions, y más. Este set tan amplio de métodos cubre muchos escenarios y ayuda a los desarrolladores a escribir pruebas más robustas.

```C#
someObject.Should().Be(expectedObject);
collection.Should().Contain(expectedItem).And.NotContain(unexpectedItem);
```

- Mensajes de fallo claros: cuando un assert falla, se genera un mensaje de error claro y descriptivo que ayuda al desarrollador fácilmente realizar un diagnóstico del issue. El mensaje de error provee información detallada sobre los valores esperados y actuales, haciendo más fácil identificar la causa de fallo.

```C#
Expected someObject to be <expectedObject>, but found <actualObject>.
```

- Reglas custom de assertion: le permite a los desarrolladores definir reglas custom o extender los metodos para las necesidades específicas de la prueba. Esta flexibilidad ayuda a los desarrolladores extender funcionalidades de `FluentAssertions` y crear assertions más específicas al negocio.

```C#
someObject.Should().Satisfy<MyClass>(obj => obj.CustomProperty == expectedValue);
```

- [Visual studio](https://github.com/daniel18acevedo/DA2-Tecnologia/blob/unit-testing/config-unit-test-project-visual-studio.md)

- [Por comandos](https://github.com/daniel18acevedo/DA2-Tecnologia/blob/unit-testing/config-unit-test-project-dotnet-cli.md)
