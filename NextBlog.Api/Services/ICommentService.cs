using NextBlog.Api.Models;

namespace NextBlog.Api.Services
{
    public interface ICommentService
    {
        Task<bool> CreateAsync(Comment comment);
        Task<Comment?> GetByIdAsync(Guid id);
        Task<IEnumerable<Comment>> GetAllAsync(Guid postId);
        Task<bool> DeleteByIdAsync(Guid postId, Guid commentId);
    }
}
