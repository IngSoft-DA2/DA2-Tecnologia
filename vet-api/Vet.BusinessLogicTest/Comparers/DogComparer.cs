using Vet.Domain;

namespace Vet.BusinessLogicTest.Comparers
{
  public class DogComparer : BaseComparer<Dog>
  {
    public override bool AreEqual(Dog expected, Dog actual)
    {
      var equals = expected.Id == actual.Id;
      equals &= expected.Name == actual.Name;
      equals &= expected.Age == actual.Age;
      equals &= expected.Race == actual.Race;

      return equals;
    }
  }
}