namespace NextBlog.Api.Models
{
    public class Post
    {
        public required Guid Id { get; init; }
        public required string Title { get; set; }
        public required string Content { get; set; }
    }
}
