using Microsoft.AspNetCore.Mvc;

namespace App.Infrastructure;

public static class RequestHandler
{
    public static async Task<IActionResult> HandleCommand<T>(T request, Func<T, Task> handler, ILogger logger)
    {
        try
        {
            logger.LogDebug("Handling HTTP request of type {type}", typeof(T).Name);
            await handler(request);
            return new OkResult();
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error handling the request");
            return new BadRequestObjectResult(new { error = e.Message, stackTrace = e.StackTrace });
        }
    }

    public static async Task<IActionResult> HandleQuery<TModel>(Func<Task<TModel>> query, ILogger log)
    {
        try
        {
            return new OkObjectResult(await query());
        }
        catch (Exception e)
        {
            log.LogError(e, "Error handling the query");
            return new BadRequestObjectResult(new
            {
                error = e.Message,
                stackTrace = e.StackTrace
            });
        }
    }
}
