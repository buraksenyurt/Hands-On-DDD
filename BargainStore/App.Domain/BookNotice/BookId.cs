using App.Framework;

namespace App.Domain.BookNotice;

public record BookId : ValueObject<BookId>
{
    private readonly Guid _value;
    public BookId(Guid value)
    {
        if (value == default)
        {
            throw new ArgumentNullException(nameof(value), "Classification Book Id can not be empty");
        }
        _value = value;
    }

    public static implicit operator Guid(BookId self) => self._value;
    public override string ToString() => _value.ToString();
    public static implicit operator BookId(Guid value) => new(value);
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _value;
    }
}
