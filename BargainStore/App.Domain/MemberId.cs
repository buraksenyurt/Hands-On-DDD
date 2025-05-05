namespace App.Domain;

//Value Object
public class MemberId
{
    private readonly Guid _value;

    public MemberId(Guid value)
    {
        if (value == default)
        {
            throw new ArgumentNullException(nameof(value), "Member Id can not be empty");
        }
        _value = value;
    }
}
