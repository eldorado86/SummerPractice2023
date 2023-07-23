using PetShop.DeveloperTesting.Business.Entities;

namespace PetShop.DeveloperTesting.ServiceLayer.Contracts
{
    public interface IPetService
    {
        public IEnumerable<PetDto> GetAll();

        /// <summary>
        /// The store is selling a pet
        /// </summary>
        /// <param name="newPet"></param>
        public void SellPet(PetDto newPet);

        /// <summary>
        /// The store is increasing the pets supply
        /// </summary>
        /// <param name="newPet"></param>
        public int IncreasePetSupply(PetDto newPet);

        
    }
}
