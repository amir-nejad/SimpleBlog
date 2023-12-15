namespace SimpleBlog.WebApi.Models.Dtos.Requests
{
    public record GetPostsRequestDto(
        bool OnlyActivePosts) : BaseRequestDto
    {
    }
}
