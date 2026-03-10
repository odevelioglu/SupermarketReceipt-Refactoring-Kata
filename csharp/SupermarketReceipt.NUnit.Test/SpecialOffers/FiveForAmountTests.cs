namespace SupermarketReceipt.NUnit.Test.SpecialOffers;

public class FiveForAmountTests: SpecialOffersTestBase
{
    [TestCase]
    public Task FiveForAmountDiscount()
    {
        _theCart.AddItem(_apples, 5);
        _teller.AddSpecialOffer(new ForAmountOffer(_apples, 5, 6.99));
        var receipt = _teller.ChecksOutArticlesFrom(_theCart);
        return Verifier.Verify(new ReceiptPrinter().PrintReceipt(receipt));
    }

    [TestCase]
    public Task FiveForAmountDiscountWithSix()
    {
        _theCart.AddItem(_apples, 6);
        _teller.AddSpecialOffer(new ForAmountOffer(_apples, 5, 6.99));
        var receipt = _teller.ChecksOutArticlesFrom(_theCart);
        return Verifier.Verify(new ReceiptPrinter().PrintReceipt(receipt));
    }

    [TestCase]
    public Task FiveForAmountDiscountWithSixteen()
    {
        _theCart.AddItem(_apples, 16);
        _teller.AddSpecialOffer(new ForAmountOffer(_apples, 5, 6.99));
        var receipt = _teller.ChecksOutArticlesFrom(_theCart);
        return Verifier.Verify(new ReceiptPrinter().PrintReceipt(receipt));
    }

    [TestCase]
    public Task FiveForAmountDiscountWithFour()
    {
        _theCart.AddItem(_apples, 4);        
        _teller.AddSpecialOffer(new ForAmountOffer(_apples, 5, 6.99));
        var receipt = _teller.ChecksOutArticlesFrom(_theCart);
        return Verifier.Verify(new ReceiptPrinter().PrintReceipt(receipt));
    }
}
