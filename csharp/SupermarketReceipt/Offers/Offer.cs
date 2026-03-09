namespace SupermarketReceipt.Offers;

public class OfferBase
{
    public OfferBase(SpecialOfferType offerType, Product product)
    {
        OfferType = offerType;        
        Product = product;
    }

    public Product Product { get; }
    public SpecialOfferType OfferType { get; }    
}
