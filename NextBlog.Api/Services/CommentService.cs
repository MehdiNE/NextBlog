using NextBlog.Api.Models;
using NextBlog.Api.Repositories;

namespace NextBlog.Api.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<bool> CreateAsync(Comment comment)
        {
            return await _commentRepository.CreateAsync(comment);
        }

        public Task<bool> DeleteByIdAsync(Guid postId, Guid commentId, string userId)
        {
            return _commentRepository.DeleteByIdAsync(postId, commentId, userId);
        }

        public Task<IEnumerable<Comment>> GetAllAsync(Guid postId)
        {
            return _commentRepository.GetAllAsync(postId);
        }

        public Task<Comment?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
