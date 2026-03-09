namespace SupermarketReceipt;

public class Teller
{
    private readonly ISupermarketCatalog _catalog;
    private readonly Dictionary<Product, Offer> _offers = new ();

    public Teller(ISupermarketCatalog catalog)
    {
        _catalog = catalog;
    }

    public void AddSpecialOffer(SpecialOfferType offerType, Product product, double argument)
    {
        _offers[product] = new Offer(offerType, product, argument);
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

        theCart.HandleOffers(receipt, _offers, _catalog);

        return receipt;
    }
}