using Microsoft.EntityFrameworkCore;
using Post.Query.Domain.Entities;
using Post.Query.Domain.Repositories;
using Post.Query.Infrastructure.DataAccess;

namespace Post.Query.Infrastructure.Repositories
{
    public class PostRepository(DatabaseContextFactory contextFactory) : IPostRepository
    {
        private readonly DatabaseContextFactory _contextFactory = contextFactory;

        public async Task CreateAsync(PostEntity post)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            context.Posts.Add(post);
            _ = await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid postId)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            var post = await GetByIdAsync(postId);

            if (post is null)
                return;

            context.Posts.Remove(post);
            _  = await context.SaveChangesAsync();
        }

        public async Task<PostEntity> GetByIdAsync(Guid postId)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            return await context.Posts
            .Include(p => p.Comments)
            .FirstOrDefaultAsync(x => x.PostId.Equals(postId));
        }

        public async Task<List<PostEntity>> ListAllAsync()
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            return await context.Posts
            .AsNoTracking()
            .Include(p => p.Comments)
            .ToListAsync();
        }

        public async Task<List<PostEntity>> ListByAuthorAsync(string author)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            return await context.Posts
            .AsNoTracking()
            .Include(p => p.Comments)
            .Where(x => x.Author.Contains(author))
            .ToListAsync();
        }

        public async Task<List<PostEntity>> ListWithCommentAsync()
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            return await context.Posts
            .AsNoTracking()
            .Include(p => p.Comments)
            .Where(x => x.Comments != null && x.Comments.Count > 0)
            .ToListAsync();
        }

        public async Task<List<PostEntity>> ListWithLikesAsync(int numberOfLikes)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            return await context.Posts.AsNoTracking()
            .Include(p => p.Comments)
            .Where(x => x.Likes >= numberOfLikes)
            .ToListAsync();
        }

        public async Task UpdateAsync(PostEntity post)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            context.Posts.Update(post);
            _ = await context.SaveChangesAsync();
        }
    }
}