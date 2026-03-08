
using NUnit.Framework;
using System.Threading.Tasks;
using VerifyNUnit;

namespace SupermarketReceipt.Test
{
    public class ReceiptPrinterTest
    {
        Product _toothbrush;
        Product _apples;
        Receipt _receipt;

        [SetUp]
        public void Setup()
        {
            _toothbrush = new Product("toothbrush", ProductUnit.Each);
            _apples = new Product("apples", ProductUnit.Kilo);
            _receipt = new Receipt();
        }

        [TestCase]
        public Task oneLineItem()
        {
            _receipt.AddProduct(_toothbrush, 1, 0.99, 0.99);
            return Verifier.Verify(new ReceiptPrinter().PrintReceipt(_receipt));
        }

        [TestCase]
        public Task quantityTwo()
        {
            _receipt.AddProduct(_toothbrush, 2, 0.99, 0.99 * 2);
            return Verifier.Verify(new ReceiptPrinter().PrintReceipt(_receipt));
        }

        [TestCase]
        public Task looseWeight()
        {
            _receipt.AddProduct(_apples, 2.3, 1.99, 1.99 * 2.3);
            return Verifier.Verify(new ReceiptPrinter().PrintReceipt(_receipt));
        }

        [TestCase]
        public Task total()
        {

            _receipt.AddProduct(_toothbrush, 1, 0.99, 2 * 0.99);
            _receipt.AddProduct(_apples, 0.75, 1.99, 1.99 * 0.75);
            return Verifier.Verify(new ReceiptPrinter().PrintReceipt(_receipt));
        }

        [TestCase]
        public Task discounts()
        {
            _receipt.AddDiscount(new Discount(_apples, "3 for 2", 0.99));
            return Verifier.Verify(new ReceiptPrinter().PrintReceipt(_receipt));
        }

        [TestCase]
        public Task printWholeReceipt()
        {
            _receipt.AddProduct(_toothbrush, 1, 0.99, 0.99);
            _receipt.AddProduct(_toothbrush, 2, 0.99, 2 * 0.99);
            _receipt.AddProduct(_apples, 0.75, 1.99, 1.99 * 0.75);
            _receipt.AddDiscount(new Discount(_toothbrush, "3 for 2", 0.99));
            return Verifier.Verify(new ReceiptPrinter().PrintReceipt(_receipt));
        }
    }
}