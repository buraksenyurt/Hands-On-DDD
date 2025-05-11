using App.Domain.Infrastructure;
using App.Domain.MemberProfile;
using App.Domain.Shared;
using App.Infrastructure;

namespace App.MemberProfile;

public class MemberProfileRepository(MembershipDbContext dbContext)
    : IMemberProfileRepository, IDisposable
{
    private readonly MembershipDbContext _dbContext = dbContext;

    public async Task CreateAsync(Member member)
    {
        await _dbContext.Members.AddAsync(member);
    }

    public async Task<bool> IsExistAsync(MemberId id)
    {
        return await _dbContext.Members.FindAsync(id) != null;
    }

    public async Task<Member> LoadAsync(MemberId id)
    {
        return await _dbContext.Members.FindAsync(id);
    }

    public void Dispose()
    {
        _dbContext.Dispose();
        GC.SuppressFinalize(this);
    }
}
