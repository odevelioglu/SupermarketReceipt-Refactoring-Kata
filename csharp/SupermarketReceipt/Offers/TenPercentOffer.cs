namespace SupermarketReceipt.Offers;

public class TenPercentOffer: IOffer
{
    public TenPercentOffer(Product product)
    {
        Product = product;        
    }
    
    public Product Product { get; }
    public SpecialOfferType OfferType => SpecialOfferType.TenPercentDiscount;
    public double Argument => throw new NotImplementedException();

    public Discount? GetDiscount(double quantity, double unitPrice)
    {
        const int percent = 10;
        return new Discount(this.Product, $"{percent}% off", -quantity * unitPrice * percent / 100.0);
    }
}