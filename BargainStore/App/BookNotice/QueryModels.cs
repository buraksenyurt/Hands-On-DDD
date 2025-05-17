namespace App.BookNotice;

public static class QueryModels
{
    public class GetBookNotice
    {
        public Guid Id { get; set; }
    }
    public class GetBooksOnSales
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
    public class GetPendingReviewsBooks
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
