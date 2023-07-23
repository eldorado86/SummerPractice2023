using StoreApi.Entities;

namespace PetShop.DeveloperTesting.Business.Entities
{
    public class PetDto
    {
        public int Id { get; set; }
        /// <summary>
        /// Eg Bishon, Siamese, Bengal Cat, Parrot, Canar etc
        /// </summary>
        public string Breed { get; set; }
        public int CategoryId { get; set; }
        public int Quantity { get; set; }
    }
}
