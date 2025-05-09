using App.Framework;

namespace App.Domain.BookNotice;

public record BookSummary
    : ValueObject<BookSummary>
{
    public string Value { get; }

    internal BookSummary(string value)
    {
        Value = value;
    }

    public static BookSummary FromString(string value)
    {
        return new BookSummary(value);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(BookSummary summary)
    {
        return summary.Value;
    }
}
