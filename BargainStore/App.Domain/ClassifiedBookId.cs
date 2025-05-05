namespace App.Domain;

public record ClassifiedBookId
{
    private readonly Guid _value;
    public ClassifiedBookId(Guid value)
    {
        if (value == default)
        {
            throw new ArgumentNullException(nameof(value), "Classification Book Id can not be empty");
        }
        _value = value;
    }
}
