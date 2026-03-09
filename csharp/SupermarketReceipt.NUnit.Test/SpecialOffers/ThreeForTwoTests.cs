namespace SupermarketReceipt.NUnit.Test.SpecialOffers;

public class ThreeForTwoTests: SpecialOffersTestBase
{
    [TestCase]
    public Task BuyTwoGetOneFree()
    {
        _theCart.AddItem(_toothbrush);
        _theCart.AddItem(_toothbrush);
        _theCart.AddItem(_toothbrush);
        _teller.AddSpecialOffer(SpecialOfferType.ThreeForTwo, _toothbrush, _catalog.GetUnitPrice(_toothbrush));
        var receipt = _teller.ChecksOutArticlesFrom(_theCart);
        return Verifier.Verify(new ReceiptPrinter().PrintReceipt(receipt));
    }

    [TestCase]
    public Task BuyFiveGetOneFree()
    {
        _theCart.AddItem(_toothbrush);
        _theCart.AddItem(_toothbrush);
        _theCart.AddItem(_toothbrush);
        _theCart.AddItem(_toothbrush);
        _theCart.AddItem(_toothbrush);
        _teller.AddSpecialOffer(SpecialOfferType.ThreeForTwo, _toothbrush, _catalog.GetUnitPrice(_toothbrush));
        var receipt = _teller.ChecksOutArticlesFrom(_theCart);
        return Verifier.Verify(new ReceiptPrinter().PrintReceipt(receipt));
    }

    [TestCase]
    public Task BuySixGetTwoFree()
    {
        _theCart.AddItem(_toothbrush);
        _theCart.AddItem(_toothbrush);
        _theCart.AddItem(_toothbrush);
        _theCart.AddItem(_toothbrush);
        _theCart.AddItem(_toothbrush);
        _theCart.AddItem(_toothbrush);
        _teller.AddSpecialOffer(SpecialOfferType.ThreeForTwo, _toothbrush, _catalog.GetUnitPrice(_toothbrush));
        var receipt = _teller.ChecksOutArticlesFrom(_theCart);
        return Verifier.Verify(new ReceiptPrinter().PrintReceipt(receipt));
    }
}
