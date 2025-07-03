using NextBlog.Api.DTOs.Follow;

namespace NextBlog.Api.Repositories
{
    public interface IFollowRepository
    {
        Task FollowAsync(string followerId, string followingId);
        Task UnfollowAsync(string followerId, string followingId);
        Task<List<FollowResponseRecord>> GetFollowingAsync(string userId);
        Task<List<FollowResponseRecord>> GetFollowersAsync(string userId);
        Task<bool> IsFollowingAsync(string followerId, string followingId);
    }
}
