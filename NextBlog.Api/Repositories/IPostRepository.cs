using NextBlog.Api.Models;

namespace NextBlog.Api.Repositories
{
    public interface IPostRepository
    {
        Task<bool> CreateAsync(Post post);
        Task<Post?> GetByIdAsync(Guid id);
        Task<IEnumerable<Post>> GetAllAsync();
        Task<bool> UpdateAsync(Post post);
        Task<bool> DeleteByIdAsync(Guid id, string userId);
        Task<bool> ExistsByIdAsync(Guid id);
    }
}
