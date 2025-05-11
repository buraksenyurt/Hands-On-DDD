using App.Domain.Infrastructure;
using App.Domain.Shared;

namespace App.Domain.BookNotice;

public record SalesPrice
    : Money
{
    public SalesPrice(decimal amount, string currencyCode, ICurrencyCodeLookup currencyCodeLookup)
        : base(amount, currencyCode, currencyCodeLookup)
    {
        if (amount < 0)
        {
            throw new ArgumentException("List price can not be negative", nameof(amount));
        }
    }
    internal SalesPrice(decimal amount, string currencyCode)
        : base(amount, new CurrencyCodeInfo { Code = currencyCode })
    {
    }

    public new static SalesPrice FromDecimal(decimal amount, string currency,
        ICurrencyCodeLookup currencyCodeLookup)
    {
        return new SalesPrice(amount, currency, currencyCodeLookup);
    }

    public static implicit operator decimal(SalesPrice self) => self.Amount;
}
