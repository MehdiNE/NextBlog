using NextBlog.Api.DTOs;
using NextBlog.Api.DTOs.Posts;
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

        public async Task<(IEnumerable<Post>, int totalCount)> GetAllAsync(PostFilterRequest filterRequest, PaginationRequest paginationRequest)
        {
            return await _postRepository.GetAllAsync(filterRequest, paginationRequest);
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
