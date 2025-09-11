[⬅️ Volver - Pruebas Unitarias](https://github.com/IngSoft-DA2/DA2-Tecnologia/blob/unit-testing/README.md)

# ⚙️ Configuración de Proyecto de Prueba Unitaria

¡Asegura la calidad y robustez de tu código configurando correctamente tus proyectos de pruebas unitarias!  
En esta guía aprenderás a integrar dos herramientas clave: **Moq** y **FluentAssertions**.

---

## 🪄 Moq

El framework recomendado para simular el comportamiento de dependencias es **`Moq`**.  
Agrega este paquete a tus proyectos de prueba para aprovechar todas sus funcionalidades:

- 🎭 **Mocks:** Permite crear objetos simulados para aislar la unidad bajo prueba.
- 🧩 **Comportamientos de Mock:**  
  - 🔒 **Strict:** Lanza una excepción si se realiza una llamada no prevista.
  - 🪶 **Loose:** Devuelve un valor por defecto si la llamada no está configurada.

```bash
dotnet add package Moq
```

---

## ✨ Recomendación: Usa FluentAssertions para tus pruebas

Se recomienda ampliamente utilizar **FluentAssertions** en tus proyectos de pruebas unitarias por las siguientes ventajas:

- 💬 **Sintaxis fluida y expresiva:** Permite encadenar múltiples assertions en una sola declaración, haciendo que el código de pruebas sea más legible, claro y fácil de mantener.
- 🧰 **Cobertura amplia:** Incluye métodos de assertion para objetos, colecciones, cadenas, números, excepciones y más, permitiéndote testear una gran variedad de escenarios con facilidad.
- 📣 **Mensajes de error descriptivos:** Cuando una assertion falla, se genera un mensaje claro y detallado que facilita el diagnóstico y la resolución del problema.
- 🛠️ **Flexibilidad:** Permite definir reglas personalizadas para assertions, adaptándose a las necesidades específicas de tus pruebas.

### Ejemplos de uso

```csharp
someObject.Should().NotBeNull().And.BeOfType<MyClass>().And.BeEquivalentTo(expectedObject);
collection.Should().Contain(expectedItem).And.NotContain(unexpectedItem);
someObject.Should().Satisfy<MyClass>(obj => obj.CustomProperty == expectedValue);
```

> Utilizar FluentAssertions no solo mejora la calidad de tus pruebas, sino que también contribuye a una mejor experiencia de desarrollo y mantenimiento.

```bash
dotnet add package FluentAssertions
```

---

## 🛠️ Pasos de Configuración

Elige el método que prefieras para configurar tu proyecto:

- 🖥️ [Visual Studio](https://github.com/daniel18acevedo/DA2-Tecnologia/blob/unit-testing/config-unit-test-project-visual-studio.md)
- 💻 [Por comandos](https://github.com/daniel18acevedo/DA2-Tecnologia/blob/unit-testing/config-unit-test-project-dotnet-cli.md)

---

¡Con estas herramientas tendrás una base sólida para escribir pruebas unitarias profesionales y efectivas! 🚀
