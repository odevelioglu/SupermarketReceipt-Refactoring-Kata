namespace SupermarketReceipt.Offers;

public class TenPercentOffer: OfferBase, IOffer
{
    public TenPercentOffer(Product product):base(SpecialOfferType.TenPercentDiscount, product)
    {
               
    }
        
    public Discount? GetDiscount(double quantity, double unitPrice)
    {
        const int percent = 10;
        return new Discount(this.Product, $"{percent}% off", -quantity * unitPrice * percent / 100.0);
    }
}