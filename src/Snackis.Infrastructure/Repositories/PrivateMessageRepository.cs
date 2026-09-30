using Microsoft.EntityFrameworkCore;
using Snackis.Domain.Entities;
using Snackis.Domain.Interfaces;
using Snackis.Infrastructure.Data;

namespace Snackis.Infrastructure.Repositories
{
	public class PrivateMessageRepository : IPrivateMessageRepository
	{
		private readonly SnackisDbContext _dbContext;

		public PrivateMessageRepository(SnackisDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<List<PrivateMessage>> GetAllForUserAsync(string userId)
		{
			return await _dbContext.PrivateMessages
				.Where(m => m.SenderId == userId || m.RecipientId == userId)
				.OrderByDescending(m => m.SentAt)
				.ToListAsync();
		}

		public async Task<List<PrivateMessage>> GetConversationAsync(string userId, string otherUserId)
		{
			return await _dbContext.PrivateMessages
				.Where(m =>
					(m.SenderId == userId && m.RecipientId == otherUserId) ||
					(m.SenderId == otherUserId && m.RecipientId == userId))
				.OrderBy(m => m.SentAt)
				.ToListAsync();
		}

		public async Task CreateAsync(PrivateMessage message)
		{
			_dbContext.PrivateMessages.Add(message);
			await _dbContext.SaveChangesAsync();
		}
	}
}
