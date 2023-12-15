using SimpleBlog.Domain.Entities;

namespace SimpleBlog.Domain.Interfaces
{
    /// <summary>
    /// This interface is used as a repository for <see cref="UserRole"/> entity.
    /// </summary>
    public interface IUserRoleRepository
    {
        Task CreateUserRoleAsync(UserRole post);

        Task UpdateUserRoleAsync(UserRole post);

        Task DeleteUserRoleAsync(UserRole post);
        
        Task<IEnumerable<UserRole>> GetAllUserRolesAsync();

        Task<UserRole> GetUserRoleById(string id);
    }
}
