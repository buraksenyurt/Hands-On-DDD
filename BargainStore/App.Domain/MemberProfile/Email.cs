using App.Domain.Shared;
using App.Framework;

namespace App.Domain.MemberProfile;

public record Email : ValueObject<Email>
{
    public string Value { get; }
    protected Email() { }
    internal Email(string email)
    {
        Value = email;
    }
    public static Email FromString(string email, TextValidator hasIllegal)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty.", nameof(email));

        if (hasIllegal(email))
            throw new DomainExceptions.InvalidEmailException(email);
        return new Email(email);
    }

    public static implicit operator string(Email email) => email.Value;
    public static implicit operator Email(string email) => new(email);
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
