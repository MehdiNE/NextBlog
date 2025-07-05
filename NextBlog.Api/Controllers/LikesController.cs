using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NextBlog.Api.Extensions;
using NextBlog.Api.Services.Like;

namespace NextBlog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        private readonly ILikeService _likeService;

        public LikesController(ILikeService likeService)
        {
            _likeService = likeService;
        }

        [HttpPost("{postId}")]
        [Authorize]
        public async Task<IActionResult> AddLike(Guid postId)
        {
            string userId = User.GetUserId();

            var result = await _likeService.AddLikeAsync(userId, postId);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok();
        }

        [HttpDelete("{postId}")]
        [Authorize]
        public async Task<IActionResult> RemoveLike(Guid postId)
        {
            string userId = User.GetUserId();

            var result = await _likeService.RemoveLikeAsync(userId, postId);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok();
        }

        [HttpGet("{postId}")]
        public async Task<IActionResult> GetLikes(Guid postId)
        {
            var likesCount = await _likeService.GetLikesAsync(postId);

            return Ok(likesCount);
        }

    }
}
