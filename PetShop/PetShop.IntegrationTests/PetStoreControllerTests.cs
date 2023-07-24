using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PetShop.DeveloperTesting.Business.Entities;
using PetShop.DeveloperTesting.DomainLayer.Context;
using PetShop.DeveloperTesting.DomainLayer.Models;
using System.Text;

namespace PetShop.IntegrationTests
{
    public class PetStoreControllerTests: IDisposable
    {
        private HttpClient _httpClient;

        private PetsDbContext _petsDbContext;

        private IList<Pet> _petsToBeRemoved = new List<Pet>();
        private IList<PetCategory> _petsCategoryToBeRemoved = new List<PetCategory>();

        public PetStoreControllerTests()
        {
            var connectionString = "Server=localhost\\SQLEXPRESS;Database=SummerPractice.PetShop;Trusted_Connection=True;TrustServerCertificate=True;";
            var options = new DbContextOptionsBuilder<PetsDbContext>()
                .UseSqlServer(new SqlConnection(connectionString))
                .Options;

            _petsDbContext = new PetsDbContext(options);

            var webApplicationFactory = new WebApplicationFactory<Program>();
            _httpClient = webApplicationFactory.CreateDefaultClient();
        }

        public void Dispose()
        {
            CleanupDatabase();
        }

        [Fact]
        public async Task Given_pets_controller_When_calling_get_all_Then_should_return_all()
        {
            //Arrange
            var petCategory = new PetCategory()
            {
                Name = "Integration test",
                Price = 15.00,
                Quantity = 0
            };

            _petsDbContext.PetCategory.Add(petCategory);
            _petsDbContext.SaveChanges();

            _petsCategoryToBeRemoved.Add(petCategory);

            var pet = new Pet()
            {
                Breed = "Parrot",
                CategoryId = petCategory.Id,
                Quantity = 1
            };

            _petsDbContext.Pets.Add(pet);
            _petsDbContext.SaveChanges();

            _petsToBeRemoved.Add(pet);

            //Act
            var response = await _httpClient.GetAsync("/api/petstore");

            //Assert
            var responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            
            List<PetDto> allPets = new();
            if (!string.IsNullOrWhiteSpace(responseContent))
            {
                allPets = JsonConvert.DeserializeObject<List<PetDto>>(responseContent);
            }

            Assert.True(response.IsSuccessStatusCode);
            Assert.True(allPets?.Count() > 0);
        }

        [Fact]
        public async Task Given_a_pet__When_adding_a_new_pet_Then_should_return_Ok()
        {
            //Arrange
            //a pet needs a category, so we need to insert that first
            var petCategory = new PetCategory()
            {
                Name = "Integration test",
                Price = 15.00,
                Quantity = 0
            };

            _petsDbContext.PetCategory.Add(petCategory);
            _petsDbContext.SaveChanges();

            _petsCategoryToBeRemoved.Add(petCategory);

            var petToBePurchased = new PetDto()
            {               
                CategoryId = petCategory.Id,
                Breed = "Integration test",
                Quantity = 1
            };

            var objectData = new Dictionary<string, string>
            {
                {"Breed", petToBePurchased.Breed },
                {"CategoryId", petToBePurchased.CategoryId.ToString() },
                {"Quantity", petToBePurchased.Quantity.ToString() }
            };

            //Act
            var response = await _httpClient.PostAsync("/api/petstore", CreateDtoRequestContent(objectData));
            var result = await response.Content.ReadAsStringAsync();

            //Assert
            int.TryParse(result, out var petId);

            _petsToBeRemoved.Add(new Pet()
            {
                Id = petId,
                CategoryId = petToBePurchased.CategoryId,
                Breed = petToBePurchased.Breed
            });

            Assert.True(response.IsSuccessStatusCode);
            Assert.True(petId > 0);
        }

        public StringContent CreateDtoRequestContent(Dictionary<string, string> objectData)
        {
            var jsonCreateCustomer = JObject.FromObject(objectData);
            var createCustomerRequestContent = new StringContent(jsonCreateCustomer.ToString(), Encoding.UTF8, "application/json");

            return createCustomerRequestContent;
        }

        private void CleanupDatabase()
        {
            _petsDbContext.Pets.RemoveRange(_petsToBeRemoved);
            _petsDbContext.SaveChanges();

            _petsDbContext.PetCategory.RemoveRange(_petsCategoryToBeRemoved);
            _petsDbContext.SaveChanges();
        }
    }
}