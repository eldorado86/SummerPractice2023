using PetShop.DeveloperTesting.DomainLayer.Models;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.UnitTests
{
    public class FakePetRepository : IRepository<Pet>
    {
        public void Delete(int entityId)
        {
            throw new NotImplementedException();
        }

        public Pet? Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pet> GetAll()
        {
            throw new NotImplementedException();
        }

        public int Insert(Pet entity)
        {
            return 1;
        }

        public void Update(Pet entity)
        {
            throw new NotImplementedException();
        }
    }
}
