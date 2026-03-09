using SupermarketReceipt.Offers;

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

    public Receipt ChecksOutArticlesFrom(ShoppingCart theCart)
    {
        var receipt = new Receipt();
        
        foreach (var pq in theCart.Items)
        {           
            var unitPrice = _catalog.GetUnitPrice(pq.Product);
            var price = pq.Quantity * unitPrice;
            receipt.AddProduct(pq.Product, pq.Quantity, unitPrice, price);
        }

        var discounts = theCart.GetDiscounts(_offers, _catalog);
        receipt.Discounts.AddRange(discounts);

        return receipt;
    }
}