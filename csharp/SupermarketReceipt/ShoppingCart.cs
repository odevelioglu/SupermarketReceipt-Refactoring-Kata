using SupermarketReceipt.Offers;

namespace SupermarketReceipt;

public class ShoppingCart
{
    public List<ProductQuantity> Items { get; } = new ();
    private readonly Dictionary<Product, double> _productQuantities = new ();
    
    public void AddItem(Product product)
    {
        AddItemQuantity(product, 1.0);
    }

    public void AddItemQuantity(Product product, double quantity)
    {
        Items.Add(new ProductQuantity(product, quantity));

        if (_productQuantities.TryGetValue(product, out var currentQuantity))
        {
            _productQuantities[product] = currentQuantity + quantity;
        }
        else
        {
            _productQuantities.Add(product, quantity);
        }
    }
        
    public IEnumerable<Discount> GetDiscounts(Dictionary<Product, IOffer> offers, ISupermarketCatalog catalog)
    {
        foreach (var kvp in _productQuantities)
        {
            var product = kvp.Key;
            var quantity = kvp.Value;

            if (offers.TryGetValue(product, out var offer))
            {                
                var unitPrice = catalog.GetUnitPrice(product);
                                
                var discount = offer.GetDiscount(quantity, unitPrice);
                if (discount != null)
                    yield return discount;
            }
        }
    }
}