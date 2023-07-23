using PetShop.DeveloperTesting._02.ServiceLayer.Exceptions;
using PetShop.DeveloperTesting.Business.Entities;
using PetShop.DeveloperTesting.DomainLayer.Models;
using PetShop.DeveloperTesting.ServiceLayer.Contracts;
using RepositoryLayer;

namespace PetShop.DeveloperTesting.ServiceLayer.Services
{
    public class PetService : IPetService
    {
        private readonly IRepository<Pet> _petsRepository;
        private readonly IRepository<PetCategory> _petsCategoryRepository;

        public PetService(IRepository<Pet> petsRepository, IRepository<PetCategory> petsCategporyRepository)
        {
            _petsRepository = petsRepository;
            _petsCategoryRepository = petsCategporyRepository;
        }

        public int IncreasePetSupply(PetDto newPet)
        {
            //Validation rules
            var newPetCategory = _petsCategoryRepository.Get(newPet.CategoryId);

            //1. If we want to buy a cat, but we have at least 10 cats, throw exception
            if (newPetCategory?.Name == "Cat")
            {                
                //1. if we want to buy a cat, but already have dogs, throw exception
                var dogs = _petsCategoryRepository.GetAll().Where(x => x.Name == "Dog").FirstOrDefault();
                if (dogs?.Quantity > 0)
                {
                    throw new ConflictException("You cannot buy a cat, because we already have a dog!");
                }
                
                if (newPetCategory?.Quantity >= 10)
                {
                    throw new OverloadException("Too many cats!");
                }
            }

            var pet = new Pet()
            {
                Name = newPet.Breed,
                CategoryId = newPet.CategoryId,
                Quantity = newPet.Quantity
            };

            var newPetId = _petsRepository.Insert(pet);

            newPetCategory.Quantity = newPetCategory.Quantity + newPet.Quantity;
            _petsCategoryRepository.Update(newPetCategory);

            return newPetId;
        }

        public IEnumerable<PetDto> GetAll()
        {
            var allPets = _petsRepository.GetAll()
                .Select(x =>
                    new PetDto
                    {
                        Id = x.Id,
                        Breed = x.Name,
                    });
            return allPets;
        }

        public void SellPet(PetDto pet)
        {
            var petCategory = _petsCategoryRepository.Get(pet.CategoryId);

            if (petCategory.Quantity < pet.Quantity)
            {
                throw new OutOfStockException($"There is no {pet.Breed} on the stock.");
            }

            _petsRepository.Delete(pet.Id);

            //var petCategory = _petsCategoryRepository.Get(pet.Category.Id);
            petCategory.Quantity = petCategory.Quantity - pet.Quantity;

            _petsCategoryRepository.Update(petCategory);
        }
    }
}
