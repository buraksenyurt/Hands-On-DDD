using App.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using static App.MemberProfile.Contracts;

namespace App.MemberProfile;

[ApiController]
[Route("/api/membership")]
public class MemberProfileCommandsApi(
    ILogger<MemberProfileCommandsApi> logger,
    MemberProfileApplicationService memberProfileApplicationService)
    : ControllerBase
{
    private readonly ILogger _logger = logger;
    private readonly MemberProfileApplicationService _memberProfileApplicationService = memberProfileApplicationService;

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] V1.CreateMember request)
    {
        _logger.LogInformation("Create a new member request {Id}", request.Id);
        return await RequestHandler.HandleCommand(request, _memberProfileApplicationService.Handle, _logger);
    }

    [HttpPut("full-name")]
    public async Task<IActionResult> PutTitle([FromBody] V1.UpdateFullName request)
    {
        _logger.LogInformation("Update fullname request {Id}, {Title}", request.Id, request.FullName);
        return await RequestHandler.HandleCommand(request, _memberProfileApplicationService.Handle, _logger);
    }

    [HttpPut("email")]
    public async Task<IActionResult> PutEmail([FromBody] V1.UpdateEmail request)
    {
        logger.LogInformation("Update email request {Id}, {Title}", request.Id, request.Email);
        return await RequestHandler.HandleCommand(request, _memberProfileApplicationService.Handle, _logger);
    }
}
