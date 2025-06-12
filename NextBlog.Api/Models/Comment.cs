namespace NextBlog.Api.Models
{
    public class Comment
    {
        public required Guid Id { get; init; }
        public required string Content { get; init; }
        public required DateTime CreatedAt { get; init; }


        public required Guid PostId { get; init; }
        public Post Post { get; set; } = null!;
    }
}
