namespace NextBlog.Api.DTOs.Posts
{
    public sealed class PostFilterRequest
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
    }
}
