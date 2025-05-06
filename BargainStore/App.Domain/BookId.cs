namespace App.Domain;

public record BookId
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
}
