using Microsoft.EntityFrameworkCore;
using Snackis.Domain.Entities;
using Snackis.Domain.Interfaces;
using Snackis.Infrastructure.Data;

namespace Snackis.Infrastructure.Repositories
{
	public class ForumCategoryRepository : IForumCategoryRepository
	{
		private readonly SnackisDbContext _dbContext;

		public ForumCategoryRepository(SnackisDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<List<ForumCategory>> GetAllAsync()
		{
			return await _dbContext.Categories.Include(c => c.SubCategories).Include(c => c.Posts).ToListAsync();
		}

		public async Task<ForumCategory> GetOneAsync(int categoryId)
		{
			return await _dbContext.Categories
				.Include(c => c.SubCategories)
				.Include(c => c.Posts)
				.Where(c => c.Id == categoryId)
				.SingleOrDefaultAsync();
		}

		public async Task CreateAsync(ForumCategory category)
		{
			_dbContext.Categories.Add(category);
			await _dbContext.SaveChangesAsync();
		}

		public async Task UpdateAsync(ForumCategory category)
		{
			_dbContext.Update(category);
			await _dbContext.SaveChangesAsync();
		}

		public async Task DeleteAsync(ForumCategory category)
		{
			_dbContext.Categories.Remove(category);
			await _dbContext.SaveChangesAsync();
		}
	}
}
