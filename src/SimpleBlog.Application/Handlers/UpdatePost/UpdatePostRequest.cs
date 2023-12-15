using MediatR;
using SimpleBlog.Domain.Entities;
using SimpleBlog.Domain.Interfaces;

namespace SimpleBlog.Application.Handlers.UpdatePost
{
    public class UpdatePostRequest(Post post) : IRequest<Post>
    {
        public Post Post { get; } = post;
    }

    public class Handler(IPostRepository repository) : IRequestHandler<UpdatePostRequest, Post>
    {
        private readonly IPostRepository _repository = repository;

        public async Task<Post> Handle(UpdatePostRequest request, CancellationToken cancellationToken)
        {
            await _repository.UpdatePostAsync(request.Post);
            return request.Post;
        }
    }
}
