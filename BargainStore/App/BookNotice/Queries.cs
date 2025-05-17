using MongoDB.Driver;
using static App.BookNotice.QueryModels;
using static App.BookNotice.ReadModels;

namespace App.BookNotice;

public static class Queries
{
    private static BookMongoQueryContext _context;

    public static void Configure(BookMongoQueryContext context)
    {
        _context = context;
    }

    public static async Task<Book> Query(GetBookNotice query)
    {
        var filter = Builders<BookDocument>.Filter.Eq(b => b.Id, query.Id.ToString());
        var doc = await _context.Books.Find(filter).FirstOrDefaultAsync();

        if (doc == null)
            throw new Exception("Book not found");

        return new Book
        {
            BookId = Guid.Parse(doc.Id),
            Title = doc.Title,
            Summary = doc.Summary,
            SalesPrice = doc.SalesPrice,
            CreatedAt = doc.CreateDate,
            SalesState = doc.SalesState,
            Comments = doc.Comments?.Select(c => new Comment
            {
                CommentId = c.Id,
                Text = c.Text,
                Rating = c.Rating
            }).ToList() ?? []
        };
    }

    public static async Task<List<Book>> Query(GetBooksOnSales query)
    {
        var filter = Builders<BookDocument>.Filter.Eq(b => b.SalesState, "Active");

        var skip = (query.Page - 1) * query.PageSize;
        var docs = await _context.Books
            .Find(filter)
            .Skip(skip)
            .Limit(query.PageSize)
            .ToListAsync();

        return docs.Select(doc => new Book
        {
            BookId = Guid.Parse(doc.Id),
            Title = doc.Title,
            Summary = doc.Summary,
            SalesPrice = doc.SalesPrice,
            CreatedAt = doc.CreateDate,
            SalesState = doc.SalesState,
            Comments = doc.Comments?.Select(c => new Comment
            {
                CommentId = c.Id,
                Text = c.Text,
                Rating = c.Rating
            }).ToList() ?? []
        }).ToList();
    }
}

