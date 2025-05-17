namespace App.BookNotice;

public static class QueryModels
{
    public class GetBookNotice
    {
        public int Id { get; set; }
    }
    public class GetBooksOnSales
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
