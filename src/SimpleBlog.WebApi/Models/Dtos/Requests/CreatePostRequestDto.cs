namespace SimpleBlog.WebApi.Models.Dtos.Requests
{
    public record CreatePostRequestDto(
        string Title, 
        string Text) : BaseRequestDto
    {
    }
}
