using NUnit.Framework;
using System.Threading.Tasks;
using VerifyNUnit;

namespace SupermarketReceipt.NUnit.Test
{
    public class SupermarketNUnitTest
    {
        private SupermarketCatalog _catalog;
        private Teller _teller;
        private ShoppingCart _theCart;
        private Product _toothbrush;
        private Product _rice;
        private Product _apples;
        private Product _cherryTomatoes;

        [SetUp]
        public void Setup()
        {
            _catalog = new FakeCatalog();
            _teller = new (_catalog);
            _theCart = new ();

            _toothbrush = new Product("toothbrush", ProductUnit.Each);
            _catalog.AddProduct(_toothbrush, 0.99);
            _rice = new Product("rice", ProductUnit.Each);
            _catalog.AddProduct(_rice, 2.99);
            _apples = new Product("apples", ProductUnit.Kilo);
            _catalog.AddProduct(_apples, 1.99);
            _cherryTomatoes = new Product("cherry tomato box", ProductUnit.Each);
            _catalog.AddProduct(_cherryTomatoes, 0.69);
        }

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

        [TestCase]
        public Task an_empty_shopping_cart_should_cost_nothing()
        {
            var receipt = _teller.ChecksOutArticlesFrom(_theCart);
            return Verifier.Verify(new ReceiptPrinter(40).PrintReceipt(receipt));
        }

        [TestCase]
        public Task one_normal_item()
        {
            _theCart.AddItem(_toothbrush);
            var receipt = _teller.ChecksOutArticlesFrom(_theCart);
            return Verifier.Verify(new ReceiptPrinter(40).PrintReceipt(receipt));
        }

        [TestCase]
        public Task two_normal_items()
        {
            _theCart.AddItem(_toothbrush);
            _theCart.AddItem(_rice);
            var receipt = _teller.ChecksOutArticlesFrom(_theCart);
            return Verifier.Verify(new ReceiptPrinter(40).PrintReceipt(receipt));
        }

        [TestCase]
        public Task buy_two_get_one_free()
        {
            _theCart.AddItem(_toothbrush);
            _theCart.AddItem(_toothbrush);
            _theCart.AddItem(_toothbrush);
            _teller.AddSpecialOffer(SpecialOfferType.ThreeForTwo, _toothbrush, _catalog.GetUnitPrice(_toothbrush));
            var receipt = _teller.ChecksOutArticlesFrom(_theCart);
            return Verifier.Verify(new ReceiptPrinter(40).PrintReceipt(receipt));
        }

        [TestCase]
        public Task buy_five_get_one_free()
        {
            _theCart.AddItem(_toothbrush);
            _theCart.AddItem(_toothbrush);
            _theCart.AddItem(_toothbrush);
            _theCart.AddItem(_toothbrush);
            _theCart.AddItem(_toothbrush);
            _teller.AddSpecialOffer(SpecialOfferType.ThreeForTwo, _toothbrush, _catalog.GetUnitPrice(_toothbrush));
            var receipt = _teller.ChecksOutArticlesFrom(_theCart);
            return Verifier.Verify(new ReceiptPrinter(40).PrintReceipt(receipt));
        }

        [TestCase]
        public Task loose_weight_product()
        {
            _theCart.AddItemQuantity(_apples, .5);
            var receipt = _teller.ChecksOutArticlesFrom(_theCart);
            return Verifier.Verify(new ReceiptPrinter(40).PrintReceipt(receipt));
        }

        [TestCase]
        public Task percent_discount()
        {
            _theCart.AddItem(_rice);
            _teller.AddSpecialOffer(SpecialOfferType.TenPercentDiscount, _rice, 10.0);
            var receipt = _teller.ChecksOutArticlesFrom(_theCart);
            return Verifier.Verify(new ReceiptPrinter(40).PrintReceipt(receipt));
        }

        [TestCase]
        public Task xForY_discount()
        {
            _theCart.AddItem(_cherryTomatoes);
            _theCart.AddItem(_cherryTomatoes);
            _teller.AddSpecialOffer(SpecialOfferType.TwoForAmount, _cherryTomatoes, .99);
            var receipt = _teller.ChecksOutArticlesFrom(_theCart);
            return Verifier.Verify(new ReceiptPrinter(40).PrintReceipt(receipt));
        }

        [TestCase]
        public Task FiveForY_discount()
        {
            _theCart.AddItemQuantity(_apples, 5);
            _teller.AddSpecialOffer(SpecialOfferType.FiveForAmount, _apples, 6.99);
            var receipt = _teller.ChecksOutArticlesFrom(_theCart);
            return Verifier.Verify(new ReceiptPrinter(40).PrintReceipt(receipt));
        }

        [TestCase]
        public Task FiveForY_discount_withSix()
        {
            _theCart.AddItemQuantity(_apples, 6);
            _teller.AddSpecialOffer(SpecialOfferType.FiveForAmount, _apples, 6.99);
            var receipt = _teller.ChecksOutArticlesFrom(_theCart);
            return Verifier.Verify(new ReceiptPrinter(40).PrintReceipt(receipt));
        }

        [TestCase]
        public Task FiveForY_discount_withSixteen()
        {
            _theCart.AddItemQuantity(_apples, 16);
            _teller.AddSpecialOffer(SpecialOfferType.FiveForAmount, _apples, 6.99);
            var receipt = _teller.ChecksOutArticlesFrom(_theCart);
            return Verifier.Verify(new ReceiptPrinter(40).PrintReceipt(receipt));
        }

        [TestCase]
        public Task FiveForY_discount_withFour()
        {
            _theCart.AddItemQuantity(_apples, 4);
            _teller.AddSpecialOffer(SpecialOfferType.FiveForAmount, _apples, 6.99);
            var receipt = _teller.ChecksOutArticlesFrom(_theCart);
            return Verifier.Verify(new ReceiptPrinter(40).PrintReceipt(receipt));
        }
    }
}