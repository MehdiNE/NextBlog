namespace NextBlog.Api.DTOs.Posts
{
    public class PostsResponse
    {
        public IEnumerable<PostResponse> Items { get; init; } = [];
        public int TotalCount { get; set; }
    }
}
