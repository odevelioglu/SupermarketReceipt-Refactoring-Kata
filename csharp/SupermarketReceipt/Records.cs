namespace SupermarketReceipt;

public record Discount(Product Product, string Description, double DiscountAmount);

public record Product(string Name, ProductUnit Unit);

public record ProductQuantity(Product Product, double Quantity);

public record ReceiptItem(Product Product, double Quantity, double Price, double TotalPrice);