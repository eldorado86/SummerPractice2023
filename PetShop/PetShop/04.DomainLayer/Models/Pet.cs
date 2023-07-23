using StoreApi.Entities;

namespace PetShop.DeveloperTesting.DomainLayer.Models
{
    public class Pet: BaseEntity
    {
        /// <summary>
        /// Eg Bishon, Siamese, Bengal Cat, Parrot, Canar etc
        /// </summary>
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int Quantity { get; set; }
    }
}
