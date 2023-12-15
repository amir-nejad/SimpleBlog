using MediatR;
using SimpleBlog.Domain.Entities;
using SimpleBlog.Domain.Interfaces;

namespace SimpleBlog.Application.Handlers.GetPostById
{
    public class GetPostByIdRequest(int id, string userId = null) : IRequest<Post>
    {
        public int Id { get; } = id;

        public string UserId { get; } = userId;
    }

    public class Handler(IPostRepository repository) : IRequestHandler<GetPostByIdRequest, Post>
    {
        private readonly IPostRepository _repository = repository;

        public async Task<Post> Handle(GetPostByIdRequest request, CancellationToken cancellationToken)
        {
            return await _repository.GetPostById(request.Id, request.UserId);
        }
    }
}
