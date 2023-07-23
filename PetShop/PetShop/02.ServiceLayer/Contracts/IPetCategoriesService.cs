using StoreApi.Entities;

namespace PetShop.DeveloperTesting._02.ServiceLayer.Contracts
{
    public interface IPetCategoriesService
    {
        public IEnumerable<PetCategoryDto> GetCategories();
        public void UpdateCategory(PetCategoryDto petCategory);
    }
}
