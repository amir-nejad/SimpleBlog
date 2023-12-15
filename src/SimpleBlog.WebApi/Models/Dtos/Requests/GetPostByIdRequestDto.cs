namespace SimpleBlog.WebApi.Models.Dtos.Requests
{
    public record GetPostByIdRequestDto(
        int Id) : BaseRequestDto
    {
    }
}
