using App.Domain.Shared;
using App.Framework;

namespace App.Domain.BookNotice;

public class Comment(Action<object> applier)
    : Entity<CommentId>(applier)
{
    public BookId ParentId { get; private set; }
    public MemberId OwnerId { get; private set; }
    public string Text { get; private set; }
    public CreateDate CreateDate { get; private set; }
    public int Rating { get; private set; }

    protected override void When(object @event)
    {
        switch (@event)
        {
            case Events.CommentAddedToBookNotice e:
                ParentId = new BookId(e.BookId);
                OwnerId = new MemberId(e.OwnerId);
                Id = new CommentId(e.CommentId);
                Text = e.Comment;
                CreateDate = CreateDate.From(e.CreateDate);
                break;
            case Events.CommentRated e:
                if (e.Point < 1 || e.Point > 5)
                {
                    throw new ArgumentOutOfRangeException(nameof(e.Point), "Rating must be between 1 and 5");
                }
                if (OwnerId != new MemberId(e.UserId))
                {
                    throw new InvalidOperationException("You can not rate your own comment");
                }

                Rating = e.Point;
                Raise(new Events.CommentRated
                {
                    BookId = ParentId,
                    CommentId = Id,
                    UserId = OwnerId,
                    Point = e.Point
                });

                break;
        }
    }
}

public record CommentId(Guid Value)
    : ValueObject<CommentId>
{
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator Guid(CommentId id) => id.Value;
    public static implicit operator CommentId(Guid id) => new(id);
}