using Microsoft.EntityFrameworkCore;
using NextBlog.Api.Database;
using NextBlog.Api.Models;

namespace NextBlog.Api.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteByIdAsync(Guid postId, Guid commentId)
        {
            var existingComment = await _context.Comments.SingleOrDefaultAsync(x => x.Id == commentId && x.PostId == postId);

            if (existingComment is null) return false;

            _context.Comments.Remove(existingComment);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<IEnumerable<Comment>> GetAllAsync(Guid postId)
        {
            var comments = await _context.Comments.Where(x => x.PostId == postId).ToListAsync();
            return comments;
        }

        public Task<Comment?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
