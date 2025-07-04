using NextBlog.Api.Models;

namespace NextBlog.Api.Repositories
{
    public interface ICommentRepository
    {
        Task<bool> CreateAsync(Comment comment);
        Task<Comment?> GetByIdAsync(Guid id);
        Task<IEnumerable<Comment>> GetAllAsync(Guid postId);
        Task<bool> DeleteByIdAsync(Guid postId, Guid commentId, string userId);
    }
}
