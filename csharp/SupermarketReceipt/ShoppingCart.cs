namespace SupermarketReceipt;

public class ShoppingCart
{
    public List<ProductQuantity> Items { get; } = new ();
    public Dictionary<Product, double> ProductQuantities = new ();
    
    public void AddItem(Product product, double quantity = 1.0)
    {
        Items.Add(new ProductQuantity(product, quantity));

        if (ProductQuantities.TryGetValue(product, out var currentQuantity))
        {
            ProductQuantities[product] = currentQuantity + quantity;
        }
        else
        {
            ProductQuantities.Add(product, quantity);
        }
    }
}