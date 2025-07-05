using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NextBlog.Api.DTOs;
using NextBlog.Api.DTOs.Posts;
using NextBlog.Api.Extensions;
using NextBlog.Api.Mapping;
using NextBlog.Api.Models;
using NextBlog.Api.Repositories;
using NextBlog.Api.Services;

namespace NextBlog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest request)
        {
            string userId = User.GetUserId();
            var post = request.MapToPost(userId);

            await _postService.CreateAsync(post);
            return CreatedAtAction(nameof(Get), new { id = post.Id }, post);

        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var post = await _postService.GetByIdAsync(id);

            if (post is null)
            {
                return NotFound();
            }

            var response = post.MapToResponse();
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<PostsResponse>> GetAll([FromQuery] PostFilterRequest filterRequest, [FromQuery] PaginationRequest paginationRequest)
        {
            var (posts, totalCount) = await _postService.GetAllAsync(filterRequest, paginationRequest);

            var response = posts.MapToResponse(totalCount);
            return Ok(response);
        }

        [HttpPut("{id:Guid}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdatePostRequest request)
        {
            string userId = User.GetUserId();
            var post = request.MapToPost(id, userId);

            var updatedPost = await _postService.UpdateAsync(post);

            if (updatedPost is null)
            {
                return NotFound();
            }

            var response = updatedPost.MapToResponse();
            return Ok(response);
        }

        [HttpDelete("{id:Guid}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            string userId = User.GetUserId();
            var deleted = await _postService.DeleteByIdAsync(id, userId);

            if (!deleted)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
