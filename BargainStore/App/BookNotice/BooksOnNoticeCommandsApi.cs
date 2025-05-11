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
        return await HandleRequest(request, _booksOnNoticeApplicationService.Handle);
    }

    [HttpPut("title")]
    public async Task<IActionResult> PutTitle([FromBody] V1.SetTitle request)
    {
        _logger.LogInformation("Set title request {Id}, {Title}", request.Id, request.Title);
        return await HandleRequest(request, _booksOnNoticeApplicationService.Handle);
    }

    [HttpPut("summary")]
    public async Task<IActionResult> PutSummary([FromBody] V1.SetSummary request)
    {
        _logger.LogInformation("Set summary request {Id}, {Summary}", request.Id, request.Summary);
        return await HandleRequest(request, _booksOnNoticeApplicationService.Handle);
    }

    [HttpPut("sales-price")]
    public async Task<IActionResult> PutSalesPrice([FromBody] V1.UpdateSalesPrice request)
    {
        _logger.LogInformation("Update sales price request {Id}, {SalesPrice}", request.Id, request.SalesPrice);
        return await HandleRequest(request, _booksOnNoticeApplicationService.Handle);
    }

    [HttpPut("request-to-publish")]
    public async Task<IActionResult> PutRequestToPublish([FromBody] V1.RequestToPublish request)
    {
        _logger.LogInformation("Request to publish book {Id}, {SentDate}", request.Id, request.SentDate);
        return await HandleRequest(request, _booksOnNoticeApplicationService.Handle);
    }

    private async Task<IActionResult> HandleRequest<T>(T request, Func<T, Task> handler)
    {
        try
        {
            _logger.LogDebug("Handling HTTP request of type {type}", typeof(T).Name);
            await handler(request);
            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error handling the request");
            return new BadRequestObjectResult(new { error = e.Message, stackTrace = e.StackTrace });
        }
    }
}
