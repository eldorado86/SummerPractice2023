using PetShop.DeveloperTesting._02.ServiceLayer.Contracts;
using PetShop.DeveloperTesting.DomainLayer.Models;
using RepositoryLayer;
using StoreApi.Entities;

namespace PetShop.DeveloperTesting.ServiceLayer.Services
{
    public class PetCategoriesService : IPetCategoriesService
    {
        private readonly IRepository<PetCategory> _petCategoriesRepository;

        public PetCategoriesService(IRepository<PetCategory> petCategoriesRepository)
        {
            _petCategoriesRepository = petCategoriesRepository;
        }

        public IEnumerable<PetCategoryDto> GetCategories()
        {
            var allcategories = _petCategoriesRepository.GetAll()
                .Select(x => new PetCategoryDto { Id = x.Id, Name = x.Name,  Price = x.Price, Quantity = x.Quantity })
                .ToList();

            return allcategories;
        }

        public void UpdateCategory(PetCategoryDto petCategory)
        {
            var petCategoryEntity = new PetCategory()
            {
                Id = petCategory.Id,
                Name = petCategory.Name,
                Price = petCategory.Price,
                Quantity = petCategory.Quantity
            };

            _petCategoriesRepository.Update(petCategoryEntity);
        }
    }
}
