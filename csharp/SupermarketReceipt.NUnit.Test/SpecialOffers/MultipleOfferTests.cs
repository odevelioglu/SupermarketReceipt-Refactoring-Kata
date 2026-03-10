namespace SupermarketReceipt.NUnit.Test.SpecialOffers;

public class MultipleOfferTests: SpecialOffersTestBase
{
    [TestCase]
    public Task MultipleOffersDifferentProducts()
    {
        _theCart.AddItem(_rice);
        _theCart.AddItem(_apples, 3);
        _teller.AddSpecialOffer(new TenPercentOffer(_rice));
        _teller.AddSpecialOffer(new ForAmountOffer(_apples, 5, 6.99));
        var receipt = _teller.ChecksOutArticlesFrom(_theCart);
        return Verifier.Verify(new ReceiptPrinter().PrintReceipt(receipt));
    }
}
