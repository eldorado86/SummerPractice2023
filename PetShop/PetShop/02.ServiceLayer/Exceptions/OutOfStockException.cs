namespace PetShop.DeveloperTesting._02.ServiceLayer.Exceptions
{
    public class OutOfStockException: Exception
    {
        public OutOfStockException(string message) : base(message) { }

    }
}
