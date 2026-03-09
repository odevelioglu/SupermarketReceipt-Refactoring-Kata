namespace SupermarketReceipt.Offers;

public class OfferBase
{
    private static readonly CultureInfo Culture = CultureInfo.CreateSpecificCulture("en-GB");

    public OfferBase(SpecialOfferType offerType, Product product)
    {
        OfferType = offerType;        
        Product = product;
    }

    public Product Product { get; }
    public SpecialOfferType OfferType { get; }

    protected string PrintPrice(double price)
    {
        return price.ToString("N2", Culture);
    }
}
