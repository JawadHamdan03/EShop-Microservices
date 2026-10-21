namespace Catalog.API.Exceptions;

public class ProductNotFoundException : Exception
{
    public ProductNotFoundException(): base("Product was not found!")
    {
        
    }

    public ProductNotFoundException(Guid Id) : base($"Product with this {Id} was not found")
    {
        
    }
    
}