namespace SupermarketReceipt.Offers;

public interface IOffer
{
    Product Product { get; }
    SpecialOfferType OfferType { get; }
    double Argument { get; }
}
