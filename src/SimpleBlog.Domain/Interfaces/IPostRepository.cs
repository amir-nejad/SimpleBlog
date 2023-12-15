using SimpleBlog.Domain.Entities;

namespace SimpleBlog.Domain.Interfaces
{
    /// <summary>
    /// This interface is used as a repository for <see cref="Post"/> entity.
    /// </summary>
    public interface IPostRepository
    {
        Task CreatePostAsync(Post post);

        Task UpdatePostAsync(Post post);

        Task DeletePostAsync(Post post);
        
        Task<IEnumerable<Post>> GetAllPostsAsync(string userId = null);

        Task<Post> GetPostById(int id, string userId = null);
    }
}
