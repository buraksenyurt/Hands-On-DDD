using App.Domain.Shared;
using App.Framework;

namespace App.Domain.MemberProfile;

public class Member
    : AggregateRoot<MemberId>
{
    public Email Email { get; private set; }
    public FullName FullName { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    protected Member() { }
    public Member(MemberId id, Email email, FullName fullName)
    {
        Raise(new Events.Created
        {
            Id = id,
            Email = email,
            FullName = fullName,
            CreatedAt = DateTime.UtcNow
        });
    }
    public void UpdateEmail(Email email)
    {
        Raise(new Events.EmailUpdated
        {
            Id = Id,
            Email = email,
        });
    }
    public void UpdateFullName(FullName fullName)
    {
        Raise(new Events.FullNameUpdated
        {
            Id = Id,
            FullName = fullName,
        });
    }

    protected override void When(object @event)
    {
        switch (@event)
        {
            case Events.Created e:
                Id = e.Id;
                Email = e.Email;
                FullName = e.FullName;
                CreatedAt = e.CreatedAt;
                break;
            case Events.EmailUpdated e:
                Email = e.Email;
                UpdatedAt = DateTime.UtcNow;
                break;
            case Events.FullNameUpdated e:
                FullName = e.FullName;
                UpdatedAt = DateTime.UtcNow;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(@event), @event, null);
        }
    }

    protected override void ValidateSate()
    {
    }
}
