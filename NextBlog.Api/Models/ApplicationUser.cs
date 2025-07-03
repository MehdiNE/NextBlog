using Microsoft.AspNetCore.Identity;

namespace NextBlog.Api.Models
{
    public sealed class ApplicationUser : IdentityUser
    {
        public ICollection<Follow> Following { get; set; } = [];
        public ICollection<Follow> Follower { get; set; } = [];
    }
}
