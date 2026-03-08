using NUnit.Framework;

namespace SupermarketReceipt.NUnit.Test
{
    public class SupermarketTests
    {
        [TestCase]
        public void TenPercentDiscount()
        {
            // ARRANGE
            SupermarketCatalog catalog = new FakeCatalog();
            var toothbrush = new Product("toothbrush", ProductUnit.Each);
            catalog.AddProduct(toothbrush, 0.99);
            var apples = new Product("apples", ProductUnit.Kilo);
            catalog.AddProduct(apples, 1.99);

            var cart = new ShoppingCart();
            cart.AddItemQuantity(apples, 2.5);

            var teller = new Teller(catalog);
            teller.AddSpecialOffer(SpecialOfferType.TenPercentDiscount, toothbrush, 10.0);

            // ACT
            var receipt = teller.ChecksOutArticlesFrom(cart);

            // ASSERT
            Assert.That(receipt.GetTotalPrice().Equals(4.975));
            Assert.That(receipt.GetDiscounts(), Is.Empty);
            Assert.That(receipt.GetItems().Count.Equals(1));
            var receiptItem = receipt.GetItems()[0];
            Assert.That(receiptItem.Product.Equals(apples));
            Assert.That(receiptItem.Price.Equals(1.99));
            Assert.That(receiptItem.TotalPrice.Equals(2.5 * 1.99));
            Assert.That(receiptItem.Quantity.Equals(2.5));
        }
    }
}