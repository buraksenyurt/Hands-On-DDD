using App.Domain.Services;

namespace App.Domain;

public record ListPrice
    : Money
{
    public ListPrice(decimal amount, string currencyCode, ICurrencyCodeLookup currencyCodeLookup) : base(amount, currencyCode, currencyCodeLookup)
    {
        if (amount < 0)
        {
            throw new ArgumentException("List price can not be negative", nameof(amount));
        }
    }
}
