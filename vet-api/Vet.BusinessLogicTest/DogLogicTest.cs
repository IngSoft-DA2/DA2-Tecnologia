using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vet.Domain;
using Vet.BusinessLogic;
using Vet.BusinessLogicTest.Comparers;

namespace Vet.BusinessLogicTest
{
  [TestClass]
  public class DogLogicTest
  {
    private DogLogic _dogLogic;

    [TestInitialize]
    public void Initialize()
    {
      this._dogLogic = new DogLogic();
    }

    [TestMethod]
    public void AddOk()
    {
      var newDog = new Dog
      {
        Name = "Perro",
        Race = "Salchicha",
        Age = 2,
      };
      var expectedDog = new Dog
      {
        Id = 1,
        Name = newDog.Name,
        Race = newDog.Race,
        Age = newDog.Age,
      };

      var response = this._dogLogic.Add(newDog);

      Assert.IsTrue(new DogComparer().AreEqual(response, expectedDog));
    }
  }
}
