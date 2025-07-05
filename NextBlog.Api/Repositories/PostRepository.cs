using Microsoft.EntityFrameworkCore;
using NextBlog.Api.Database;
using NextBlog.Api.DTOs;
using NextBlog.Api.DTOs.Posts;
using NextBlog.Api.Models;

namespace NextBlog.Api.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(Post post)
        {
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteByIdAsync(Guid id, string userId)
        {
            var post = await _context.Posts.SingleOrDefaultAsync(p => p.Id == id);
            if (post is null)
            {
                return false;
            }

            if (userId != post.UserId)
            {
                return false;
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsByIdAsync(Guid id)
        {
            var post = await _context.Posts.SingleOrDefaultAsync(post => post.Id == id);
            if (post is null)
            {
                return false;
            }
            return true;
        }

        public async Task<(IEnumerable<Post>, int totalCount)> GetAllAsync(PostFilterRequest filterRequest, PaginationRequest paginationRequest)
        {
            var query = _context.Posts.AsQueryable();

            if (!string.IsNullOrEmpty(filterRequest.Title))
            {
                query = query.Where(p => p.Title.Contains(filterRequest.Title));
            }

            if (!string.IsNullOrEmpty(filterRequest.Content))
            {
                query = query.Where(p => p.Content.Contains(filterRequest.Content));
            }

            var posts = await query
                .Skip((paginationRequest.PageNumber - 1) * paginationRequest.PageSize)
                .Take(paginationRequest.PageSize)
                .ToListAsync();

            var totalCount = await query.CountAsync();

            return (posts, totalCount);

        }

        public async Task<Post?> GetByIdAsync(Guid id)
        {
            var post = await _context.Posts.SingleOrDefaultAsync(post => post.Id == id);
            return post;
        }

        public async Task<bool> UpdateAsync(Post post)
        {
            var existingMovie = await _context.Posts.SingleOrDefaultAsync(x => x.Id == post.Id);

            if (existingMovie is null) return false;
            if (existingMovie.UserId != post.UserId)
            {
                return false;
            }

            existingMovie.Title = post.Title;
            existingMovie.Content = post.Content;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
