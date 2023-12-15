using MediatR;
using SimpleBlog.Domain.Entities;
using SimpleBlog.Domain.Interfaces;

namespace SimpleBlog.Application.Handlers.CreatePost
{
    public class CreatePostRequest(Post post) : IRequest<Post>
    {
        public Post Post { get; } = post;
    }

    public class Handler(IPostRepository repository) : IRequestHandler<CreatePostRequest, Post>
    {
        private readonly IPostRepository _repository = repository;

        public async Task<Post> Handle(CreatePostRequest request, CancellationToken cancellationToken)
        {
            return await _repository.CreatePostAsync(request.Post);
        }
    }
}
