namespace NextBlog.Api.DTOs.Posts
{
    public class PostResponse
    {
        public required Guid Id { get; init; }
        public required string Title { get; init; }
        public required string Content { get; init; }
    }
}
