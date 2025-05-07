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
    }
}
