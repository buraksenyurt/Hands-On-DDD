using App.Domain.Services;

namespace App.Domain;

public record Money
{
    public decimal Amount { get; }
    public CurrencyCodeInfo CurrencyCodeInfo { get; }
    private Money(decimal amount, CurrencyCodeInfo currencyCodeInfo)
    {
        Amount = amount;
        CurrencyCodeInfo = currencyCodeInfo;
    }
    protected Money(decimal amount, string currencyCode, ICurrencyCodeLookup currencyCodeLookup)
    {
        if (string.IsNullOrEmpty(currencyCode))
        {
            throw new ArgumentNullException(nameof(currencyCode), "Currency code must be specified");
        }
        var currency = currencyCodeLookup.Find(currencyCode);
        if (!currency.InUse)
        {
            throw new ArgumentException($"{currencyCode} is not in use or valid");
        }
        if (decimal.Round(amount, currency.DecimalPlaces) != amount)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), $"Amount in {currencyCode} can not have more thane {currency.DecimalPlaces} decimals");
        }
        Amount = amount;
        CurrencyCodeInfo = currency;
    }
    public static Money FromDecimal(decimal amount, string currencyCode, ICurrencyCodeLookup currencyCodeLookup)
    {
        return new Money(amount, currencyCode, currencyCodeLookup);
    }
    public static Money FromString(string amount, string currencyCode, ICurrencyCodeLookup currencyCodeLookup)
    {
        return new Money(decimal.Parse(amount), currencyCode, currencyCodeLookup);
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
        if (CurrencyCodeInfo != summand.CurrencyCodeInfo)
        {
            throw new MismatchedCurrencyCodeException("Can not add amounts with different currencies");
        }
        return new Money(Amount + summand.Amount, CurrencyCodeInfo);
    }
    public Money Substract(Money subtrahend)
    {
        if (CurrencyCodeInfo != subtrahend.CurrencyCodeInfo)
        {
            throw new MismatchedCurrencyCodeException("Can not subtract amounts with different currencies");
        }
        return new Money(Amount - subtrahend.Amount, CurrencyCodeInfo);
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
