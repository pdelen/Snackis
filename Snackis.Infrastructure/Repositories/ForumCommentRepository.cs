using Microsoft.EntityFrameworkCore;
using Snackis.Domain.Entities;
using Snackis.Domain.Interfaces;
using Snackis.Infrastructure.Data;

namespace Snackis.Infrastructure.Repositories
{
	public class ForumCommentRepository : IForumCommentRepository
	{
		private readonly MyDbContext _myDbContext;

		public ForumCommentRepository(MyDbContext myDbContext)
		{
			_myDbContext = myDbContext;
		}

		public async Task<List<ForumComment>> GetAllForPostAsync(int postId)
		{
			return await _myDbContext.Comments
				.Where(c => c.ForumPostId == postId)
				.OrderBy(c => c.CreatedAt)
				.ToListAsync();
		}

		public async Task<ForumComment> GetOneAsync(int commentId)
		{
			return await _myDbContext.Comments.Where(c => c.Id == commentId).SingleOrDefaultAsync();
		}

		public async Task CreateAsync(ForumComment comment)
		{
			_myDbContext.Comments.Add(comment);
			await _myDbContext.SaveChangesAsync();
		}

		public async Task DeleteAsync(ForumComment comment)
		{
			_myDbContext.Comments.Remove(comment);
			await _myDbContext.SaveChangesAsync();
		}
	}
}
