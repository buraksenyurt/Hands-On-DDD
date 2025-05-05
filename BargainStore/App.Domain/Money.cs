namespace App.Domain;

public record Money
{
    private const string DefaultCurrencyCode = "TL";
    public decimal Amount { get; }
    public string CurrencyCode { get; }
    protected Money(decimal amount, string currencyCode = "TL")
    {
        Amount = amount;
        CurrencyCode = currencyCode;
    }
    public static Money FromDecimal(decimal amount, string currencyCode = DefaultCurrencyCode)
    {
        return new Money(amount, currencyCode);
    }
    public static Money FromString(string amount, string currencyCode = DefaultCurrencyCode)
    {
        return new Money(decimal.Parse(amount), currencyCode);
    }
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
        if (CurrencyCode != summand.CurrencyCode)
        {
            throw new MismatchedCurrencyCodeException("Can not add amounts with different currencies");
        }
        return new Money(Amount + summand.Amount);
    }
    public Money Substract(Money subtrahend)
    {
        if (CurrencyCode != subtrahend.CurrencyCode)
        {
            throw new MismatchedCurrencyCodeException("Can not subtract amounts with different currencies");
        }
        return new Money(Amount - subtrahend.Amount);
    }
    public class MismatchedCurrencyCodeException(string message)
        : Exception(message)
    {
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
