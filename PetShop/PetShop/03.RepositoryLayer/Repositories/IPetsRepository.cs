using PetShop.DeveloperTesting.DomainLayer.Models;

namespace PetShop.DeveloperTesting.DataAccess.Repositories
{
    public interface IPetsRepository
    {
        public void AddNewPet(Pet newPet);
        public void RemovePet(Pet pet);
    }
}
