using System;

namespace SupermarketReceipt;

public class ShoppingCart
{
    public List<ProductQuantity> Items { get; } = new ();
    private readonly Dictionary<Product, double> _productQuantities = new ();
    private static readonly CultureInfo Culture = CultureInfo.CreateSpecificCulture("en-GB");

    public void AddItem(Product product)
    {
        AddItemQuantity(product, 1.0);
    }

    public void AddItemQuantity(Product product, double quantity)
    {
        Items.Add(new ProductQuantity(product, quantity));

        if (_productQuantities.TryGetValue(product, out var currentQuantity))
        {
            _productQuantities[product] = currentQuantity + quantity;
        }
        else
        {
            _productQuantities.Add(product, quantity);
        }
    }
        
    public void HandleOffers(Receipt receipt, Dictionary<Product, Offer> offers, ISupermarketCatalog catalog)
    {
        foreach (var p in _productQuantities.Keys)
        {
            var quantity = _productQuantities[p];
            var quantityAsInt = (int) quantity;
            if (offers.ContainsKey(p))
            {
                var offer = offers[p];
                var unitPrice = catalog.GetUnitPrice(p);
                
                var discount = GetDiscount(p, offer, quantityAsInt, quantity, unitPrice);
                if (discount != null)
                    receipt.AddDiscount(discount);
            }
        }
    }

    private Discount? GetDiscount(Product p, Offer offer, int quantityAsInt, 
        double quantity, double unitPrice)
    {                
        if (offer.OfferType == SpecialOfferType.ThreeForTwo && quantityAsInt > 2)
        {               
            var numberOfXs = quantityAsInt / 3;
            var discountAmount = quantity * unitPrice - (numberOfXs * 2 * unitPrice + quantityAsInt % 3 * unitPrice);
            return new Discount(p, "3 for 2", -discountAmount);            
        }
        else if (offer.OfferType == SpecialOfferType.TwoForAmount && quantityAsInt >= 2)
        {                    
            return GetDiscountForAmount(p, offer, quantityAsInt, quantity, unitPrice, 2);
        }
        else if (offer.OfferType == SpecialOfferType.FiveForAmount && quantityAsInt >= 5)
        {            
            return GetDiscountForAmount(p, offer, quantityAsInt, quantity, unitPrice, 5);            
        }
        else if (offer.OfferType == SpecialOfferType.TenPercentDiscount)
        {            
            return new Discount(p, $"{offer.Argument}% off", -quantity * unitPrice * offer.Argument / 100.0);
        }

        return null;
    }

    private Discount GetDiscountForAmount(Product p, Offer offer, int quantityAsInt,
        double quantity, double unitPrice, int x) 
    {        
        var numberOfXs = quantityAsInt / x;
        var discountTotal = unitPrice * quantity - (offer.Argument * numberOfXs + quantityAsInt % x * unitPrice);
        var description = $"{x} for {PrintPrice(offer.Argument)}";
        
        return new Discount(p, description, -discountTotal);
    }
    
    private string PrintPrice(double price)
    {
        return price.ToString("N2", Culture);
    }
}