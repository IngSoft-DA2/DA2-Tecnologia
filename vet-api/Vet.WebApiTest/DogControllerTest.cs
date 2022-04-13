using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Vet.Api.BusinessLogic;
using Vet.BusinessLogicInterface;
using Vet.Domain;
using Vet.WebApi.Models.Read;
using Vet.WebApi.Models.Write;
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
          expectedDogs.Select(dog => new DogBasicInfoModel { Id = dog.Id, Name = dog.Name }).ToList(),
          value.ToList(),
          new DogBasicInfoModelComparer());
    }

    [TestMethod]
    public void GetDogsByIdOk()
    {
      var retrievedDog = new Dog
      {
        Id = 1,
        Name = "Emma",
        Age = 2,
        OwnerId = 3,
        Race = "Salchicha",
        Owner = new User()
        {
          Id = 3,
          Name = "Pedro",
          PhoneNumber = "098765432",
          Address = "Somewhere"
        }
      };
      var expectedDog = new DogDetailInfoModel
      {
        Id = retrievedDog.Id,
        Name = retrievedDog.Name,
        Owner = new OwnerBasicInfoModel
        {
          Id = retrievedDog.OwnerId
        },
        Age = retrievedDog.Age,
        Race = retrievedDog.Race,
      };

      this._dogLogicMock.Setup(dogLogic => dogLogic.GetById(It.IsAny<int>())).Returns(retrievedDog);
      var response = this._dogController.GetDog(1);
      var specificResponse = response as OkObjectResult;
      var value = specificResponse.Value as DogDetailInfoModel;

      this._dogLogicMock.VerifyAll();
      Assert.IsTrue(new DogDetailInfoModelComparer().Compare(expectedDog, value) == 0);
    }

    [TestMethod]
    public void GetDogsByIdNotFound()
    {
      this._dogLogicMock.Setup(dogLogic => dogLogic.GetById(It.IsAny<int>())).Returns(default(Dog));
      var response = this._dogController.GetDog(1);
      var specificResponse = response as NotFoundResult;

      this._dogLogicMock.VerifyAll();
      Assert.AreEqual(specificResponse.StatusCode, 404);
    }

    [TestMethod]
    public void CreateDogOk()
    {
      var dogModel = new DogModel
      {
        Name = "Emma",
        Age = 2,
        OwnerId = 3,
        Race = "Salchicha",
      };
      var createdDog = new Dog
      {
        Name = dogModel.Name,
        Age = dogModel.Age,
        Race = dogModel.Race,
        OwnerId = dogModel.OwnerId
      };
      var expectedDog = new DogDetailInfoModel
      {
        Id = createdDog.Id,
        Name = createdDog.Name,
        Owner = new OwnerBasicInfoModel
        {
          Id = createdDog.OwnerId
        },
        Age = createdDog.Age,
        Race = createdDog.Race,
      };

      this._dogLogicMock.Setup(dogLogic => dogLogic.Add(It.IsAny<Dog>())).Returns(createdDog);
      var response = this._dogController.CreateAdog(dogModel);
      Console.WriteLine(response);
      var createdResult = response as CreatedAtRouteResult;
      var value = createdResult.Value as DogDetailInfoModel;

      this._dogLogicMock.VerifyAll();
      Assert.IsTrue(new DogDetailInfoModelComparer().Compare(expectedDog, value) == 0);
    }

    [TestMethod]
    public void UpdateDogOk()
    {
      int dogToUpdateId = 1;
      var dogModel = new DogModel
      {
        Age = 2,
      };
      this._dogLogicMock.Setup(dogLogic => dogLogic.Update(It.IsAny<int>(), It.IsAny<Dog>()));
      var response = this._dogController.UpdateAdog(dogToUpdateId, dogModel);
      var specificResult = response as NoContentResult;

      this._dogLogicMock.VerifyAll();
      Assert.AreEqual(specificResult.StatusCode, 204);
    }

    [TestMethod]
    public void UpdateDogNotFound()
    {
      int dogToUpdateId = -1;
      var dogModel = new DogModel
      {
        Age = 2,
      };
      this._dogLogicMock.Setup(dogLogic => dogLogic.Update(It.IsAny<int>(), It.IsAny<Dog>())).Throws(new ArgumentNullException());
      var response = this._dogController.UpdateAdog(dogToUpdateId, dogModel);
      var specificResult = response as NotFoundObjectResult;

      this._dogLogicMock.VerifyAll();
      Assert.AreEqual(specificResult.StatusCode, 404);
    }

    [TestMethod]
    public void DeleteADogOk()
    {
      int dogToDeleteId = -1;
      this._dogLogicMock.Setup(dogLogic => dogLogic.Delete(It.IsAny<int>()));
      var response = this._dogController.DeleteAdog(dogToDeleteId);
      var specificResult = response as NoContentResult;

      this._dogLogicMock.VerifyAll();
      Assert.AreEqual(specificResult.StatusCode, 204);
    }
  }
}
