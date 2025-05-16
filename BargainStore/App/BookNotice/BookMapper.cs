using App.Domain.BookNotice;

namespace App.BookNotice;

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
            Comments = [.. book.Comments.Select(c => new CommentDocument
            {
                Id = c.Id,
                OwnerId = c.OwnerId.ToString(),
                Text = c.Text,
                CreateDate = c.CreateDate.Value,
                Rating = c.Rating
            })]
        };
    }

    public static Book ToDomain(this BookDocument doc)
    {
        var book = new Book(doc.Id, doc.OwnerId);
        if (!string.IsNullOrEmpty(doc.Title))
        {
            book.Apply(new Events.TitleChanged
            {
                Id = book.Id,
                Title = doc.Title
            });
        }

        if (!string.IsNullOrEmpty(doc.Summary))
        {
            book.Apply(new Events.SummaryUpdated
            {
                Id = book.Id,
                Summary = doc.Summary
            });
        }

        if (doc.SalesPrice > 0)
        {
            book.Apply(new Events.SalesPriceUpdated
            {
                Id = book.Id,
                SalesPrice = doc.SalesPrice,
                CurrencyCode = doc.CurrencyCode
            });
        }

        if (doc.SentDate.HasValue)
        {
            book.Apply(new Events.SentForReview
            {
                Id = book.Id,
                SentDate = doc.SentDate.Value
            });
        }

        foreach (var commentDoc in doc.Comments)
        {
            var e = new Events.CommentAddedToBookNotice
            {
                BookId = Guid.Parse(doc.Id),
                CommentId = commentDoc.Id,
                OwnerId = Guid.Parse(doc.OwnerId),
                Comment = commentDoc.Text,
                CreateDate = commentDoc.CreateDate
            };

            book.Apply(e);

            if (commentDoc.Rating > 0)
            {
                var rateEvent = new Events.CommentRated
                {
                    BookId = Guid.Parse(doc.Id),
                    CommentId = commentDoc.Id,
                    UserId = Guid.Parse(doc.OwnerId),
                    Point = commentDoc.Rating
                };
                book.Apply(rateEvent);
            }
        }

        return book;
    }
}
