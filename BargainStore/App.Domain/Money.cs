namespace App.Domain;

public class Money
    : IEquatable<Money>
{
    public decimal Amount { get; }
    public Money(decimal amount)
    {
        Amount = amount;
    }

    public bool Equals(Money? other)
    {
        if (other is null)
        {
            return false;
        }
        if (ReferenceEquals(this, other))
        {
            return true;
        }
        return Amount.Equals(other.Amount);
    }
}
