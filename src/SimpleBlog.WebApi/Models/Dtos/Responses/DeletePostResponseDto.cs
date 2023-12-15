namespace SimpleBlog.WebApi.Models.Dtos.Responses
{
    public record DeletePostResponseDto(
        bool IsSuccess = false,
        string Message = null) : BaseResponseDto(IsSuccess, Message)
    {
    }
}
