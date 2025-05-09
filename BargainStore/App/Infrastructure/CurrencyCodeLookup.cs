using App.Domain.Infrastructure;

namespace App.Infrastructure;

public class CurrencyCodeLookup
    : ICurrencyCodeLookup
{
    private static IReadOnlyList<CurrencyCodeInfo> _currencyCodes = [
           new CurrencyCodeInfo{
            Code="TL",
            DecimalPlaces=2,
            InUse=true
        },
        new CurrencyCodeInfo{
            Code="USD",
            DecimalPlaces=2,
            InUse=true
        },
        new CurrencyCodeInfo{
            Code="JPY",
            DecimalPlaces=0,
            InUse=true
        }
       ];
    public CurrencyCodeInfo Find(string code)
    {
        var currency = _currencyCodes.FirstOrDefault(c => c.Code == code);
        return currency ?? CurrencyCodeInfo.None;
    }
}
