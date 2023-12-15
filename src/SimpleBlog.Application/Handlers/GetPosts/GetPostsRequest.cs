using MediatR;
using SimpleBlog.Domain.Entities;
using SimpleBlog.Domain.Interfaces;

namespace SimpleBlog.Application.Handlers.GetPosts
{
    public class GetPostsRequest(string userId = null) : IRequest<IEnumerable<Post>>
    {
        public string UserId { get; } = userId;
    }

    public class Handler(IPostRepository repository) : IRequestHandler<GetPostsRequest, IEnumerable<Post>>
    {
        private readonly IPostRepository _repository = repository;

        public async Task<IEnumerable<Post>> Handle(GetPostsRequest request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllPostsAsync(request.UserId);
        }
    }
}
