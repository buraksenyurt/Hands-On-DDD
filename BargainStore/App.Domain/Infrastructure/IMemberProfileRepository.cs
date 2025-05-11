using App.Domain.MemberProfile;
using App.Domain.Shared;

namespace App.Domain.Infrastructure;

public interface IMemberProfileRepository
{
    Task<Member> LoadAsync(MemberId id);
    Task CreateAsync(Member member);
    Task<bool> IsExistAsync(MemberId id);
}
