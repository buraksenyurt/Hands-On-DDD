using App.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace App.BookNotice;

[ApiController]
[Route("/api/notice")]
public class BooksOnNoticeQueryApi(ILogger<BooksOnNoticeQueryApi> logger)
    : ControllerBase
{
    private readonly ILogger<BooksOnNoticeQueryApi> _logger = logger;

    [HttpGet]
    public Task<IActionResult> Get([FromQuery] QueryModels.GetBookNotice request)
    {
        return RequestHandler.HandleQuery(() => Queries.Query(request), _logger);
    }

    [HttpGet]
    [Route("on-sales")]
    public Task<IActionResult> Get([FromQuery] QueryModels.GetBooksOnSales request)
    {
        return RequestHandler.HandleQuery(() => Queries.Query(request), _logger);
    }
}