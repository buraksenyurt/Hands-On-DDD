using App.Framework;

namespace App.Domain.Infrastructure;

public interface ICurrencyCodeLookup
{
    CurrencyCodeInfo Find(string code);
}

public record CurrencyCodeInfo
   : ValueObject<CurrencyCodeInfo>
{
    public required string Code { get; set; }
    public bool InUse { get; set; }
    public short DecimalPlaces { get; set; }

    public static readonly CurrencyCodeInfo None = new()
    {
        Code = string.Empty,
        InUse = false
    };

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Code;
        yield return InUse;
        yield return DecimalPlaces;
    }
}
