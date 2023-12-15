namespace SimpleBlog.WebApi.Models.Dtos.Responses
{
    public record GetPostsResponseDto(
        List<PostDto> Posts,
        bool IsSuccess = false,
        string Message = null) : BaseResponseDto(IsSuccess, Message)
    {
    }
}
