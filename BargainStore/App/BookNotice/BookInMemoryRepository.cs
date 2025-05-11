using App.Domain.BookNotice;
using App.Domain.Infrastructure;

namespace App.BookNotice;

public class BookInMemoryRepository(ILogger<BookInMemoryRepository> logger)
    : IBookRepository
{
    private readonly Dictionary<BookId, Book> _books = [];
    private readonly ILogger<BookInMemoryRepository> _logger = logger;

    public Task<bool> IsExistsAsync(BookId id)
    {
        _logger.LogInformation("Checking if book with ID {} exists", id);
        return Task.FromResult(_books.ContainsKey(id));
    }

    public Task<Book> LoadAsync(BookId id)
    {
        _logger.LogInformation("Loading book with ID {}", id);
        if (_books.TryGetValue(id, out var book))
        {
            return Task.FromResult(book);
        }
        else
        {
            return Task.FromResult<Book>(null);
        }
    }

    public Task SaveAsync(Book entity)
    {
        _logger.LogInformation("Saving book with ID {}", entity.Id);
        if (!_books.TryAdd(entity.Id, entity))
        {
            _books[entity.Id] = entity;
        }

        return Task.CompletedTask;
    }
}
