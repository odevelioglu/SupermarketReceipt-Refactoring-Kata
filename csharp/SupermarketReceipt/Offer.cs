namespace SupermarketReceipt;

public class Offer
{
    private Product _product; //TODO: why never used? 

    public Offer(SpecialOfferType offerType, Product product, double argument)
    {
        OfferType = offerType;
        Argument = argument;
        _product = product;
    }

    public SpecialOfferType OfferType { get; }
    public double Argument { get; }
}