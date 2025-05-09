using App.Framework;

namespace App.Domain.BookNotice;
public record BookTitle
    : ValueObject<BookTitle>
{
    public string Value { get; }
    internal BookTitle(string value)
    {
        Value = value;
    }
    public static BookTitle FromString(string title)
    {
        Validate(title);
        return new BookTitle(title);
    }

    private static void Validate(string value)
    {
        if (value.Length > 100)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Book title can not be longer that 100 characters");
        }
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(BookTitle self) => self.Value;
}
