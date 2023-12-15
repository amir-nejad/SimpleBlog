using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleBlog.WebApi.Models.Dtos.Requests;
using SimpleBlog.WebApi.Models.Dtos.Responses;
using System.Security.Claims;

namespace SimpleBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ILogger<PostsController> _logger;

        public PostsController(IMediator mediator, IHttpContextAccessor contextAccessor, ILogger<PostsController> logger)
        {
            _mediator = mediator;
            _contextAccessor = contextAccessor;
            _logger = logger;
        }

        [HttpPost]
        public async Task<CreatePostResponseDto> CreatePostAsync([FromBody] CreatePostRequestDto createPostRequestDto)
        {
            CreatePostResponseDto response = new();

            

            return response;
        }
    }
}
