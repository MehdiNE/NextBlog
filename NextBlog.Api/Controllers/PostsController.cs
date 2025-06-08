using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NextBlog.Api.DTOs.Posts;
using NextBlog.Api.Mapping;
using NextBlog.Api.Models;
using NextBlog.Api.Repositories;

namespace NextBlog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _postRepository;

        public PostsController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest request)
        {
            var post = request.MapToPost();

            await _postRepository.CreateAsync(post);
            return CreatedAtAction(nameof(Get), new { id = post.Id }, post);

        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var post = await _postRepository.GetByIdAsync(id);

            if (post is null)
            {
                return NotFound();
            }

            var response = post.MapToResponse();
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var posts = await _postRepository.GetAllAsync();

            var response = posts.MapToResponse();
            return Ok(response);
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdatePostRequest request)
        {
            var post = request.MapToPost(id);

            var updated = await _postRepository.UpdateAsync(post);

            if (!updated)
            {
                return NotFound();
            }

            var response = post.MapToResponse();
            return Ok(response);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deleted = await _postRepository.DeleteByIdAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
