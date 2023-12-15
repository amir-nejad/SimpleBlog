using MediatR;
using SimpleBlog.Domain.Entities;
using SimpleBlog.Domain.Interfaces;

namespace SimpleBlog.Application.Handlers.DeletePost
{
    public class DeletePostRequest(int id) : IRequest<bool>
    {
        public int Id { get; } = id;
    }

    public class Handler(IPostRepository repository) : IRequestHandler<DeletePostRequest, bool>
    {
        private readonly IPostRepository _repository = repository;

        public async Task<bool> Handle(DeletePostRequest request, CancellationToken cancellationToken)
        {
            return await _repository.DeletePostAsync(request.Id);
        }
    }
}
