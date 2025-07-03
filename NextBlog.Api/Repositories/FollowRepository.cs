using Microsoft.EntityFrameworkCore;
using NextBlog.Api.Database;
using NextBlog.Api.DTOs.Follow;
using NextBlog.Api.Models;

namespace NextBlog.Api.Repositories
{
    public class FollowRepository : IFollowRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task FollowAsync(string followerId, string followingId)
        {
            var follow = new Follow
            {
                FollowerId = followerId,
                FollowingId = followingId,
                FollowDate = DateTime.Now,
            };

            _context.Follow.Add(follow);
            await _context.SaveChangesAsync();
        }
        public async Task UnfollowAsync(string followerId, string followingId)
        {
            var follow = await _context.Follow.SingleOrDefaultAsync(f => f.FollowerId == followerId && f.FollowingId == followingId);

            if (follow is null)
            {
                throw new InvalidOperationException("Cannot unfollow");
            }

            _context.Follow.Remove(follow);
            await _context.SaveChangesAsync();
        }

        public async Task<List<FollowResponseRecord>> GetFollowersAsync(string userId)
        {
            var followers = await _context.Follow
                .Where(f => f.FollowingId == userId)
                .Select(f => new FollowResponseRecord
                {
                    UserId = f.FollowerId,
                    Name = f.Follower.UserName
                })
                .ToListAsync();

            return followers;
        }

        public async Task<List<FollowResponseRecord>> GetFollowingAsync(string userId)
        {
            var followers = await _context.Follow
               .Where(f => f.FollowerId == userId)
               .Select(f => new FollowResponseRecord
               {
                   UserId = f.FollowerId,
                   Name = f.Following.UserName
               })
               .ToListAsync();

            return followers;
        }

        public async Task<bool> IsFollowingAsync(string followerId, string followingId)
        {
            return await _context.Follow
                 .AnyAsync(f => f.FollowerId == followerId && f.FollowingId == followingId);
        }
    }
}
