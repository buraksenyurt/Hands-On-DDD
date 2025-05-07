using App.Domain.BookNotice;
using App.Domain.Services;

namespace App.Providers;

public class BookRepository
    : IBookRepository
{
    public Task<bool> IsExistsAsync(BookId id)
    {
        throw new NotImplementedException();
    }

    public Task<Book> LoadAsync(BookId id)
    {
        throw new NotImplementedException();
    }

    public Task SaveAsync(Book entity)
    {
        throw new NotImplementedException();
    }
}
