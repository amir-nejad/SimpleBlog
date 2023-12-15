namespace SimpleBlog.WebApi.Models.Dtos
{
    public record PostDto(
        int? Id = null,
        string Title = null,
        string Text = null,
        bool IsActive = false)
    {
    }
}
