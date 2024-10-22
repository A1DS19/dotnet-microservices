namespace Catalog.API.Exceptions;

public class ProductNotFoundException : Exception
{
    public ProductNotFoundException(string identifier = "")
        : base($"Product {identifier} not found") { }
}
