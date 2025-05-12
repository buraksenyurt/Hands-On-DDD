using App.Domain.Infrastructure;
using App.Domain.MemberProfile;
using App.Domain.Shared;
using App.Framework;

namespace App.MemberProfile;

public class MemberProfileApplicationService(
    ILogger<MemberProfileApplicationService> logger,
    IMemberProfileRepository memberProfileRepository,
    IPostgresUnitOfWork unitOfWork,
    TextValidator textValidator)
        : IApplicationService
{
    private readonly ILogger _logger = logger;
    private readonly IMemberProfileRepository _memberProfileRepository = memberProfileRepository;
    private readonly IPostgresUnitOfWork _unitOfWork = unitOfWork;
    private readonly TextValidator _textValidator = textValidator;

    public async Task Handle(object command)
    {
        switch (command)
        {
            case Contracts.V1.CreateMember createCmd:
                _logger.LogInformation("Creating member profile with ID {Id}", createCmd.Id);
                if (await _memberProfileRepository.IsExistAsync(createCmd.Id))
                {
                    throw new InvalidOperationException($"Member profile with ID {createCmd.Id} already exists.");
                }
                await _unitOfWork.StartTransactionAsync();
                using (_unitOfWork)
                {
                    var newMember = new Member(
                        createCmd.Id,
                        Email.FromString(createCmd.Email, _textValidator),
                        FullName.FromString(createCmd.FullName, _textValidator)
                    );
                    await _memberProfileRepository.CreateAsync(newMember);
                    await _unitOfWork.CommitAsync();
                    break;
                }
            case Contracts.V1.UpdateFullName setNameCmd:
                await HandleUpdate(setNameCmd.Id, member =>
                {
                    member.UpdateFullName(FullName.FromString(setNameCmd.FullName, _textValidator));
                });
                break;
            case Contracts.V1.UpdateEmail setEmailCmd:
                await HandleUpdate(setEmailCmd.Id, member =>
                {
                    member.UpdateEmail(Email.FromString(setEmailCmd.Email, _textValidator));
                });
                break;
            default:
                throw new NotSupportedException($"Command type {command.GetType().FullName} is not supported.");
        }
    }

    private async Task HandleUpdate(MemberId id, Action<Member> updateAction)
    {
        _logger.LogInformation("Updating member profile with ID {Id}", id);
        await _unitOfWork.StartTransactionAsync();
        using (_unitOfWork)
        {
            var memberProfile = await _memberProfileRepository.LoadAsync(id);
            if (memberProfile == null)
            {
                throw new InvalidOperationException($"Member profile with ID {id} not found.");
            }
            updateAction(memberProfile);
            await _unitOfWork.CommitAsync();
        }
    }
}
