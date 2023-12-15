namespace SimpleBlog.WebApi.Models.Dtos.Responses
{
    public record GetPostByIdResponseDto(
        PostDto Post = null,
        bool IsSuccess = false,
        string Message = null) : BaseResponseDto(IsSuccess, Message)
    {
    }
}
