namespace App.BookNotice;

public static class ReadModels
{
    public class Book
    {
        public Guid BookId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public decimal SalesPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public string SalesState { get; set; }
        public List<Comment> Comments { get; set; }
    }

    public class Comment
    {
        public Guid CommentId { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
    }
}
