namespace Catalog.API.Exceptions;

public class ProductNotFoundExceptions : Exception
{
    public ProductNotFoundExceptions(string identifier = "")
        : base($"Product {identifier} not found") { }
}
