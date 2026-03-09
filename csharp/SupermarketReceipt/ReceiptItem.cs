namespace SupermarketReceipt;

public class ReceiptItem
{
    public ReceiptItem(Product product, double quantity, double price, double totalPrice)
    {
        Product = product;
        Quantity = quantity;
        Price = price;
        TotalPrice = totalPrice;
    }

    public Product Product { get; }
    public double Price { get; }
    public double TotalPrice { get; }
    public double Quantity { get; }
}