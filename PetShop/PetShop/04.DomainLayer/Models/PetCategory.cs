namespace PetShop.DeveloperTesting.DomainLayer.Models
{ 
    /// <summary>
    /// shoudl call Breed ?
    /// Eg: Cat, Dog, Turtle, Bird etc 
    /// </summary>
    public class PetCategory:BaseEntity
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
