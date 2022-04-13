using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Vet.BusinessLogicInterface;
using Vet.Domain;
using Vet.WebApi.Models.Read;
using Vet.WebApi.Models.Write;

namespace Vet.Api.BusinessLogic
{
  [ApiController]
  [Route("dogs")]
  public class DogController : ControllerBase
  {
    private readonly IDogLogic _dogLogic;

    public DogController(IDogLogic dogLogic)
    {
      this._dogLogic = dogLogic;
    }

    [HttpGet]
    public IActionResult GetAllDogs()
    {
      var dogsSaved = this._dogLogic.GetAll();
      var dogsConverted = dogsSaved.Select(dog => new DogBasicInfoModel
      {
        Id = dog.Id,
        Name = dog.Name
      });

      return Ok(dogsConverted);
    }

    [HttpGet("{dogId}", Name = "GetDogById")]
    public IActionResult GetDog(int dogId)
    {
      var dogSaved = this._dogLogic.GetById(dogId);

      if (dogSaved is null)
      {
        return NotFound();
      }

      return Ok(new DogDetailInfoModel
      {
        Id = dogSaved.Id,
        Name = dogSaved.Name,
        Age = dogSaved.Age,
        Race = dogSaved.Race,
        Owner = new OwnerBasicInfoModel
        {
          Id = dogSaved.OwnerId,
          Name = dogSaved.Owner.Name,
          Address = dogSaved.Owner.Address,
          PhoneNumber = dogSaved.Owner.PhoneNumber
        }
      });
    }

    [HttpPost]
    public IActionResult CreateAdog(DogModel dog)
    {
      var newDog = new Dog
      {
        Name = dog.Name,
        Age = dog.Age,
        Race = dog.Race,
        OwnerId = dog.OwnerId
      };

      var dogSaved = this._dogLogic.Add(newDog);

      var dogSavedConverted = new DogDetailInfoModel
      {
        Id = dogSaved.Id,
        Name = dogSaved.Name,
        Owner = new OwnerBasicInfoModel
        {
          Id = dogSaved.OwnerId
        },
        Age = dogSaved.Age,
        Race = dogSaved.Race,
      };

      return CreatedAtRoute("GetDogById", new { dogId = dogSaved.Id }, dogSavedConverted);
    }

    [HttpPut("{dogId}")]
    public IActionResult UpdateAdog(int dogId, DogModel dog)
    {
      var dogConverted = new Dog
      {
        Age = dog.Age
      };
      try
      {
        this._dogLogic.Update(dogId, dogConverted);
      }
      catch (ArgumentNullException ex)
      {
        return NotFound(new { message = ex.Message });
      }

      return NoContent();
    }

    [HttpDelete("{dogId}")]
    public IActionResult DeleteAdog(int dogId)
    {
      this._dogLogic.Delete(dogId);

      return NoContent();
    }
  }
}
