namespace SimpleBlog.WebApi.Models.Dtos.Responses
{
    public record GetPostByIdResponseDto(
        PostDto PostDto,
        bool IsSuccess = false,
        string Message = null) : BaseResponseDto(IsSuccess, Message)
    {
    }
}
