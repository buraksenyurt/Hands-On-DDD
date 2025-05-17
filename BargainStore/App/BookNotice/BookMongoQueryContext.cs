using App.Infrastructure;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace App.BookNotice;

public class BookMongoQueryContext
{
    private readonly IMongoCollection<BookDocument> _books;
    public IMongoCollection<BookDocument> Books
    {
        get
        {
            return _books;
        }
    }

    public BookMongoQueryContext(IOptions<MongoDbSettings> settings, IMongoClient client)
    {
        var db = client.GetDatabase(settings.Value.DatabaseName);
        _books = db.GetCollection<BookDocument>(settings.Value.CollectionName);
    }
}
