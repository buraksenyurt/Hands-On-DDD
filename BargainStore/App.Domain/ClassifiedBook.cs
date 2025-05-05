namespace App.Domain;

public class ClassifiedBook
{
    public Guid Id { get; private set; }

    public ClassifiedBook(Guid id, Guid ownerId)
    {
        if (id == default)
        {
            throw new ArgumentException("Identity must be specified", nameof(id));
        }
        if (ownerId == default)
        {
            throw new ArgumentException("Owner Id must be specified", nameof(ownerId));
        }
        Id = id;
    }

    // Behaviors
    public void SetTitle(string title) => _title = title;
    public void UpdateDetails(string details) => _details = details;
    public void UpdateListPrice(decimal listPrice) => _listPrice = listPrice;

    private Guid _ownerId;
    private string _title;
    private string _details;
    private decimal _listPrice;

}
