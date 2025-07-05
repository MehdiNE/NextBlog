using NextBlog.Api.DTOs.Like;

namespace NextBlog.Api.Services.Like
{
    public interface ILikeService
    {
        Task<(bool Success, string? ErrorMessage)> AddLikeAsync(string userId, Guid postId);
        Task<(bool Success, string? ErrorMessage)> RemoveLikeAsync(string userId, Guid postId);
        Task<LikesResponse> GetLikesAsync(Guid postId);
    }
}
