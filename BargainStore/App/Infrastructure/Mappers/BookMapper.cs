using App.Domain.BookNotice;
using App.Domain.BookNotice.Events;
using App.Infrastructure.Documents;

namespace App.Infrastructure.Mappers;

public static class BookMapper
{
    public static BookDocument ToDocument(this Book book)
    {
        return new BookDocument
        {
            Id = book.Id.ToString(),
            OwnerId = book.OwnerId.ToString(),
            Title = book.Title?.Value,
            Summary = book.Summary?.Value,
            SalesPrice = book.SalesPrice?.Amount ?? 0,
            CurrencyCode = book.SalesPrice?.CurrencyCodeInfo.Code,
            CreateDate = book.CreateDate?.Value ?? DateTime.UtcNow,
            SentDate = book.SentDate?.Value,
            ActivateDate = book.ActivateDate?.Value,
            SalesState = book.SalesState.ToString(),
            ApprovedBy = book.ApprovedBy?.ToString(),
            Comments = [.. book.Comments.Select(c => c.ToString())]
        };
    }

    public static Book ToDomain(this BookDocument doc)
    {
        //todo@Buraksenyurt Buraya çözüm bulalım. Aynı event bazlı yöntem Book sınıfı içerisinde zaten var.
        var book = new Book(doc.Id, doc.OwnerId);
        if (!string.IsNullOrEmpty(doc.Title))
        {
            book.Raise(new BookEvents.TitleChanged
            {
                Id = book.Id,
                Title = doc.Title
            });
        }

        if (!string.IsNullOrEmpty(doc.Summary))
        {
            book.Raise(new BookEvents.SummaryUpdated
            {
                Id = book.Id,
                Summary = doc.Summary
            });
        }

        if (doc.SalesPrice > 0)
        {
            book.Raise(new BookEvents.SalesPriceUpdated
            {
                Id = book.Id,
                SalesPrice = doc.SalesPrice,
                CurrencyCode = doc.CurrencyCode
            });
        }

        if (doc.SentDate.HasValue)
        {
            book.Raise(new BookEvents.SentForReview
            {
                Id = book.Id,
                SentDate = doc.SentDate.Value
            });
        }

        foreach (var comment in doc.Comments)
        {
            book.Raise(new BookEvents.CommentAddedToBookNotice
            {
                BookId = book.Id,
                CommentId = Guid.NewGuid(),
                // OwnerId = comment, //todo@buraksenyurt Buraya çözüm bul
                Comment = comment,
                CreateDate = DateTime.UtcNow
            });
        }

        return book;
    }
}
