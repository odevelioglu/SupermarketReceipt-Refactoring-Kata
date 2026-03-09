namespace SupermarketReceipt;

public class Product: IEquatable<Product>
{
    public Product(string name, ProductUnit unit)
    {
        Name = name;
        Unit = unit;
    }

    public string Name { get; }
    public ProductUnit Unit { get; }

    #region Equality Members
    public bool Equals(Product? other)
    {
        if (ReferenceEquals(other, null)) return false;
        if (ReferenceEquals(this, other)) return true;
        if (GetType() != other.GetType()) return false;

        return string.Equals(Name, other.Name, StringComparison.InvariantCulture)
               && Unit == other.Unit;
    }

    public override bool Equals(object? obj) => Equals(obj as Product);

    public override int GetHashCode() => HashCode.Combine(Name, Unit);
    #endregion
}