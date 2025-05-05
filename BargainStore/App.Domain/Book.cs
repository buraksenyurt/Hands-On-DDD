namespace App.Domain;

public class Book
{
    public BookId Id { get; private set; }

    public Book(BookId id, MemberId ownerId)
    {
        Id = id;
        _ownerId = ownerId;
    }

    // Behaviors
    public void SetTitle(BookTitle title) => _title = title;
    public void UpdateDetails(BookDetails details) => _details = details;
    public void UpdateListPrice(ListPrice listPrice) => _listPrice = listPrice;

    private MemberId _ownerId;
    private BookTitle _title;
    private BookDetails _details;
    private ListPrice _listPrice;

}
