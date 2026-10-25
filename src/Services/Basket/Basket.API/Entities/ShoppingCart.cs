namespace Basket.API.Entities;

public class ShoppingCart
{
    public string UserName { get; set; }
    public List<ShoppingCartItem> Items { get; set; }
    public decimal TotalPrice => Items.Sum(x=>x.Price*x.Quantity);
    
    public ShoppingCart()
    {
        
    }

    public ShoppingCart(string userName)
    {
        this.UserName = userName;
    }
}