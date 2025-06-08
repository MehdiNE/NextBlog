using Microsoft.EntityFrameworkCore;
using NextBlog.Api.Database;
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

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var post = await _context.Posts.SingleOrDefaultAsync(p => p.Id == id);
            if (post is null)
            {
                return false;
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await _context.Posts.ToListAsync();
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

            existingMovie.Title = post.Title;
            existingMovie.Content = post.Content;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
