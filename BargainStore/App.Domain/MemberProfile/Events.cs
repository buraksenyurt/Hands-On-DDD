namespace App.Domain.MemberProfile;

public static class Events
{
    public class Subscribed
    {
        public Guid Id { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }

    }
    public class Unsubscribed
    {
        public Guid Id { get; set; }
        public DateTime UnsubscribeDate { get; set; }
    }
    public class FullNameUpdated
    {
        public Guid Id { get; set; }
        public required string FullName { get; set; }
    }
    public class EmailUpdated
    {
        public Guid Id { get; set; }
        public required string Email { get; set; }
    }
    public class ProfilePhotoUpdated
    {
        public Guid Id { get; set; }
        public required string PhotoUrl { get; set; }
    }
}
