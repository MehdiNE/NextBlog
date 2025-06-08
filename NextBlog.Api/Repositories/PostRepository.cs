using NextBlog.Api.Models;

namespace NextBlog.Api.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly List<Post> _posts = [];

        public Task<bool> CreateAsync(Post post)
        {
            _posts.Add(post);
            return Task.FromResult(true);
        }

        public Task<bool> DeleteByIdAsync(Guid id)
        {
            var removedCount = _posts.RemoveAll(post => post.Id == id);
            var removed = removedCount > 0;
            return Task.FromResult(removed);
        }

        public Task<IEnumerable<Post>> GetAllAsync()
        {
            return Task.FromResult(_posts.AsEnumerable());
        }

        public Task<Post?> GetByIdAsync(Guid id)
        {
            var post = _posts.SingleOrDefault(post => post.Id == id);
            return Task.FromResult(post);
        }

        public Task<bool> UpdateAsync(Post post)
        {
            var postIndex = _posts.FindIndex(x => x.Id == post.Id);
            if (postIndex == -1)
            {
                return Task.FromResult(false);
            }

            _posts[postIndex] = post;
            return Task.FromResult(true);
        }
    }
}
