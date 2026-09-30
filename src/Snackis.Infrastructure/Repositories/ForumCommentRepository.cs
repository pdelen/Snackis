using Microsoft.EntityFrameworkCore;
using Snackis.Domain.Entities;
using Snackis.Domain.Interfaces;
using Snackis.Infrastructure.Data;

namespace Snackis.Infrastructure.Repositories
{
	public class ForumCommentRepository : IForumCommentRepository
	{
		private readonly SnackisDbContext _dbContext;

		public ForumCommentRepository(SnackisDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<List<ForumComment>> GetAllForPostAsync(int postId)
		{
			return await _dbContext.Comments
				.Where(c => c.ForumPostId == postId)
				.OrderBy(c => c.CreatedAt)
				.ToListAsync();
		}

		public async Task<ForumComment> GetOneAsync(int commentId)
		{
			return await _dbContext.Comments.Where(c => c.Id == commentId).SingleOrDefaultAsync();
		}

		public async Task CreateAsync(ForumComment comment)
		{
			_dbContext.Comments.Add(comment);
			await _dbContext.SaveChangesAsync();
		}

		public async Task DeleteAsync(ForumComment comment)
		{
			_dbContext.Comments.Remove(comment);
			await _dbContext.SaveChangesAsync();
		}
	}
}
