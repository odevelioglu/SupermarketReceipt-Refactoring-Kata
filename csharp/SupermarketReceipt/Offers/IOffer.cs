namespace SupermarketReceipt.Offers;

public interface IOffer
{
    Product Product { get; }
    SpecialOfferType OfferType { get; }
    Discount? GetDiscount(double quantity, double unitPrice);
}
