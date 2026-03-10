namespace SupermarketReceipt.NUnit.Test.SpecialOffers;

public class TenPercentDiscountTests: SpecialOffersTestBase
{
    [TestCase]
    public void TenPercentDiscount()
    {
        // ARRANGE
        ISupermarketCatalog catalog = new FakeCatalog();
        var toothbrush = new Product("toothbrush", ProductUnit.Each);
        catalog.AddProduct(toothbrush, 0.99);
        var apples = new Product("apples", ProductUnit.Kilo);
        catalog.AddProduct(apples, 1.99);

        var cart = new ShoppingCart();
        cart.AddItem(apples, 2.5);

        var teller = new Teller(catalog);
        teller.AddSpecialOffer(new TenPercentOffer(toothbrush)); //not in the cart

        // ACT
        var receipt = teller.ChecksOutArticlesFrom(cart);

        // ASSERT
        Assert.That(receipt.GetTotalPrice().Equals(4.975));
        Assert.That(receipt.Discounts, Is.Empty);
        Assert.That(receipt.Items.Count.Equals(1));
        var receiptItem = receipt.Items[0];
        Assert.That(receiptItem.Product.Equals(apples));
        Assert.That(receiptItem.Price.Equals(1.99));
        Assert.That(receiptItem.TotalPrice.Equals(2.5 * 1.99));
        Assert.That(receiptItem.Quantity.Equals(2.5));
    }

    [TestCase]
    public Task PercentDiscount()
    {
        _theCart.AddItem(_rice);
        _teller.AddSpecialOffer(new TenPercentOffer(_rice));
        var receipt = _teller.ChecksOutArticlesFrom(_theCart);
        return Verifier.Verify(new ReceiptPrinter().PrintReceipt(receipt));
    }

    [TestCase]
    public Task PercentDiscountWithTwoProducts()
    {
        _theCart.AddItem(_rice);
        _teller.AddSpecialOffer(new TenPercentOffer(_rice));

        _theCart.AddItem(_apples);
        _teller.AddSpecialOffer(new TenPercentOffer(_apples));
        var receipt = _teller.ChecksOutArticlesFrom(_theCart);
        return Verifier.Verify(new ReceiptPrinter().PrintReceipt(receipt));
    }

    [TestCase]
    public Task TenPercentDiscountOnKiloProduct()
    {
        _theCart.AddItem(_apples, 2.5);
        _teller.AddSpecialOffer(new TenPercentOffer(_apples));
        var receipt = _teller.ChecksOutArticlesFrom(_theCart);

        return Verifier.Verify(new ReceiptPrinter().PrintReceipt(receipt));
    }
}
