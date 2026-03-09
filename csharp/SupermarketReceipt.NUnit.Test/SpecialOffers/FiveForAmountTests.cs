namespace SupermarketReceipt.NUnit.Test.SpecialOffers;

public class FiveForAmountTests: SpecialOffersTestBase
{
    [TestCase]
    public Task FiveForAmountDiscount()
    {
        _theCart.AddItemQuantity(_apples, 5);
        _teller.AddSpecialOffer(SpecialOfferType.FiveForAmount, _apples, 6.99);
        var receipt = _teller.ChecksOutArticlesFrom(_theCart);
        return Verifier.Verify(new ReceiptPrinter().PrintReceipt(receipt));
    }

    [TestCase]
    public Task FiveForAmountDiscountWithSix()
    {
        _theCart.AddItemQuantity(_apples, 6);
        _teller.AddSpecialOffer(SpecialOfferType.FiveForAmount, _apples, 6.99);
        var receipt = _teller.ChecksOutArticlesFrom(_theCart);
        return Verifier.Verify(new ReceiptPrinter().PrintReceipt(receipt));
    }

    [TestCase]
    public Task FiveForAmountDiscountWithSixteen()
    {
        _theCart.AddItemQuantity(_apples, 16);
        _teller.AddSpecialOffer(SpecialOfferType.FiveForAmount, _apples, 6.99);
        var receipt = _teller.ChecksOutArticlesFrom(_theCart);
        return Verifier.Verify(new ReceiptPrinter().PrintReceipt(receipt));
    }

    [TestCase]
    public Task FiveForAmountDiscountWithFour()
    {
        _theCart.AddItemQuantity(_apples, 4);
        _teller.AddSpecialOffer(SpecialOfferType.FiveForAmount, _apples, 6.99);
        var receipt = _teller.ChecksOutArticlesFrom(_theCart);
        return Verifier.Verify(new ReceiptPrinter().PrintReceipt(receipt));
    }
}
