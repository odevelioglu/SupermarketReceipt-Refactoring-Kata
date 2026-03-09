namespace SupermarketReceipt.Offers;

public class Offer: IOffer
{
    public Offer(SpecialOfferType offerType, Product product, double argument)
    {
        OfferType = offerType;
        Argument = argument;
        Product = product;
    }

    public Product Product { get; }
    public SpecialOfferType OfferType { get; }
    public double Argument { get; }
}
