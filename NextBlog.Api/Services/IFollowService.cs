using NextBlog.Api.DTOs.Follow;

namespace NextBlog.Api.Services
{
    public interface IFollowService
    {
        Task FollowAsync(string followerId, string followeeId);
        Task UnfollowAsync(string followerId, string followeeId);
        Task<FollowResponse> GetFollowingAsync(string userId);
        Task<FollowResponse> GetFollowersAsync(string userId);
    }
}
