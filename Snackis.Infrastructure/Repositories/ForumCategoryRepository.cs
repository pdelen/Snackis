using Microsoft.EntityFrameworkCore;
using Snackis.Domain.Entities;
using Snackis.Domain.Interfaces;
using Snackis.Infrastructure.Data;

namespace Snackis.Infrastructure.Repositories
{
	public class ForumCategoryRepository : IForumCategoryRepository
	{
		private readonly MyDbContext _myDbContext;

		public ForumCategoryRepository(MyDbContext myDbContext)
		{
			_myDbContext = myDbContext;
		}

		public async Task<List<ForumCategory>> GetAllAsync()
		{
			return await _myDbContext.Categories.Include(c => c.SubCategories).Include(c => c.Posts).ToListAsync();
		}

		public async Task<ForumCategory> GetOneAsync(int categoryId)
		{
			return await _myDbContext.Categories
				.Include(c => c.SubCategories)
				.Include(c => c.Posts)
				.Where(c => c.Id == categoryId)
				.SingleOrDefaultAsync();
		}

		public async Task CreateAsync(ForumCategory category)
		{
			_myDbContext.Categories.Add(category);
			await _myDbContext.SaveChangesAsync();
		}

		public async Task UpdateAsync(ForumCategory category)
		{
			_myDbContext.Update(category);
			await _myDbContext.SaveChangesAsync();
		}

		public async Task DeleteAsync(ForumCategory category)
		{
			_myDbContext.Categories.Remove(category);
			await _myDbContext.SaveChangesAsync();
		}
	}
}
