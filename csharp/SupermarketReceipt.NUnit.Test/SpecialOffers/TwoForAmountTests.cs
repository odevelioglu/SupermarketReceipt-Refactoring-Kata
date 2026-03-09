namespace SupermarketReceipt.NUnit.Test.SpecialOffers;

public class TwoForAmountTests: SpecialOffersTestBase
{
    [TestCase]
    public Task TwoForAmountDiscount()
    {
        _theCart.AddItem(_cherryTomatoes);
        _theCart.AddItem(_cherryTomatoes);
        _teller.AddSpecialOffer(SpecialOfferType.TwoForAmount, _cherryTomatoes, .99);
        var receipt = _teller.ChecksOutArticlesFrom(_theCart);
        return Verifier.Verify(new ReceiptPrinter().PrintReceipt(receipt));
    }

    [TestCase]
    public Task TwoForAmountDiscountWithThree()
    {
        _theCart.AddItem(_cherryTomatoes);
        _theCart.AddItem(_cherryTomatoes);
        _theCart.AddItem(_cherryTomatoes);
        _teller.AddSpecialOffer(SpecialOfferType.TwoForAmount, _cherryTomatoes, .99);
        var receipt = _teller.ChecksOutArticlesFrom(_theCart);
        return Verifier.Verify(new ReceiptPrinter().PrintReceipt(receipt));
    }

    [TestCase]
    public Task TwoForAmountDiscountWithFour()
    {
        _theCart.AddItem(_cherryTomatoes);
        _theCart.AddItem(_cherryTomatoes);
        _theCart.AddItem(_cherryTomatoes);
        _theCart.AddItem(_cherryTomatoes);
        _teller.AddSpecialOffer(SpecialOfferType.TwoForAmount, _cherryTomatoes, .99);
        var receipt = _teller.ChecksOutArticlesFrom(_theCart);
        return Verifier.Verify(new ReceiptPrinter().PrintReceipt(receipt));
    }
}
