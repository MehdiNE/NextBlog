namespace NextBlog.Api.Models
{
    public class Follow
    {
        public required string FollowerId { get; set; }
        public ApplicationUser? Follower { get; set; }

        public required string FollowingId { get; set; }
        public ApplicationUser? Following { get; set; }

        public DateTime FollowDate { get; set; }
    }
}
