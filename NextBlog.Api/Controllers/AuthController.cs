using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NextBlog.Api.DTOs.Auth;
using NextBlog.Api.Services;

namespace NextBlog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly TokenProvider _tokenProvider;
        public AuthController(UserManager<IdentityUser> userManager, TokenProvider tokenProvider)
        {
            _userManager = userManager;
            _tokenProvider = tokenProvider;
        }

        /// <summary>
        /// Test
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<ActionResult<AccessTokenDto>> Register([FromBody] RegisterRequest request)
        {
            var user = new IdentityUser
            {
                UserName = request.Name,
                Email = request.Email,
            };

            var registerResult = await _userManager.CreateAsync(user, request.Password);

            if (!registerResult.Succeeded)
            {
                return BadRequest(registerResult);
            }

            var tokenRequest = new TokenRequest(user.Id, user.Email);
            var accessToken = _tokenProvider.Create(tokenRequest);

            return Ok(accessToken);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AccessTokenDto>> Login([FromBody] LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                return Unauthorized();
            }

            var tokenRequest = new TokenRequest(user.Id, user.Email!);
            var accessToken = _tokenProvider.Create(tokenRequest);

            return Ok(accessToken);
        }
    }
}
