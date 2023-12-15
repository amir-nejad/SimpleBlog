namespace SimpleBlog.WebApi.Models.Dtos.Requests
{
    public record UpdatePostRequestDto(
        int Id,
        string Title, 
        string Text,
        bool IsActive = true) : BaseRequestDto
    {
    }
}
