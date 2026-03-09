using SupermarketReceipt.Offers;

namespace SupermarketReceipt;

public class ShoppingCart
{
    public List<ProductQuantity> Items { get; } = new ();
    private readonly Dictionary<Product, double> _productQuantities = new ();
    
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
        
    public void HandleOffers(Receipt receipt, Dictionary<Product, IOffer> offers, ISupermarketCatalog catalog)
    {
        foreach (var kvp in _productQuantities)
        {
            var product = kvp.Key;
                        
            if (offers.TryGetValue(product, out var offer))
            {                
                var unitPrice = catalog.GetUnitPrice(product);
                var quantity = kvp.Value;
                var quantityAsInt = (int)quantity;

                var discount = GetDiscount(product, offer, quantityAsInt, quantity, unitPrice);
                if (discount != null)
                    receipt.AddDiscount(discount);
            }
        }
    }

    private Discount? GetDiscount(Product p, IOffer offer, int quantityAsInt, 
        double quantity, double unitPrice)
    {                
        if (offer is ThreeForTwoOffer threeForTwoOffer)
        {               
             return threeForTwoOffer.GetDiscount(quantity, unitPrice);
        }
        else if (offer is ForAmountOffer forAmountOffer)
        {                    
            return forAmountOffer.GetDiscount(quantity, unitPrice);
        }        
        else if (offer is TenPercentOffer tenPercentOffer)
        {            
            return tenPercentOffer.GetDiscount(quantity, unitPrice);
        }

        return null;
    }    
}