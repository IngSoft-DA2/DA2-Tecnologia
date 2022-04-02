using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Vet.Api.BusinessLogic;
using Vet.BusinessLogicInterface;
using Vet.Domain;
using Vet.WebApi.Models.Read;
using Vet.WebApiTest.Comparers;

namespace Vet.WebApiTest
{
    [TestClass]
    public class UnitTest1
    {
        private DogController _dogController;
        private Mock<IDogLogic> _dogLogicMock;

        [TestInitialize]
        public void Initialize()
        {
            this._dogLogicMock = new Mock<IDogLogic>(MockBehavior.Strict);
            this._dogController = new DogController(this._dogLogicMock.Object);
        }

        [TestMethod]
        public void TestGetAllDogsOk()
        {
            var expectedDogs = new List<Dog>
            {
                new Dog
                {
                    Id = 1,
                    Name = "Emma"
                }
            };

            this._dogLogicMock.Setup(dogLogic => dogLogic.GetAll()).Returns(expectedDogs);
            var response = this._dogController.GetAllDogs();
            var specificResponse = response as OkObjectResult;
            var value = specificResponse.Value as IEnumerable<DogBasicInfoModel>;
            
            this._dogLogicMock.VerifyAll();
            CollectionAssert.AreEqual(
                expectedDogs.Select(dog => new DogBasicInfoModel { Id = dog.Id, Name=dog.Name }).ToList(), 
                value.ToList(), 
                new DogBasicInfoModelComparer());
        }
    }
}
