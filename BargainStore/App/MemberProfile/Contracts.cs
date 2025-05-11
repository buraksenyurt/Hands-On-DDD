namespace App.MemberProfile;

public static class Contracts
{
    public static class V1
    {
        public class CreateMember
        {
            public Guid Id { get; set; }
            public required string FullName { get; set; }
            public required string Email { get; set; }
        }
        public class UpdateFullName
        {
            public Guid Id { get; set; }
            public required string FullName { get; set; }
        }
        public class UpdateEmail
        {
            public Guid Id { get; set; }
            public required string Email { get; set; }
        }
    }
}
