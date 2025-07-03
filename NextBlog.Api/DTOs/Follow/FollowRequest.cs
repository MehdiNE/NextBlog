namespace NextBlog.Api.DTOs.Follow
{
    public class FollowRequest
    {
        public required string FollowerId { get; init; }
        public required string FollowingId { get; init; }
    }
}
