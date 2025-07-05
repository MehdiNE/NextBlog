namespace NextBlog.Api.DTOs
{
    public sealed class PaginationRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }
}
