using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SimpleBlog.Domain.Interfaces;
using SimpleBlog.Infrastructure.Models;

namespace SimpleBlog.Infrastructure.Data
{
    public class UserRepository(SimpleBlogContext context, IMapper mapper, ILogger<UserRepository> logger) : IUserRepository
    {
        private readonly SimpleBlogContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<UserRepository> _logger = logger;


        // Get All
        public async Task<IEnumerable<Domain.Entities.User>> GetAllUsersAsync()
        {
            List<Domain.Entities.User> users = [];
            
            var dbUsers = await _context.Users.ToListAsync();

            dbUsers.ForEach(x => users.Add(_mapper.Map<Domain.Entities.User>(x)));

            return users;
        }

        // Get by id
        public async Task<Domain.Entities.User> GetUserById(string id)
        {
            var dbUser = await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();

            return _mapper.Map<Domain.Entities.User>(dbUser);
        }

        // Create
        public async Task CreateUserAsync(Domain.Entities.User user)
        {
            var dbUser = _mapper.Map<Models.User>(user);

            try
            {
                await _context.AddAsync(dbUser);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, message: ex.Message);
            }
        }

        // Delete
        public async Task DeleteUserAsync(Domain.Entities.User user)
        {
            var dbUser = _mapper.Map<Models.User>(user);

            try
            {
                _context.Remove(dbUser);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, message: ex.Message);
            }
        }


        // Update
        public async Task UpdateUserAsync(Domain.Entities.User user)
        {
            var dbUser = _mapper.Map<Models.User>(user);

            try
            {
                _context.Update(dbUser);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, message: ex.Message);
            }
        }
    }
}
