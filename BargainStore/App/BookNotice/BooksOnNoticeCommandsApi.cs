using App.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using static App.BookNotice.Contracts;

namespace App.BookNotice;

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
        return await RequestHandler.HandleCommand(request, _booksOnNoticeApplicationService.Handle, _logger);
    }

    [HttpPut("title")]
    public async Task<IActionResult> PutTitle([FromBody] V1.SetTitle request)
    {
        _logger.LogInformation("Set title request {Id}, {Title}", request.Id, request.Title);
        return await RequestHandler.HandleCommand(request, _booksOnNoticeApplicationService.Handle, _logger);
    }

    [HttpPut("summary")]
    public async Task<IActionResult> PutSummary([FromBody] V1.SetSummary request)
    {
        _logger.LogInformation("Set summary request {Id}, {Summary}", request.Id, request.Summary);
        return await RequestHandler.HandleCommand(request, _booksOnNoticeApplicationService.Handle, _logger);
    }

    [HttpPut("sales-price")]
    public async Task<IActionResult> PutSalesPrice([FromBody] V1.UpdateSalesPrice request)
    {
        _logger.LogInformation("Update sales price request {Id}, {SalesPrice}", request.Id, request.SalesPrice);
        return await RequestHandler.HandleCommand(request, _booksOnNoticeApplicationService.Handle, _logger);
    }
    [HttpPut("add-comment")]
    public async Task<IActionResult> AddComment([FromBody] V1.AddComment request)
    {
        _logger.LogInformation("Add comment request book id: {Id}", request.Id);
        return await RequestHandler.HandleCommand(request, _booksOnNoticeApplicationService.Handle, _logger);
    }

    [HttpPut("request-to-publish")]
    public async Task<IActionResult> PutRequestToPublish([FromBody] V1.RequestToPublish request)
    {
        _logger.LogInformation("Request to publish book {Id}, {SentDate}", request.Id, request.SentDate);
        return await RequestHandler.HandleCommand(request, _booksOnNoticeApplicationService.Handle, _logger);
    }
}
