namespace App.Domain.BookNotice;

public record BookSummary
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

    public static implicit operator string(BookSummary summary)
    {
        return summary.Value;
    }
}
