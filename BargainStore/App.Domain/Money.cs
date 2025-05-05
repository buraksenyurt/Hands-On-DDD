namespace App.Domain;

public record Money(decimal Amount)
{
    public static Money operator +(Money summand1, Money summand2)
    {
        return summand1.Add(summand2);
    }
    public static Money operator -(Money summand1, Money summand2)
    {
        return summand1.Substract(summand2);
    }
    public Money Add(Money summand)
    {
        return new Money(Amount + summand.Amount);
    }
    public Money Substract(Money subtrahend)
    {
        return new Money(Amount - subtrahend.Amount);
    }
    public static Money FromDecimal(decimal amount)
    {
        return new Money(amount);
    }
    public static Money FromString(string amount)
    {
        return new Money(decimal.Parse(amount));
    }
}

// public class Money
//     : IEquatable<Money>
// {
//     public decimal Amount { get; }
//     public Money(decimal amount)
//     {
//         Amount = amount;
//     }

//     public bool Equals(Money? other)
//     {
//         if (other is null)
//         {
//             return false;
//         }
//         if (ReferenceEquals(this, other))
//         {
//             return true;
//         }
//         return Amount.Equals(other.Amount);
//     }
// }
