namespace NextBlog.Api.Models
{
    public class PostLike
    {
        public required Guid Id { get; init; }
        public required DateTime CreatedAt { get; init; }

        public required string UserId { get; init; }
        public ApplicationUser? User { get; set; }

        public required Guid PostId { get; init; }
        public Post? Post { get; set; }
    }
}
