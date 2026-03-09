namespace SupermarketReceipt.Offers;

public class ForAmountOffer: OfferBase, IOffer
{
    public ForAmountOffer(Product product, int productCount, double discountedPrice)
        :base(productCount == 5 ? SpecialOfferType.FiveForAmount : SpecialOfferType.TwoForAmount, product)
    {        
        DiscountedPrice = discountedPrice;
        ProductCount = productCount;
    }

    public int ProductCount { get; }
    public double DiscountedPrice { get; }

    public Discount? GetDiscount(double quantity, double unitPrice)
    {
        var quantityAsInt = (int)quantity;

        if (quantityAsInt < this.ProductCount)
        {
            return null;
        }
         
        var numberOfXs = quantityAsInt / this.ProductCount;
        var discountTotal = unitPrice * quantity - (this.DiscountedPrice * numberOfXs + quantityAsInt % this.ProductCount * unitPrice);
        var description = $"{this.ProductCount} for {PrintPrice(this.DiscountedPrice)}";

        return new Discount(this.Product, description, -discountTotal);
    }
}