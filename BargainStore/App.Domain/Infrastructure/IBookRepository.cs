using App.Domain.BookNotice;

namespace App.Domain.Infrastructure;

public interface IBookRepository
{
    Task<Book> LoadAsync(BookId id);
    Task SaveAsync(Book entity);
    Task<bool> IsExistsAsync(BookId id);
}
