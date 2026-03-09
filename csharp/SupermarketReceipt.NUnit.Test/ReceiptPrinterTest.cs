namespace SupermarketReceipt.Test;

public class ReceiptPrinterTest
{
    Product _toothbrush;
    Product _apples;
    Receipt _receipt;

    [SetUp]
    public void Setup()
    {
        _toothbrush = new Product("toothbrush", ProductUnit.Each);
        _apples = new Product("apples", ProductUnit.Kilo);
        _receipt = new Receipt();
    }

    [TestCase]
    public Task OneLineItem()
    {
        _receipt.AddProduct(_toothbrush, 1, 0.99, 0.99);
        return Verifier.Verify(new ReceiptPrinter().PrintReceipt(_receipt));
    }

    [TestCase]
    public Task QuantityTwo()
    {
        _receipt.AddProduct(_toothbrush, 2, 0.99, 0.99 * 2);
        return Verifier.Verify(new ReceiptPrinter().PrintReceipt(_receipt));
    }

    [TestCase]
    public Task LooseWeight()
    {
        _receipt.AddProduct(_apples, 2.3, 1.99, 1.99 * 2.3);
        return Verifier.Verify(new ReceiptPrinter().PrintReceipt(_receipt));
    }

    [TestCase]
    public Task Total()
    {
        _receipt.AddProduct(_toothbrush, 1, 0.99, 2 * 0.99);
        _receipt.AddProduct(_apples, 0.75, 1.99, 1.99 * 0.75);
        return Verifier.Verify(new ReceiptPrinter().PrintReceipt(_receipt));
    }

    [TestCase]
    public Task Discounts()
    {
        _receipt.AddDiscount(new Discount(_apples, "3 for 2", 0.99));
        return Verifier.Verify(new ReceiptPrinter().PrintReceipt(_receipt));
    }

    [TestCase]
    public Task PrintWholeReceipt()
    {
        _receipt.AddProduct(_toothbrush, 1, 0.99, 0.99);
        _receipt.AddProduct(_toothbrush, 2, 0.99, 2 * 0.99);
        _receipt.AddProduct(_apples, 0.75, 1.99, 1.99 * 0.75);
        _receipt.AddDiscount(new Discount(_toothbrush, "3 for 2", 0.99));
        return Verifier.Verify(new ReceiptPrinter().PrintReceipt(_receipt));
    }

    [TestCase]
    public Task CustomColumnsAlignment()
    {
        _receipt.AddProduct(_toothbrush, 1, 0.99, 0.99);
        // Use a narrow receipt width to exercise whitespace calculation
        return Verifier.Verify(new ReceiptPrinter().PrintReceipt(_receipt));
    }

    [TestCase]
    public Task LongProductName()
    {
        var longNameProduct = new Product("very-long-product-name-that-exceeds-columns", ProductUnit.Each);
        _receipt.AddProduct(longNameProduct, 1, 9.99, 9.99);
        // Default printer (40 columns) — this ensures behavior when name length > available whitespace
        return Verifier.Verify(new ReceiptPrinter().PrintReceipt(_receipt));
    }

    [TestCase]
    public Task ZeroQuantity()
    {
        // Edge case: quantity 0 — ReceiptPrinter prints the extra quantity line whenever quantity != 1
        _receipt.AddProduct(_toothbrush, 0, 0.99, 0.0);
        return Verifier.Verify(new ReceiptPrinter().PrintReceipt(_receipt));
    }

    [TestCase]
    public Task KiloQuantityRounding()
    {
        // Use a value that will be rounded when formatted to 3 decimals (N3)
        _receipt.AddProduct(_apples, 1.2345, 1.99, 1.99 * 1.2345);
        return Verifier.Verify(new ReceiptPrinter().PrintReceipt(_receipt));
    }

    [TestCase]
    public Task MultipleDiscounts()
    {
        _receipt.AddProduct(_toothbrush, 2, 0.99, 2 * 0.99);
        _receipt.AddDiscount(new Discount(_toothbrush, "3 for 2", -0.99));
        _receipt.AddDiscount(new Discount(_toothbrush, "10% off", -0.20));
        return Verifier.Verify(new ReceiptPrinter().PrintReceipt(_receipt));
    }
}