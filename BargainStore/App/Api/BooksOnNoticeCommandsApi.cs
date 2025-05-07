using Microsoft.AspNetCore.Mvc;

namespace App.Api;

[ApiController]
[Route("/api/notice")]
public class BooksOnNoticeCommandsApi(
    ILogger<BooksOnNoticeCommandsApi> logger,
    BooksOnNoticeApplicationService booksOnNoticeApplicationService)
    : ControllerBase
{
    private readonly ILogger _logger = logger;
    private readonly BooksOnNoticeApplicationService _booksOnNoticeApplicationService = booksOnNoticeApplicationService;

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Contracts.BooksOnNotice.V1.Create request)
    {
        _logger.LogInformation("Create book request {Id}, {OwnerId}", request.Id, request.OwnerId);

        await _booksOnNoticeApplicationService.Handle(request);

        return Ok(new { Message = "Book created successfully" });
    }
}
