using App.Framework;

namespace App.Domain.Shared;

public record CreateDate
    : ValueObject<CreateDate>
{
    public DateTime Value { get; }
    public CreateDate(DateTime date)
    {
        if (date == default)
            throw new ArgumentException("Date cannot be default value.", nameof(date));

        Value = date;
    }
    public static implicit operator DateTime(CreateDate date) => date.Value;
    public static implicit operator CreateDate(DateTime date) => new(date);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    internal static CreateDate From(DateTime createDate)
    {
        if (createDate == default)
            throw new ArgumentException("Date cannot be default value.", nameof(createDate));

        return new CreateDate(createDate);
    }
}
