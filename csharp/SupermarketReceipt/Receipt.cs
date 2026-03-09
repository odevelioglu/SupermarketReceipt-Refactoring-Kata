namespace SupermarketReceipt;

public class Receipt
{
    public List<Discount> Discounts { get; } = new();
    public List<ReceiptItem> Items { get; } = [];

    public double GetTotalPrice()
    {
        var total = 0.0;
        foreach (var item in Items) total += item.TotalPrice;
        foreach (var discount in Discounts) total += discount.DiscountAmount;
        return total;
    }

    public void AddProduct(Product p, double quantity, double price, double totalPrice)
    {
        Items.Add(new ReceiptItem(p, quantity, price, totalPrice));
    }
    
    public void AddDiscount(Discount discount)
    {
        Discounts.Add(discount);
    }
}