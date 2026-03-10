namespace SupermarketReceipt.NUnit.Test.SpecialOffers;

public class SpecialOffersTestBase
{
    public ISupermarketCatalog _catalog;
    public Teller _teller;
    public ShoppingCart _theCart;
    public Product _toothbrush;
    public Product _rice;
    public Product _apples;
    public Product _cherryTomatoes;

    [SetUp]
    public void Setup()
    {
        _catalog = new FakeCatalog();
        _teller = new(_catalog);
        _theCart = new();

        _toothbrush = new Product("toothbrush", ProductUnit.Each);
        _catalog.AddProduct(_toothbrush, 0.99);
        _rice = new Product("rice", ProductUnit.Each);
        _catalog.AddProduct(_rice, 2.99);
        _apples = new Product("apples", ProductUnit.Kilo);
        _catalog.AddProduct(_apples, 1.99);
        _cherryTomatoes = new Product("cherry tomato box", ProductUnit.Each);
        _catalog.AddProduct(_cherryTomatoes, 0.69);
    }
}


