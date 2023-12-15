using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleBlog.Application.Handlers.CreatePost;
using SimpleBlog.Application.Handlers.DeletePost;
using SimpleBlog.Application.Handlers.GetPostById;
using SimpleBlog.Application.Handlers.GetPosts;
using SimpleBlog.Application.Handlers.UpdatePost;
using SimpleBlog.Domain.Entities;
using SimpleBlog.WebApi.Models.Dtos;
using SimpleBlog.WebApi.Models.Dtos.Requests;
using SimpleBlog.WebApi.Models.Dtos.Responses;
using SimpleBlog.WebApi.Utilities;
using System.Security.Claims;

namespace SimpleBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = ConfigConstants.RequireApiScope)]
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

        [HttpGet("{id?}")]
        public async Task<GetPostByIdResponseDto> GetPostByIdAsync(int? id)
        {
            GetPostByIdResponseDto response = new();

            if (id is null)
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;

                response = response with
                {
                    IsSuccess = false,
                    Message = "Bad Request!"
                };

                return response;
            }

            // Check if user is an admin
            string userId = _contextAccessor.HttpContext.User.FindFirstValue("sub");
            bool isAdmin = false;
            var userRole = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);

            if (userRole is not null && userRole == CustomRoles.Administrator)
                isAdmin = true;

            if (isAdmin)
                userId = "";

            Post post = await _mediator.Send(new GetPostByIdRequest(id.Value, userId));

            if (post is null)
            {
                Response.StatusCode = StatusCodes.Status404NotFound;

                response = response with
                {
                    IsSuccess = false,
                    Message = "Post not found."
                };

                return response;
            }

            return response with
            {
                IsSuccess = true,
                Post = new Models.Dtos.PostDto(Id: post.Id, Title: post.Title, Text: post.Text, IsActive: post.IsActive)
            };
        }

        [HttpGet]
        public async Task<GetPostsResponseDto> GetPostsAsync()
        {
            GetPostsResponseDto response = new();

            // Check if user is an admin
            string userId = _contextAccessor.HttpContext.User.FindFirstValue("sub");
            bool isAdmin = false;
            var userRole = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);

            if (userRole is not null && userRole == CustomRoles.Administrator)
                isAdmin = true;

            if (isAdmin)
                userId = "";

            var posts = await _mediator.Send(new GetPostsRequest(userId));

            if (posts is null || posts.Count() == 0)
            {
                return response with
                {
                    IsSuccess = true,
                    Message = "You don't have any post yet."
                };
            }

            List<PostDto> postDtos = [];

            foreach (var post in posts)
            {
                var postDto = new PostDto(post.Id, post.Title, post.Text, post.IsActive);

                postDtos.Add(postDto);
            }

            return response with
            {
                IsSuccess = true,
                Posts = postDtos
            };
        }

        [HttpPost]
        public async Task<CreatePostResponseDto> CreatePostAsync([FromBody] CreatePostRequestDto createPostRequestDto)
        {
            CreatePostResponseDto response = new();

            var post = new Post()
            {
                Title = createPostRequestDto.Title,
                Text = createPostRequestDto.Text,
                UserId = _contextAccessor.HttpContext.User.FindFirstValue("sub")
            };

            post = await _mediator.Send(new CreatePostRequest(post));

            if (post is null)
            {
                response = response with
                {
                    IsSuccess = false,
                    Message = "Post creation was failed."
                };

                return response;
            }

            return response with
            {
                IsSuccess = true,
                Message = "The Post has been created successfully.",
                Post = new Models.Dtos.PostDto(Id: post.Id, Title: post.Title, Text: post.Text, IsActive: post.IsActive)
            };
        }

        [HttpPut("{id?}")]
        [Authorize(Policy = ConfigConstants.RequireAdministratorRole)]
        public async Task<UpdatePostResponseDto> DisablePostAsync(int? id)
        {
            UpdatePostResponseDto response = new();

            if (id is null)
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;

                response = response with
                {
                    IsSuccess = false,
                    Message = "Bad Request!"
                };

                return response;
            }

            Post post = await _mediator.Send(new GetPostByIdRequest(id.Value));

            if (post is null)
            {
                Response.StatusCode = StatusCodes.Status404NotFound;

                response = response with
                {
                    IsSuccess = false,
                    Message = "Post not found."
                };

                return response;
            }

            post = await _mediator.Send(new UpdatePostRequest(post));

            if (post is null)
            {
                response = response with
                {
                    IsSuccess = false,
                    Message = "Post update process was failed."
                };

                return response;
            }

            return response with
            {
                IsSuccess = true,
                Message = "The Post has been updated successfully.",
                Post = new Models.Dtos.PostDto(Id: post.Id, Title: post.Title, Text: post.Text, IsActive: post.IsActive)
            };

            return response;
        }

        [HttpPut]
        public async Task<UpdatePostResponseDto> UpdatePostAsync([FromBody] UpdatePostRequestDto updatePostRequestDto)
        {
            UpdatePostResponseDto response = new();

            Post post = await _mediator.Send(new GetPostByIdRequest(updatePostRequestDto.Id));

            if (post is null)
            {
                Response.StatusCode = StatusCodes.Status404NotFound;

                response = response with
                {
                    IsSuccess = false,
                    Message = "Post not found."
                };

                return response;
            }

            // Check if user is an admin
            string userId = _contextAccessor.HttpContext.User.FindFirstValue("sub");
            bool isAdmin = false;
            var userRole = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);

            if (userRole is not null && userRole == CustomRoles.Administrator)
                isAdmin = true;

            if (!isAdmin && userId != post.UserId)
            {
                Response.StatusCode = StatusCodes.Status403Forbidden;

                return response with
                {
                    IsSuccess = false,
                    Message = "Access Denied"
                };
            }


            post.Title = updatePostRequestDto.Title;
            post.Text = updatePostRequestDto.Text;
            post.UpdatedDate = DateTime.UtcNow;

            post = await _mediator.Send(new UpdatePostRequest(post));

            if (post is null)
            {
                response = response with
                {
                    IsSuccess = false,
                    Message = "Post update process was failed."
                };

                return response;
            }

            return response with
            {
                IsSuccess = true,
                Message = "The Post has been updated successfully.",
                Post = new Models.Dtos.PostDto(Id: post.Id, Title: post.Title, Text: post.Text, IsActive: post.IsActive)
            };
        }

        [HttpDelete]
        public async Task<DeletePostResponseDto> DeletePostAsync([FromBody] DeletePostRequestDto deletePostRequestDto)
        {
            DeletePostResponseDto response = new();

            Post post = await _mediator.Send(new GetPostByIdRequest(deletePostRequestDto.Id));

            if (post is null)
            {
                Response.StatusCode = StatusCodes.Status404NotFound;

                response = response with
                {
                    IsSuccess = false,
                    Message = "Post not found."
                };

                return response;
            }

            // Check if user is an admin
            string userId = _contextAccessor.HttpContext.User.FindFirstValue("sub");
            bool isAdmin = false;
            var userRole = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);

            if (userRole is not null && userRole == CustomRoles.Administrator)
                isAdmin = true;

            if (!isAdmin && userId != post.UserId)
            {
                Response.StatusCode = StatusCodes.Status403Forbidden;

                return response with
                {
                    IsSuccess = false,
                    Message = "Access Denied"
                };
            }

            var isSucceded = await _mediator.Send(new DeletePostRequest(deletePostRequestDto.Id));

            if (!isSucceded)
            {
                response = response with
                {
                    IsSuccess = false,
                    Message = "Post delete process was failed."
                };

                return response;
            }

            return response with
            {
                IsSuccess = true,
                Message = "The Post has been deleted successfully."
            };
        }
    }
}
