using NextBlog.Api.Models;

namespace NextBlog.Api.Repositories.Like
{
    public interface ILikeRepository
    {
        Task<PostLike?> GetLikeAsync(Guid postId, string userId);
        Task<bool> AddLikeAsync(PostLike like);
        Task<bool> RemoveLikeAsync(PostLike like);
        Task<int> GetLikesAsync(Guid postId);
    }
}
