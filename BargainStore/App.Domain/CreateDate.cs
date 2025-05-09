using App.Framework;

namespace App.Domain;

public record CreateDate(DateTime Value)
    : ValueObject<CreateDate>
{
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
