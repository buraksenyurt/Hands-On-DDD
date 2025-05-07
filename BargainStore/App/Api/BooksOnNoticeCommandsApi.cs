using Microsoft.AspNetCore.Mvc;
using static App.Contracts.BooksOnNotice;

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
    public async Task<IActionResult> Post([FromBody] V1.Create request)
    {
        _logger.LogInformation("Create book request {Id}, {OwnerId}", request.Id, request.OwnerId);

        await _booksOnNoticeApplicationService.Handle(request);

        return Ok(new { Message = "Book created successfully" });
    }

    [HttpPut("title")]
    public async Task<IActionResult> PutTitle([FromBody] V1.SetTitle request)
    {
        _logger.LogInformation("Set title request {Id}, {Title}", request.Id, request.Title);

        await _booksOnNoticeApplicationService.Handle(request);

        return Ok(new { Message = "Book title updated successfully" });
    }

    [HttpPut("summary")]
    public async Task<IActionResult> PutSummary([FromBody] V1.SetSummary request)
    {
        _logger.LogInformation("Set summary request {Id}, {Summary}", request.Id, request.Summary);

        await _booksOnNoticeApplicationService.Handle(request);

        return Ok(new { Message = "Book summary updated successfully" });
    }

    [HttpPut("sales-price")]
    public async Task<IActionResult> PutSalesPrice([FromBody] V1.UpdateSalesPrice request)
    {
        _logger.LogInformation("Update sales price request {Id}, {SalesPrice}", request.Id, request.SalesPrice);

        await _booksOnNoticeApplicationService.Handle(request);

        return Ok(new { Message = "Book sales price updated successfully" });
    }

    [HttpPut("request-to-publish")]
    public async Task<IActionResult> PutRequestToPublish([FromBody] V1.RequestToPublish request)
    {
        _logger.LogInformation("Request to publish book {Id}, {SentDate}", request.Id, request.SentDate);

        await _booksOnNoticeApplicationService.Handle(request);

        return Ok(new { Message = "Book requested for publication successfully" });
    }
}
