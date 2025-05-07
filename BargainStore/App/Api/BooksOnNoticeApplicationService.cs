using App.Domain;
using App.Domain.BookNotice;
using App.Domain.Services;
using App.Framework;

namespace App.Api;

public class BooksOnNoticeApplicationService(
    ILogger<BooksOnNoticeApplicationService> logger,
    IEntityStore entityStore,
    ICurrencyCodeLookup currencyCodeLookup)
    : IApplicationService
{
    private readonly ILogger _logger = logger;
    private readonly IEntityStore _entityStore = entityStore;
    private readonly ICurrencyCodeLookup _currencyCodeLookup = currencyCodeLookup;

    public async Task Handle(object command)
    {
        _logger.LogInformation("Handling command: {}", command.GetType().FullName);
        Book book;

        switch (command)
        {
            case Contracts.BooksOnNotice.V1.Create createCmd:
                if (await _entityStore.IsExistsAsync<Book>(createCmd.Id.ToString()))
                {
                    throw new InvalidOperationException($"Book with ID {createCmd.Id} already exists.");
                }
                book = new Book(new BookId(createCmd.Id), new MemberId(createCmd.OwnerId));
                await _entityStore.SaveAsync(book);
                break;
            case Contracts.BooksOnNotice.V1.SetTitle setTitleCmd:
                book = await GetEntity(setTitleCmd.Id);
                book.SetTitle(BookTitle.FromString(setTitleCmd.Title));
                await _entityStore.SaveAsync(book);
                break;
            case Contracts.BooksOnNotice.V1.SetSummary updateSummaryCmd:
                book = await GetEntity(updateSummaryCmd.Id);
                book.UpdateSummary(BookSummary.FromString(updateSummaryCmd.Summary));
                await _entityStore.SaveAsync(book);
                break;
            case Contracts.BooksOnNotice.V1.UpdateSalesPrice updateSalesPriceCmd:
                book = await GetEntity(updateSalesPriceCmd.Id);
                book.UpdateSalesPrice(new SalesPrice(updateSalesPriceCmd.SalesPrice, updateSalesPriceCmd.CurrencyCode, _currencyCodeLookup));
                await _entityStore.SaveAsync(book);
                break;
            case Contracts.BooksOnNotice.V1.RequestToPublish requestToPublish:
                book = await GetEntity(requestToPublish.Id);
                book.RequestToPublish();
                await _entityStore.SaveAsync(book);
                break;
            default:
                throw new InvalidOperationException($"Command type {command.GetType().FullName} is unknown");
        }
    }

    private async Task<Book> GetEntity(Guid id) => await _entityStore.LoadAsync<Book>(id.ToString()) ?? throw new InvalidOperationException($"Book with ID {id} does not exist.");
}
