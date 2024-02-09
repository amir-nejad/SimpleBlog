using SimpleBlog.Domain.Entities;

namespace SimpleBlog.Domain.Interfaces
{
    /// <summary>
    /// This interface is used as a repository for <see cref="Post"/> entity.
    /// </summary>
    public interface IPostRepository
    {
        Task<Post> CreatePostAsync(Post post);

        Task<bool> UpdatePostAsync(Post post);

        Task<bool> DeletePostAsync(int id);
        
        Task<IEnumerable<Post>> GetAllPostsAsync(string userId = null);

        Task<Post> GetPostById(int id, string userId = null);
    }
}
