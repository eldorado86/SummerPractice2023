using PetShop.DeveloperTesting.DomainLayer.Models;
using PetShop.DeveloperTesting.DataAccess.Repositories;
using PetShop.DeveloperTesting.DomainLayer.Context;

namespace StoreApi.DataAccess.Repositories
{
    public class PetRepository: IPetsRepository
    {
        private readonly PetsDbContext _dbContext;

        public PetRepository(PetsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddNewPet(Pet newPet)
        {
            _dbContext.Pets.Add(new Pet());
            _dbContext.SaveChanges();
        }

        public void RemovePet(Pet pet)
        {
            _dbContext.Pets.Remove(pet);
            _dbContext.SaveChanges();
        }
    }
}
