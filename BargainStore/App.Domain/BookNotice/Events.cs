namespace App.Domain.BookNotice;

// Domain Event'lerde primitive türler kullanılır zira bunlar sistemler arası kullanılan nesnelerdir.
// Bu nedenle Value Object kullanılmaz.
public static class Events
{
    public class Created
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public DateTime CreateDate { get; set; }
    }
    public class TitleChanged
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
    }
    public class SummaryUpdated
    {
        public Guid Id { get; set; }
        public required string Summary { get; set; }
    }
    public class SalesPriceUpdated
    {
        public Guid Id { get; set; }
        public decimal SalesPrice { get; set; }
        public required string CurrencyCode { get; set; }
    }
    public class SentForReview
    {
        public Guid Id { get; set; }
        public DateTime SentDate {get;set;}
    }
    public class CommentAddedToBookNotice
    {
        public Guid BookId { get; set; }
        public Guid CommentId { get; set; }
        public Guid OwnerId { get; set; }
        public required string Comment { get; set; }
        public DateTime CreateDate { get; set; }
    }
    public class CommentRated
    {
        public Guid BookId { get; set; }
        public Guid CommentId { get; set; }
        public Guid UserId { get; set; }
        public int Point { get; set; }
    }
}
