

using NextBlog.Api.DTOs.Like;
using NextBlog.Api.Models;
using NextBlog.Api.Repositories.Like;

namespace NextBlog.Api.Services.Like
{
    public class LikeService : ILikeService
    {
        private readonly ILikeRepository _likeRepository;

        public LikeService(ILikeRepository likeRepository)
        {
            _likeRepository = likeRepository;
        }

        public async Task<(bool Success, string? ErrorMessage)> AddLikeAsync(string userId, Guid postId)
        {
            var existingLike = await _likeRepository.GetLikeAsync(postId, userId);

            if (existingLike is not null) return (false, "You have already liked this post.");

            var newLike = new PostLike { CreatedAt = DateTime.Now, PostId = postId, UserId = userId, Id = Guid.NewGuid() };

            await _likeRepository.AddLikeAsync(newLike);
            return (true, null);
        }

        public async Task<LikesResponse> GetLikesAsync(Guid postId)
        {
            var likeCount = await _likeRepository.GetLikesAsync(postId);
            var likeResponse = new LikesResponse { LikeCount = likeCount };

            return likeResponse;
        }

        public async Task<(bool Success, string? ErrorMessage)> RemoveLikeAsync(string userId, Guid postId)
        {
            var existingLike = await _likeRepository.GetLikeAsync(postId, userId);

            if (existingLike is null) return (false, "Like not found.");

            await _likeRepository.RemoveLikeAsync(existingLike);
            return (true, null);
        }
    }
}
