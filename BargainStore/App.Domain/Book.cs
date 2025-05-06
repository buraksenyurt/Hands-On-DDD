using App.Domain.Exceptions;

namespace App.Domain;

public class Book(BookId id, MemberId ownerId)
    : IEntity
{
    public BookId Id { get; private set; } = id;
    public MemberId OwnerId { get; }
    public BookTitle Title { get; private set; }
    public BookDetails Details { get; private set; }
    public SalesPrice SalesPrice { get; private set; }
    public BookCrateDate CreateDate { get; private set; }
    public MemberId ApprovedBy { get; set; }
    public BookSalesState SalesState { get; private set; }

    // Behaviors
    public void SetTitle(BookTitle title)
    {
        Title = title;
    }
    public void UpdateDetails(BookDetails details)
    {
        Details = details;
    }
    public void UpdateSalesPrice(SalesPrice salesPrice)
    {
        SalesPrice = salesPrice;
    }
    // Behaviors

    public void RequestToPublish()
    {
        CreateDate = new BookCrateDate(DateTime.Now);
        SalesState = BookSalesState.PendingReview;
        Validate();
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
        if (SalesPrice?.Amount == 0)
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
