using App.Domain.BookNotice.Events;
using App.Domain.Exceptions;
using App.Framework;

namespace App.Domain.BookNotice;

public class Book
    : AggregateRoot<BookId>
{
    public Book(BookId id, MemberId memberId)
    {
        Raise(new BookEvents.Created
        {
            Id = id,
            OwnerId = memberId,
            CreateDate = DateTime.UtcNow
        });
    }
    public BookId Id { get; private set; }
    public MemberId OwnerId { get; private set; }
    public BookTitle Title { get; private set; }
    public BookSummary Summary { get; private set; }
    public SalesPrice SalesPrice { get; private set; }
    public Date CreateDate { get; private set; }
    public Date SentDate { get; private set; }
    public Date ActivateDate { get; private set; }
    public MemberId ApprovedBy { get; private set; }
    public BookSalesState SalesState { get; private set; } = BookSalesState.Inactive;
    public List<Comment> Comments { get; private set; } = [];

    // Behaviors
    public void SetTitle(BookTitle title)
    {
        Raise(new BookEvents.TitleChanged
        {
            Id = Id,
            Title = title
        });
    }
    public void UpdateSummary(BookSummary summary)
    {
        Raise(new BookEvents.SummaryUpdated
        {
            Id = Id,
            Summary = summary
        });
    }
    public void UpdateSalesPrice(SalesPrice salesPrice)
    {
        Raise(new BookEvents.SalesPriceUpdated
        {
            Id = Id,
            SalesPrice = salesPrice,
            CurrencyCode = salesPrice.CurrencyCodeInfo.Code
        });
    }
    // Behaviors

    public void RequestToPublish()
    {
        Raise(new BookEvents.SentForReview
        {
            Id = Id,
            SentDate = DateTime.UtcNow
        });
    }

    public void AddComment(string comment, MemberId ownerId)
    {
        Raise(new BookEvents.CommentAddedToBookNotice
        {
            BookId = Id,
            CommentId = Guid.NewGuid(),
            OwnerId = ownerId,
            Comment = comment,
            CreateDate = DateTime.UtcNow
        });
    }

    public void RateComment(CommentId commentId, int point, MemberId ownerId)
    {
        Raise(new BookEvents.CommentRated
        {
            BookId = Id,
            CommentId = commentId,
            UserId = ownerId,
            Point = point
        });
    }

    protected override void When(object @event)
    {
        switch (@event)
        {
            case BookEvents.Created e:
                Id = new BookId(e.Id);
                OwnerId = new MemberId(e.OwnerId);
                CreateDate = new Date(e.CreateDate);
                SalesState = BookSalesState.Inactive;
                break;
            case BookEvents.TitleChanged e:
                Title = new BookTitle(e.Title);
                break;
            case BookEvents.SummaryUpdated e:
                Summary = new BookSummary(e.Summary);
                break;
            case BookEvents.SalesPriceUpdated e:
                SalesPrice = new SalesPrice(e.SalesPrice, e.CurrencyCode);
                break;
            case BookEvents.CommentAddedToBookNotice e:
                Comment comment = new(Raise);
                ApplyToEntity(comment, e);
                break;
            case BookEvents.CommentRated e:
                var commentId = new CommentId(e.CommentId);
                var commentToRate = Comments.FirstOrDefault(c => c.Id == commentId) ?? throw new InvalidOperationException("Comment not found");
                ApplyToEntity(commentToRate, e);
                break;
            case BookEvents.SentForReview e:
                SalesState = BookSalesState.PendingReview;
                SentDate = new Date(e.SentDate);
                break;
        }
    }

    protected override void ValidateSate()
    {
        var is_valid =
                Id != null &&
                OwnerId != null &&
                (SalesState switch
                {
                    BookSalesState.PendingReview =>
                        Title != null
                        && Summary != null
                        && SalesPrice?.Amount > 0
                        && SentDate.Value != default,
                    BookSalesState.Active =>
                        Title != null
                        && Summary != null
                        && SalesPrice?.Amount > 0
                        && ApprovedBy != null
                        && ActivateDate.Value != default,
                    _ => true
                });

        if (!is_valid)
        {
            throw new InvalidEntityStateException(this, $"Post-checks failed in state {SalesState}");
        }
    }

    public enum BookSalesState
    {
        Active,
        Inactive,
        MarkAsSold,
        PendingReview
    }

    public void Apply(object @event)
    {
        When(@event);
    }
}
