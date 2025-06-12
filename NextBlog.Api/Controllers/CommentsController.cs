using Microsoft.AspNetCore.Mvc;
using NextBlog.Api.DTOs.Comments;
using NextBlog.Api.Models;
using NextBlog.Api.Services;

namespace NextBlog.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost("posts/{postId:Guid}/comments")]
        public async Task<IActionResult> Create([FromBody] CreateCommentRequest request, [FromRoute] Guid postId)
        {
            var comment = new Comment
            {
                Content = request.Content,
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                PostId = postId
            };

            await _commentService.CreateAsync(comment);
            return Ok();
        }

        [HttpGet("posts/{postId:Guid}/comments")]
        public async Task<IActionResult> GetByPost([FromRoute] Guid postId)
        {
            var comments = await _commentService.GetAllAsync(postId);
            return Ok(comments);
        }

        [HttpDelete("posts/{postId:Guid}/comments/{commentId:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid postId, [FromRoute] Guid commentId)
        {
            var isDeleted = await _commentService.DeleteByIdAsync(postId, commentId);
            if (!isDeleted)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
