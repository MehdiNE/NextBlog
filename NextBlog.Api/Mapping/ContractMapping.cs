using NextBlog.Api.DTOs.Posts;
using NextBlog.Api.Models;

namespace NextBlog.Api.Mapping
{
    public static class ContractMapping
    {
        public static Post MapToPost(this CreatePostRequest request, string userId)
        {
            return new Post
            {
                Content = request.Content,
                Id = Guid.NewGuid(),
                Title = request.Title,
                UserId = userId,
            };
        }

        public static PostResponse MapToResponse(this Post post)
        {
            return new PostResponse
            {
                Id = post.Id,
                Content = post.Content,
                Title = post.Title,
            };

        }

        public static PostsResponse MapToResponse(this IEnumerable<Post> posts)
        {
            return new PostsResponse
            {
                Items = posts.Select(p => p.MapToResponse()),
            };
        }

        public static Post MapToPost(this UpdatePostRequest request, Guid id, string userId)
        {
            return new Post
            {
                Content = request.Content,
                Title = request.Title,
                Id = id,
                UserId = userId,
            };
        }
    }
}
