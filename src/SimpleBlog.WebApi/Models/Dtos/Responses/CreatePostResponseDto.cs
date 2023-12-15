namespace SimpleBlog.WebApi.Models.Dtos.Responses
{
    public record CreatePostResponseDto(
        PostDto Post,
        bool IsSuccess = false,
        string Message = null) : BaseResponseDto(IsSuccess, Message)
    {
    }
}
