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
            case Contracts.BooksOnNotice.V1.SetTitle setTitle:
                _logger.LogInformation("Set title for book with ID: {} to {}", setTitle.Id, setTitle.Title);
                break;
            case Contracts.BooksOnNotice.V1.SetSummary setSummary:
                _logger.LogInformation("Set summary for book with ID: {} to {}", setSummary.Id, setSummary.Summary);
                break;
            case Contracts.BooksOnNotice.V1.UpdateSalesPrice updateSalesPrice:
                _logger.LogInformation("Updated sales price for book with ID: {} to {}", updateSalesPrice.Id, updateSalesPrice.SalesPrice);
                break;
            case Contracts.BooksOnNotice.V1.RequestToPublish requestToPublish:
                _logger.LogInformation("Requested to publish book with ID: {} on {}", requestToPublish.Id, requestToPublish.SentDate);
                break;
            default:
                throw new InvalidOperationException($"Command type {command.GetType().FullName} is unknown");
        }

        return Task.CompletedTask;
    }
}
