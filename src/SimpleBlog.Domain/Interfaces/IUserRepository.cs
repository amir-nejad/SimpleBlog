using SimpleBlog.Domain.Entities;

namespace SimpleBlog.Domain.Interfaces
{
    /// <summary>
    /// This interface is used as a repository for <see cref="User"/> entity.
    /// </summary>
    public interface IUserRepository
    {
        Task CreateUserAsync(User post);

        Task UpdateUserAsync(User post);

        Task DeleteUserAsync(User post);
        
        Task<IEnumerable<User>> GetAllUsersAsync();

        Task<User> GetUserById(string id);
    }
}
