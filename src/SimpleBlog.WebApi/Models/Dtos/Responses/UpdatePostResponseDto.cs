﻿namespace SimpleBlog.WebApi.Models.Dtos.Responses
{
    public record UpdatePostResponseDto(
        PostDto Post = null,
        bool IsSuccess = false,
        string Message = null) : BaseResponseDto(IsSuccess, Message)
    {
    }
}
