using Microsoft.EntityFrameworkCore;
using Snackis.Domain.Entities;
using Snackis.Domain.Interfaces;
using Snackis.Infrastructure.Data;

namespace Snackis.Infrastructure.Repositories
{
	public class ForumPostRepository : IForumPostRepository
	{
		private readonly SnackisDbContext _dbContext;

		public ForumPostRepository(SnackisDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<List<ForumPost>> GetAllAsync()
		{
			return await _dbContext.Posts.Include(p => p.Category).ToListAsync();
		}

		public async Task<ForumPost> GetOneAsync(int postId)
		{
			return await _dbContext.Posts.Include(p => p.Category).Where(p => p.Id == postId).SingleOrDefaultAsync();
		}

		public async Task<List<ForumPost>> GetAllForCategoryAsync(int categoryId)
		{
			return await _dbContext.Posts
				.Where(p => p.ForumCategoryId == categoryId)
				.OrderBy(p => p.CreatedAt)
				.ToListAsync();
		}

		public async Task CreateAsync(ForumPost post)
		{
			_dbContext.Posts.Add(post);
			await _dbContext.SaveChangesAsync();
		}

		public async Task UpdateAsync(ForumPost post)
		{
			_dbContext.Update(post);
			await _dbContext.SaveChangesAsync();
		}

		public async Task DeleteAsync(ForumPost post)
		{
			_dbContext.Posts.Remove(post);
			await _dbContext.SaveChangesAsync();
		}
	}
}
