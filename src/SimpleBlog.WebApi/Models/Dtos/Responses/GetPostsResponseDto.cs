namespace SimpleBlog.WebApi.Models.Dtos.Responses
{
    public record GetPostsResponseDto(
        List<PostDto> Posts = null,
        bool IsSuccess = false,
        string Message = null) : BaseResponseDto(IsSuccess, Message)
    {
    }
}
