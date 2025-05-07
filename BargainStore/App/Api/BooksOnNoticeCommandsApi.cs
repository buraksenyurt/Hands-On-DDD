using Microsoft.AspNetCore.Mvc;

namespace App.Api;

[ApiController]
[Route("/api/notice")]
public class BooksOnNoticeCommandsApi : ControllerBase
{
    private readonly ILogger _logger;

    public BooksOnNoticeCommandsApi(ILogger<BooksOnNoticeCommandsApi> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Contracts.BooksOnNotice.V1.Create request)
    {
        _logger.LogInformation("Create book request {Id}, {OwnerId}", request.Id, request.OwnerId);

        // Handle the request here

        return Ok("Command received");
    }
}
