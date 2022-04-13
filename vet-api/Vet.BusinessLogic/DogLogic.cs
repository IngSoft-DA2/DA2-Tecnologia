using System;
using System.Collections.Generic;
using System.Linq;
using Vet.BusinessLogicInterface;
using Vet.Domain;

namespace Vet.BusinessLogic
{
  public class DogLogic : IDogLogic
  {
    private static List<Dog> _dogs;

    public DogLogic()
    {
      if (_dogs is null)
      {
        _dogs = new List<Dog>();
      }
    }

    public Dog Add(Dog dog)
    {
      dog.Id = _dogs.Count + 1;
      dog.OwnerId = 1;
      dog.Owner = new User
      {
        Id = 1,
        Name = "Daniel Acevedo",
        Address = "Direccion",
        PhoneNumber = "Phone"
      };

      _dogs.Add(dog);

      return dog;
    }

    public void Delete(int dogId)
    {
      _dogs = _dogs.Where(dog => dog.Id != dogId).ToList();
    }

    public IEnumerable<Dog> GetAll()
    {
      return _dogs;
    }

    public Dog GetById(int dogId)
    {
      var dogSaved = _dogs.FirstOrDefault(dog => dog.Id == dogId);

      return dogSaved;
    }

    public void Update(int dogId, Dog dog)
    {
      var dogSaved = this.GetById(dogId);

      if (dogSaved is null)
      {
        throw new ArgumentNullException("Dog doesn't exist");
      }

      dogSaved.Age = dog.Age;
    }
  }
}
