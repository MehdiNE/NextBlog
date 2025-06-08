namespace NextBlog.Api.DTOs.Posts
{
    public class PostsResponse
    {
        public IEnumerable<PostResponse> Items { get; init; } = [];
    }
}
