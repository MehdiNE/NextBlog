using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NextBlog.Api.DTOs.Follow;
using NextBlog.Api.Services;

namespace NextBlog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowController : ControllerBase
    {
        private readonly IFollowService _followService;

        public FollowController(IFollowService followService)
        {
            _followService = followService;
        }

        [HttpPost]
        public async Task<IActionResult> Follow([FromBody] FollowRequest request)
        {
            await _followService.FollowAsync(request.FollowerId, request.FollowingId);
            return Ok("Followed successfully.");
        }

        [HttpDelete("{followerId}/{followingId}")]
        public async Task<IActionResult> Unfollow(string followerId, string followingId)
        {
            // todo: Should get followerId from claims
            await _followService.UnfollowAsync(followerId, followingId);
            return NoContent();
        }

        [HttpGet("{userId}/following")]
        public async Task<ActionResult<FollowResponse>> GetFollowing(string userId)
        {
            var followingList = await _followService.GetFollowingAsync(userId);
            return Ok(followingList);
        }

        [HttpGet("{userId}/followers")]
        public async Task<ActionResult<FollowResponse>> GetFollowers(string userId)
        {
            var followersList = await _followService.GetFollowersAsync(userId);
            return Ok(followersList);
        }
    }
}
