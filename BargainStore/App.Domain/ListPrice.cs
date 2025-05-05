namespace App.Domain;

public class ListPrice
    : Money
{
    public ListPrice(decimal amount) : base(amount)
    {
        if (amount < 0)
        {
            throw new ArgumentException("List price can not be negative", nameof(amount));
        }
    }
}
