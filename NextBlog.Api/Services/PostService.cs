using NextBlog.Api.Models;
using NextBlog.Api.Repositories;

namespace NextBlog.Api.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<bool> CreateAsync(Post post)
        {
            return await _postRepository.CreateAsync(post);
        }

        public async Task<bool> DeleteByIdAsync(Guid id, string userId)
        {
            return await _postRepository.DeleteByIdAsync(id, userId);
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await _postRepository.GetAllAsync();
        }

        public async Task<Post?> GetByIdAsync(Guid id)
        {
            return await _postRepository.GetByIdAsync(id);
        }

        public async Task<Post?> UpdateAsync(Post post)
        {
            var postExist = await _postRepository.ExistsByIdAsync(post.Id);
            if (!postExist)
            {
                return null;
            }

            await _postRepository.UpdateAsync(post);
            return post;
        }
    }
}
