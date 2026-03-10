namespace SupermarketReceipt;

public class Teller
{
    private readonly ISupermarketCatalog _catalog;
    private readonly Dictionary<Product, IOffer> _offers = new ();

    public Teller(ISupermarketCatalog catalog)
    {
        _catalog = catalog;
    }
    
    public void AddSpecialOffer(IOffer offer)
    {
        _offers[offer.Product] = offer;
    }

    public Receipt ChecksOutArticlesFrom(ShoppingCart cart)
    {
        var receipt = new Receipt();
        
        foreach (var item in cart.Items)
        {           
            var unitPrice = _catalog.GetUnitPrice(item.Product);
            var price = item.Quantity * unitPrice;
            receipt.AddProduct(item.Product, item.Quantity, unitPrice, price);
        }

        var discounts = GetDiscounts(cart);
        receipt.Discounts.AddRange(discounts);

        return receipt;
    }

    private IEnumerable<Discount> GetDiscounts(ShoppingCart cart)
    {
        foreach (var kvp in cart.ProductQuantities)
        {
            var product = kvp.Key;
            var quantity = kvp.Value;

            if (_offers.TryGetValue(product, out var offer))
            {
                var unitPrice = _catalog.GetUnitPrice(product);

                var discount = offer.GetDiscount(quantity, unitPrice);
                if (discount != null)
                    yield return discount;
            }
        }
    }
}