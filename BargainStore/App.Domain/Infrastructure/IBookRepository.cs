using App.Domain.BookNotice;

namespace App.Domain.Infrastructure;

public interface IBookRepository
{
    Task<BookNotice.Book> LoadAsync(BookId id);
    Task SaveAsync(BookNotice.Book entity);
    Task<bool> IsExistsAsync(BookId id);
}
