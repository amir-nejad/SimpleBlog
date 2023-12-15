namespace SimpleBlog.WebApi.Models
{
    public abstract record BaseResponseDto(bool IsSuccess, string Message)
    {
    }
}
