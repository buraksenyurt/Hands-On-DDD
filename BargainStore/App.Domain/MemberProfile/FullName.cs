using App.Domain.Shared;
using App.Framework;

namespace App.Domain.MemberProfile;

public record FullName : ValueObject<FullName>
{
    public string Value { get; }
    protected FullName() { }
    internal FullName(string fullName)
    {
        Value = fullName;
    }
    public static FullName FromString(string fullName, TextValidator hasIllegal)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Full name cannot be empty.", nameof(fullName));

        if (hasIllegal(fullName))
            throw new DomainExceptions.IllegalWordsFoundException(fullName);

        return new FullName(fullName);
    }
    public static implicit operator string(FullName fullName) => fullName.Value;
    public static implicit operator FullName(string fullName) => new(fullName);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
