namespace SupermarketReceipt.NUnit.Test.SpecialOffers;

public class NoOfferTests: SpecialOffersTestBase
{
    [TestCase]
    public Task AnEmptyShoppingCartShouldCostNothing()
    {
        var receipt = _teller.ChecksOutArticlesFrom(_theCart);
        return Verifier.Verify(new ReceiptPrinter().PrintReceipt(receipt));
    }

    [TestCase]
    public Task OneNormalItem()
    {
        _theCart.AddItem(_toothbrush);
        var receipt = _teller.ChecksOutArticlesFrom(_theCart);
        return Verifier.Verify(new ReceiptPrinter().PrintReceipt(receipt));
    }

    [TestCase]
    public Task TwoNormalItems()
    {
        _theCart.AddItem(_toothbrush);
        _theCart.AddItem(_rice);
        var receipt = _teller.ChecksOutArticlesFrom(_theCart);
        return Verifier.Verify(new ReceiptPrinter().PrintReceipt(receipt));
    }

    [TestCase]
    public Task LooseWeightProduct()
    {
        _theCart.AddItemQuantity(_apples, .5);
        var receipt = _teller.ChecksOutArticlesFrom(_theCart);
        return Verifier.Verify(new ReceiptPrinter().PrintReceipt(receipt));
    }
}
