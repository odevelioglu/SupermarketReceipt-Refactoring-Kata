namespace SupermarketReceipt;

public class Receipt
{
    public List<Discount> Discounts { get; } = new();
    public List<ReceiptItem> Items { get; } = [];

    public double GetTotalPrice() =>
        Items.Sum(i => i.TotalPrice) + Discounts.Sum(d => d.DiscountAmount);

    public void AddProduct(Product product, double quantity, double price, double totalPrice)
    {
        Items.Add(new ReceiptItem(product, quantity, price, totalPrice));
    }
    
    public void AddDiscount(Discount discount)
    {
        Discounts.Add(discount);
    }
}