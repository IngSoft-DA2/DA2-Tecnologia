using Vet.WebApi.Models.Read;

namespace Vet.WebApiTest.Comparers
{
    public class DogBasicInfoModelComparer : BaseComparer<DogBasicInfoModel>
    {
        protected override bool AreEqual(DogBasicInfoModel expected, DogBasicInfoModel actual)
        {
            var equals = expected.Id == actual.Id;
            equals &= expected.Name == actual.Name;

            return equals;
        }
    }
}