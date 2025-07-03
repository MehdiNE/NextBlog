namespace NextBlog.Api.DTOs.Follow
{
    public class FollowResponseRecord
    {
        public required string UserId { get; init; }
        public required string Name { get; init; }
    }

    public class FollowResponse
    {
        public required List<FollowResponseRecord> Items { get; init; } = [];
    }
}
