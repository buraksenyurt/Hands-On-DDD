using App.Domain.BookNotice;
using App.Domain.Infrastructure;
using App.Domain.Shared;
using App.Framework;

namespace App.Api;

public class BooksOnNoticeApplicationService(
    ILogger<BooksOnNoticeApplicationService> logger,
    IBookRepository bookRepository,
    ICurrencyCodeLookup currencyCodeLookup,
    IUnitOfWork unitOfWork
    ) : IApplicationService
{
    private readonly ILogger _logger = logger;
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly ICurrencyCodeLookup _currencyCodeLookup = currencyCodeLookup;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Handle(object command)
    {
        _logger.LogInformation("Handling command: {}", command.GetType().FullName);
        await _unitOfWork.StartTransactionAsync();

        using (_unitOfWork)
        {
            try
            {
                Book book;

                switch (command)
                {
                    case Contracts.BooksOnNotice.V1.Create createCmd:
                        if (await _bookRepository.IsExistsAsync(new BookId(createCmd.Id)))
                        {
                            throw new InvalidOperationException($"Book with ID {createCmd.Id} already exists.");
                        }
                        _logger.LogWarning("Creating book with ID {Id}, OwnerId {OwnerId}", createCmd.Id, createCmd.OwnerId);
                        book = new Book(new BookId(createCmd.Id), new MemberId(createCmd.OwnerId));
                        await _bookRepository.SaveAsync(book);
                        break;
                    case Contracts.BooksOnNotice.V1.SetTitle setTitleCmd:
                        await HandleUpdate(setTitleCmd.Id, book =>
                        {
                            book.SetTitle(BookTitle.FromString(setTitleCmd.Title));
                        });
                        break;
                    case Contracts.BooksOnNotice.V1.SetSummary updateSummaryCmd:
                        await HandleUpdate(updateSummaryCmd.Id, book =>
                        {
                            book.UpdateSummary(BookSummary.FromString(updateSummaryCmd.Summary));
                        });
                        break;
                    case Contracts.BooksOnNotice.V1.UpdateSalesPrice updateSalesPriceCmd:
                        await HandleUpdate(updateSalesPriceCmd.Id, book =>
                        {
                            book.UpdateSalesPrice(new SalesPrice(updateSalesPriceCmd.SalesPrice, updateSalesPriceCmd.CurrencyCode, _currencyCodeLookup));
                        });
                        break;
                    case Contracts.BooksOnNotice.V1.RequestToPublish requestToPublish:
                        await HandleUpdate(requestToPublish.Id, book =>
                        {
                            book.RequestToPublish();
                        });
                        break;
                    default:
                        throw new InvalidOperationException($"Command type {command.GetType().FullName} is unknown");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling command: {}", command.GetType().FullName);
                throw;
            }
            finally
            {
                await _unitOfWork.CommitAsync();
            }
        }
    }

    private async Task HandleUpdate(Guid id, Action<Book> operation)
    {
        var book = await _bookRepository.LoadAsync(id.ToString());
        if (book == null)
        {
            throw new InvalidOperationException($"Entity with id {id} cannot be found");
        }

        operation(book);
        await _bookRepository.SaveAsync(book);
    }
}
