using NextBlog.Api.DTOs.Follow;
using NextBlog.Api.Models;
using NextBlog.Api.Repositories;

namespace NextBlog.Api.Services
{
    public class FollowService : IFollowService
    {
        private readonly IFollowRepository _followRepository;

        public FollowService(IFollowRepository followRepository)
        {
            _followRepository = followRepository;
        }

        public async Task FollowAsync(string followerId, string followeeId)
        {
            // todo: check if id's are correct by fetching user from database
            if (followerId == followeeId)
                throw new InvalidOperationException("Cannot follow oneself.");
            if (await _followRepository.IsFollowingAsync(followerId, followeeId))
                throw new InvalidOperationException("Already following this user.");

            await _followRepository.FollowAsync(followerId, followeeId);
        }
        public Task UnfollowAsync(string followerId, string followeeId)
        {
            if (followerId == followeeId)
                throw new InvalidOperationException("Cannot unfollow oneself.");

            return _followRepository.UnfollowAsync(followerId, followeeId);
        }

        public async Task<FollowResponse> GetFollowersAsync(string userId)
        {
            var followers = await _followRepository.GetFollowersAsync(userId);
            return new FollowResponse { Items = followers };
        }

        public async Task<FollowResponse> GetFollowingAsync(string userId)
        {
            var following = await _followRepository.GetFollowingAsync(userId);
            return new FollowResponse { Items = following };
        }
    }
}
