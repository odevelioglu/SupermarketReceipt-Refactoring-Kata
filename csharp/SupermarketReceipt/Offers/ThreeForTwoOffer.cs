namespace SupermarketReceipt.Offers;

public class ThreeForTwoOffer: IOffer
{
    public ThreeForTwoOffer(Product product)
    {
        Product = product;
    }

    public Product Product { get; }
    public SpecialOfferType OfferType => SpecialOfferType.ThreeForTwo;
    public double Argument => throw new NotImplementedException();

    public Discount? GetDiscount(double quantity, double unitPrice)
    {
        var quantityAsInt = (int)quantity;

        if (quantityAsInt > 2)
        {
            var numberOfXs = quantityAsInt / 3;
            var discountAmount = quantity * unitPrice - (numberOfXs * 2 * unitPrice + quantityAsInt % 3 * unitPrice);
            return new Discount(Product, "3 for 2", -discountAmount);
        }
        
        return null;
    }
}