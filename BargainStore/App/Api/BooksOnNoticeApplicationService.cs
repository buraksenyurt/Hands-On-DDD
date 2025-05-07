using App.Framework;

namespace App.Api;

public class BooksOnNoticeApplicationService(ILogger<BooksOnNoticeApplicationService> logger)
    : IApplicationService
{
    private readonly ILogger _logger = logger;

    public Task Handle(object command)
    {
        switch (command)
        {
            case Contracts.BooksOnNotice.V1.Create create:
                _logger.LogInformation("Created book with ID: {}", create.Id);
                break;
            default:
                throw new InvalidOperationException($"Command type {command.GetType().FullName} is unknown");
        }

        return Task.CompletedTask;
    }
}
