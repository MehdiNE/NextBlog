using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NextBlog.Api.Database;
using NextBlog.Api.DTOs.Auth;
using NextBlog.Api.Models;
using NextBlog.Api.Services;
using NextBlog.Api.Settings;

namespace NextBlog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly TokenProvider _tokenProvider;
        private readonly JwtAuthOptions _jwtAuthOptions;
        private readonly ApplicationDbContext _context;
        public AuthController(UserManager<IdentityUser> userManager, TokenProvider tokenProvider, IOptions<JwtAuthOptions> options, ApplicationDbContext context)
        {
            _userManager = userManager;
            _tokenProvider = tokenProvider;
            _jwtAuthOptions = options.Value;
            _context = context;
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

            var refreshToken = new RefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                ExpiresAtUtc = DateTime.UtcNow.AddDays(_jwtAuthOptions.RefreshTokenExpirationInDays),
                Token = accessToken.RefreshToken,
            };

            _context.RefreshToken.Add(refreshToken);
            await _context.SaveChangesAsync();

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

            var refreshToken = new RefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                ExpiresAtUtc = DateTime.UtcNow.AddDays(_jwtAuthOptions.RefreshTokenExpirationInDays),
                Token = accessToken.RefreshToken,
            };

            _context.RefreshToken.Add(refreshToken);
            await _context.SaveChangesAsync();

            return Ok(accessToken);
        }

        [HttpPost("refresh")]
        public async Task<ActionResult<AccessTokenDto>> Refresh([FromBody] RefreshTokenDto refreshTokenDto)
        {
            var refreshToken = await _context.RefreshToken
                .Include(rt => rt.User)
                .FirstOrDefaultAsync(rt => rt.Token == refreshTokenDto.RefreshToken);

            if (refreshToken is null)
            {
                return Unauthorized();
            }

            if (refreshToken.ExpiresAtUtc < DateTime.UtcNow)
            {
                return Unauthorized();
            }

            var tokenRequest = new TokenRequest(refreshToken.User.Id, refreshToken.User.Email!);
            var accessToken = _tokenProvider.Create(tokenRequest);

            refreshToken.Token = accessToken.RefreshToken;
            refreshToken.ExpiresAtUtc = DateTime.UtcNow.AddDays(_jwtAuthOptions.RefreshTokenExpirationInDays);

            await _context.SaveChangesAsync();

            return Ok(accessToken);

        }
    }
}
