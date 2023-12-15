namespace SimpleBlog.WebApi.Models.Dtos.Responses
{
    public record UpdatePostResponseDto(
        PostDto PostDto,
        bool IsSuccess = false,
        string Message = null) : BaseResponseDto(IsSuccess, Message)
    {
    }
}
