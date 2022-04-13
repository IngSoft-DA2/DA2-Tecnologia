using Vet.WebApi.Models.Read;

namespace Vet.WebApiTest.Comparers
{
  public class DogDetailInfoModelComparer : BaseComparer<DogDetailInfoModel>
  {
    protected override bool AreEqual(DogDetailInfoModel expected, DogDetailInfoModel actual)
    {
      var equals = expected.Id == actual.Id;
      equals &= expected.Name == actual.Name;
      equals &= expected.Age == actual.Age;
      equals &= expected.Race == actual.Race;

      return equals;
    }
  }
}