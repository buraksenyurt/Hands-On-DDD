namespace App.Domain.Services;

public interface ICurrencyCodeLookup
{
    CurrencyCodeInfo Find(string code);
}

public class CurrencyCodeInfo
{
    public string Code { get; set; }
    public bool InUse { get; set; }
    public short DecimalPlaces { get; set; }
    public static CurrencyCodeInfo None = new CurrencyCodeInfo
    {
        InUse = false
    };
}
