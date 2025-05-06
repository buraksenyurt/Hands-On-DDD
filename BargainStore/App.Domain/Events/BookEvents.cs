namespace App.Domain.Events;

// Domain Event'lerde primitive türler kullanılır zira bunlar sistemler arası kullanılan nesnelerdir.
// Bu nedenle Value Object kullanılmaz.
public static class BookEvents
{
    public class Created
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
    }
    public class TitleChanged
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
    }
    public class DetailsUpdated
    {
        public Guid Id { get; set; }
        public string Details { get; set; }
    }
    public class SalesPriceUpdated
    {
        public Guid Id { get; set; }
        public decimal SalesPrice { get; set; }
        public string CurrencyCode { get; set; }
    }
    public class SentForReview
    {
        public Guid Id { get; set; }
    }
}
