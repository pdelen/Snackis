using Microsoft.EntityFrameworkCore;
using Snackis.Domain.Entities;
using Snackis.Domain.Interfaces;
using Snackis.Infrastructure.Data;

namespace Snackis.Infrastructure.Repositories
{
	public class ForumPostRepository : IForumPostRepository
	{
		private readonly MyDbContext _myDbContext;

		public ForumPostRepository(MyDbContext myDbContext)
		{
			_myDbContext = myDbContext;
		}

		public async Task<List<ForumPost>> GetAllAsync()
		{
			return await _myDbContext.Posts.Include(p => p.Category).ToListAsync();
		}

		public async Task<ForumPost> GetOneAsync(int postId)
		{
			return await _myDbContext.Posts.Include(p => p.Category).Where(p => p.Id == postId).SingleOrDefaultAsync();
		}

		public async Task<List<ForumPost>> GetAllForCategoryAsync(int categoryId)
		{
			return await _myDbContext.Posts
				.Where(p => p.ForumCategoryId == categoryId)
				.OrderBy(p => p.CreatedAt)
				.ToListAsync();
		}

		public async Task CreateAsync(ForumPost post)
		{
			_myDbContext.Posts.Add(post);
			await _myDbContext.SaveChangesAsync();
		}

		public async Task UpdateAsync(ForumPost post)
		{
			_myDbContext.Update(post);
			await _myDbContext.SaveChangesAsync();
		}

		public async Task DeleteAsync(ForumPost post)
		{
			_myDbContext.Posts.Remove(post);
			await _myDbContext.SaveChangesAsync();
		}
	}
}
