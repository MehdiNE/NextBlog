namespace NextBlog.Api.DTOs.Posts
{
    public class CreatePostRequest
    {
        public required string Title { get; init; }
        public required string Content { get; init; }
    }
}
