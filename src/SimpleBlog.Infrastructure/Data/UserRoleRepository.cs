using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SimpleBlog.Domain.Interfaces;
using SimpleBlog.Infrastructure.Models;

namespace SimpleBlog.Infrastructure.Data
{
    public class UserRoleRepository(SimpleBlogContext context, IMapper mapper, ILogger<UserRoleRepository> logger) : IUserRoleRepository
    {
        private readonly SimpleBlogContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<UserRoleRepository> _logger = logger;


        // Get All
        public async Task<IEnumerable<Domain.Entities.UserRole>> GetAllUserRolesAsync()
        {
            List<Domain.Entities.UserRole> userRoles = [];
            
            var dbUserRoles = await _context.UserRoles.ToListAsync();

            dbUserRoles.ForEach(x => userRoles.Add(_mapper.Map<Domain.Entities.UserRole>(x)));

            return userRoles;
        }

        // Get by id
        public async Task<Domain.Entities.UserRole> GetUserRoleById(string id)
        {
            var dbUserRole = await _context.UserRoles.Where(x => x.Id == id).FirstOrDefaultAsync();

            return _mapper.Map<Domain.Entities.UserRole>(dbUserRole);
        }

        // Create
        public async Task CreateUserRoleAsync(Domain.Entities.UserRole userRole)
        {
            var dbUserRole = _mapper.Map<Models.UserRole>(userRole);

            try
            {
                await _context.AddAsync(dbUserRole);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, message: ex.Message);
            }
        }

        // Delete
        public async Task DeleteUserRoleAsync(Domain.Entities.UserRole userRole)
        {
            var dbUserRole = _mapper.Map<Models.UserRole>(userRole);

            try
            {
                _context.Remove(dbUserRole);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, message: ex.Message);
            }
        }


        // Update
        public async Task UpdateUserRoleAsync(Domain.Entities.UserRole userRole)
        {
            var dbUserRole = _mapper.Map<Models.UserRole>(userRole);

            try
            {
                _context.Update(dbUserRole);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, message: ex.Message);
            }
        }
    }
}
