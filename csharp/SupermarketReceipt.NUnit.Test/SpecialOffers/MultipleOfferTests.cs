namespace SupermarketReceipt.NUnit.Test.SpecialOffers;

public class MultipleOfferTests: SpecialOffersTestBase
{
    [TestCase]
    public Task MultipleOffersDifferentProducts()
    {
        _theCart.AddItem(_rice);
        _theCart.AddItemQuantity(_apples, 3);
        _teller.AddSpecialOffer(SpecialOfferType.TenPercentDiscount, _rice, 10.0);
        _teller.AddSpecialOffer(SpecialOfferType.FiveForAmount, _apples, 6.99);
        var receipt = _teller.ChecksOutArticlesFrom(_theCart);
        return Verifier.Verify(new ReceiptPrinter().PrintReceipt(receipt));
    }
}
