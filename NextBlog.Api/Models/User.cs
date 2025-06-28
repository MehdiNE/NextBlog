namespace NextBlog.Api.Models
{
    public sealed class User
    {
        public Guid Id { get; set; }
        public required string Email { get; set; }
        public required string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid IdentityId { get; set; }
    }
}
