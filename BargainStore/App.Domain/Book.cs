using App.Domain.Events;
using App.Domain.Exceptions;
using App.Framework;

namespace App.Domain;

public class Book
    : Entity
{
    public Book(BookId id, MemberId memberId)
    {
        Id = id;
        OwnerId = memberId;

        Raise(new BookEvents.Created
        {
            Id = id,
            OwnerId = memberId
        });
    }
    public BookId Id { get; private set; }
    public MemberId OwnerId { get; }
    public BookTitle Title { get; private set; }
    public BookDetails Details { get; private set; }
    public SalesPrice SalesPrice { get; private set; }
    public BookCrateDate CreateDate { get; private set; }
    public MemberId ApprovedBy { get; set; }
    public BookSalesState SalesState { get; private set; } = BookSalesState.Inactive;

    // Behaviors
    public void SetTitle(BookTitle title)
    {
        Title = title;
        Raise(new BookEvents.TitleChanged
        {
            Id = Id,
            Title = title
        });
    }
    public void UpdateDetails(BookDetails details)
    {
        Details = details;
        Raise(new BookEvents.DetailsUpdated
        {
            Id = Id,
            Details = details
        });
    }
    public void UpdateSalesPrice(SalesPrice salesPrice)
    {
        SalesPrice = salesPrice;
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
        CreateDate = new BookCrateDate(DateTime.Now);
        SalesState = BookSalesState.PendingReview;
        Validate();
        Raise(new BookEvents.SentForReview
        {
            Id = Id
        });
    }

    protected void Validate()
    {
        if (Id == null)
        {
            throw new InvalidEntityStateException(this, "Id can not bu null");
        }
        if (OwnerId == null)
        {
            throw new InvalidEntityStateException(this, "Owner Id can not bu null");
        }
        if (Title == null)
        {
            throw new InvalidEntityStateException(this, "Title can not be empty");
        }
        if (Details == null)
        {
            throw new InvalidEntityStateException(this, "Details can not be empty");
        }
        if (SalesPrice == null || SalesPrice.Amount <= 0)
        {
            throw new InvalidEntityStateException(this, "Sales price can not be zero");
        }
    }

    public enum BookSalesState
    {
        Active,
        Inactive,
        MarkAsSold,
        PendingReview
    }
}
