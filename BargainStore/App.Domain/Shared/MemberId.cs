using App.Framework;

namespace App.Domain.Shared;

public record MemberId : ValueObject<MemberId>
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
    public static implicit operator Guid(MemberId self) => self._value;
    public override string ToString() => _value.ToString();

    public static implicit operator MemberId(Guid value) => new(value);
    public static implicit operator MemberId(string value) => new(Guid.Parse(value));

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _value;
    }
}
