namespace StoreApi.Entities
{
    /// <summary>
    /// shoudl call Breed ?
    /// Eg: Cat, Dog, Turtle, Bird etc 
    /// </summary>
    public class PetCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
