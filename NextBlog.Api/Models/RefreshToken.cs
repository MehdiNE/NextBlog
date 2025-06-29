
using Microsoft.AspNetCore.Identity;

namespace NextBlog.Api.Models
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public required string UserId { get; set; }
        public required string Token { get; set; }
        public required DateTime ExpiresAtUtc { get; set; }

        public IdentityUser User { get; set; }
    }
}
