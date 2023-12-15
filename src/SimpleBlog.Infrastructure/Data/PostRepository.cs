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
                dbPosts = await _context.Posts.ToListAsync();
            }
            else
            {
                dbPosts = await _context.Posts.Where(x => x.UserId == userId).ToListAsync();
            }


            dbPosts.ForEach(x => posts.Add(_mapper.Map<Domain.Entities.Post>(x)));

            return posts;
        }

        // Get by id
        public async Task<Domain.Entities.Post> GetPostById(int id, string userId = null)
        {
            Models.Post dbPost;

            if (userId is null)
            {
                dbPost = await _context.Posts.Where(x => x.Id == id).FirstOrDefaultAsync();
            }
            else
            {
                dbPost = await _context.Posts.Where(x => x.Id == id && x.UserId == userId).FirstOrDefaultAsync();
            }


            return _mapper.Map<Domain.Entities.Post>(dbPost);
        }

        // Create
        public async Task CreatePostAsync(Domain.Entities.Post post)
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
        }

        // Delete
        public async Task DeletePostAsync(Domain.Entities.Post post)
        {
            var dbPost = _mapper.Map<Models.Post>(post);

            try
            {
                _context.Remove(dbPost);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, message: ex.Message);
            }
        }


        // Update
        public async Task UpdatePostAsync(Domain.Entities.Post post)
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
            }
        }
    }
}
