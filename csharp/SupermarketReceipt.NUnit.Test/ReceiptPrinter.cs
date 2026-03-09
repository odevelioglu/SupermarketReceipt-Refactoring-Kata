namespace SupermarketReceipt;

public class ReceiptPrinter
{
    private static readonly CultureInfo Culture = CultureInfo.CreateSpecificCulture("en-GB");

    private int _columns;        

    public string PrintReceipt(Receipt receipt)
    {
        SetColumnSize(receipt);

        var result = new StringBuilder();

        receipt.GetItems().ForEach(item => result.Append(PrintReceiptItem(item)));
        receipt.GetDiscounts().ForEach(discount => result.Append(PrintDiscount(discount)));
                                
        result.AppendLine();
        result.Append(PrintTotal(receipt));
        
        return result.ToString();
    }

    private void SetColumnSize(Receipt receipt)
    {
        var maxNameLength = receipt.GetItems().Any()
            ? receipt.GetItems().Max(item => item.Product.Name.Length)
            : 0;

        _columns = Math.Max(40, maxNameLength + 10);
    }

    private string PrintTotal(Receipt receipt)
    {
        string name = "Total: ";
        string value = PrintPrice(receipt.GetTotalPrice());
        return FormatLineWithWhitespace(name, value);
    }

    private string PrintDiscount(Discount discount)
    {
        string name = $"{discount.Description}({discount.Product.Name})";
        string value = PrintPrice(discount.DiscountAmount);

        return FormatLineWithWhitespace(name, value);
    }

    private string PrintReceiptItem(ReceiptItem item)
    {
        string totalPrice = PrintPrice(item.TotalPrice);
        string name = item.Product.Name;
        string line = FormatLineWithWhitespace(name, totalPrice);
        if (item.Quantity != 1)
        {            
            line += $"  {PrintPrice(item.Price)} * {PrintQuantity(item)}\n";
        }

        return line;
    }
        
    private string FormatLineWithWhitespace(string name, string value)
    {
        var line = new StringBuilder();
        line.Append(name);

        var whitespaceSize = _columns - name.Length - value.Length;

        line.Append(' ', whitespaceSize);        
        line.Append(value);
        line.AppendLine();
        return line.ToString();
    }

    private string PrintPrice(double price) => price.ToString("N2", Culture);

    private static string PrintQuantity(ReceiptItem item) =>
        ProductUnit.Each == item.Product.Unit
            ? ((int)item.Quantity).ToString()
            : item.Quantity.ToString("N3", Culture);            
}