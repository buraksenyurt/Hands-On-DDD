namespace App.Domain;

public class ClassifiedBook
{
    public ClassifiedBookId Id { get; private set; }

    public ClassifiedBook(ClassifiedBookId id, MemberId ownerId)
    {
        Id = id;
        _ownerId = ownerId;
    }

    // Behaviors
    public void SetTitle(string title) => _title = title;
    public void UpdateDetails(string details) => _details = details;
    public void UpdateListPrice(decimal listPrice) => _listPrice = listPrice;

    private MemberId _ownerId;
    private string _title;
    private string _details;
    private decimal _listPrice;

}
