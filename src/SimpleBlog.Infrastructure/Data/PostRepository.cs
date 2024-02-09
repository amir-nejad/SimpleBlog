using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SimpleBlog.Domain.Interfaces;
using SimpleBlog.Infrastructure.Models;

namespace SimpleBlog.Infrastructure.Data
{
    public class PostRepository(SimpleBlogContext context, IMapper mapper, ILogger<PostRepository> logger) : IPostRepository
    {
        private readonly SimpleBlogContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<PostRepository> _logger = logger;


        // Get All
        public async Task<IEnumerable<Domain.Entities.Post>> GetAllPostsAsync(string userId = null)
        {
            List<Domain.Entities.Post> posts = [];
            List<Models.Post> dbPosts;

            if (userId is null)
            {
                dbPosts = await _context
                    .Posts
                    .AsNoTracking()
                    .ToListAsync();
            }
            else
            {
                dbPosts = await _context
                    .Posts
                    .Where(x => x.UserId == userId)
                    .AsNoTracking()
                    .ToListAsync();
            }


            dbPosts
                .ForEach(x => 
                    posts
                        .Add(_mapper.Map<Domain.Entities.Post>(x)));

            return posts;
        }

        // Get by id
        public async Task<Domain.Entities.Post> GetPostById(int id, string userId = null)
        {
            Models.Post dbPost;

            if (userId is null)
            {
                dbPost = await _context
                    .Posts
                    .Where(x => x.Id == id)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();
            }
            else
            {
                dbPost = await _context
                    .Posts
                    .Where(x => x.Id == id && x.UserId == userId)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();
            }


            return _mapper.Map<Domain.Entities.Post>(dbPost);
        }

        // Create
        public async Task<Domain.Entities.Post> CreatePostAsync(Domain.Entities.Post post)
        {
            var dbPost = _mapper.Map<Models.Post>(post);

            try
            {
                await _context.AddAsync(dbPost);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, message: ex.Message);
            }

            return _mapper.Map<Domain.Entities.Post>(dbPost);
        }

        // Delete
        public async Task<bool> DeletePostAsync(int id)
        {
            try
            {
                var dbPost = await _context
                    .Posts
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync() ?? throw new Exception($"The Post with id {id} was not found");
               
                _context.Remove(dbPost);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, message: ex.Message);
                return false;
            }

            return true;
        }


        // Update
        public async Task<bool> UpdatePostAsync(Domain.Entities.Post post)
        {
            var dbPost = _mapper.Map<Models.Post>(post);

            try
            {
                _context.Update(dbPost);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, message: ex.Message);
                return false;
            }

            return true;
        }
    }
}
