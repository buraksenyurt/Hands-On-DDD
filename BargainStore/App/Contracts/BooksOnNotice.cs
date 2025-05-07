namespace App.Contracts;

public static class BooksOnNotice
{
    public static class V1
    {
        public class Create
        {
            public Guid Id { get; set; }
            public Guid OwnerId { get; set; }
            public DateTime CreatedDate { get; set; }
        }

        public class SetTitle
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
        }

        public class SetSummary
        {
            public Guid Id { get; set; }
            public string Summary { get; set; }
        }
        public class UpdateSalesPrice
        {
            public Guid Id { get; set; }
            public decimal SalesPrice { get; set; }
        }
        public class RequestToPublish
        {
            public Guid Id { get; set; }
            public DateTime SentDate { get; set; }
        }
    }
}
