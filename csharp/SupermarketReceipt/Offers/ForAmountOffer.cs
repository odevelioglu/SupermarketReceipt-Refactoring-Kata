namespace SupermarketReceipt.Offers;

public class ForAmountOffer: IOffer
{
    private static readonly CultureInfo Culture = CultureInfo.CreateSpecificCulture("en-GB");

    public ForAmountOffer(Product product, int productCount, double discountedPrice)
    {
        Product = product;
        DiscountedPrice = discountedPrice;
        ProductCount = productCount;
    }

    public int ProductCount { get; }
    public double DiscountedPrice { get; }
    public Product Product { get; }
    public SpecialOfferType OfferType => ProductCount == 5 ? SpecialOfferType.FiveForAmount : SpecialOfferType.TwoForAmount;
    public double Argument => throw new NotImplementedException();

    public Discount? GetDiscount(double quantity, double unitPrice)
    {
        var quantityAsInt = (int)quantity;

        if (quantityAsInt >= this.ProductCount)
        { 
            var numberOfXs = quantityAsInt / this.ProductCount;
            var discountTotal = unitPrice * quantity - (this.DiscountedPrice * numberOfXs + quantityAsInt % this.ProductCount * unitPrice);
            var description = $"{this.ProductCount} for {PrintPrice(this.DiscountedPrice)}";

            return new Discount(this.Product, description, -discountTotal);
        }

        return null;
    }

    private string PrintPrice(double price)
    {
        return price.ToString("N2", Culture);
    }
}