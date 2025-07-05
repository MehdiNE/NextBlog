
using Microsoft.EntityFrameworkCore;
using NextBlog.Api.Database;
using NextBlog.Api.Models;

namespace NextBlog.Api.Repositories.Like
{
    public class LikeRepository : ILikeRepository
    {
        private readonly ApplicationDbContext _context;

        public LikeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddLikeAsync(PostLike like)
        {
            _context.PostLike.Add(like);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<PostLike?> GetLikeAsync(Guid postId, string userId)
        {
            var like = await _context.PostLike.SingleOrDefaultAsync(x => x.PostId == postId && x.UserId == userId);

            return like;
        }

        public async Task<int> GetLikesAsync(Guid postId)
        {
            var likesCount = await _context.PostLike.CountAsync(x => x.PostId == postId);

            return likesCount;
        }

        public async Task<bool> RemoveLikeAsync(PostLike like)
        {
            _context.PostLike.Remove(like);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
