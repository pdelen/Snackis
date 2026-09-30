using Microsoft.EntityFrameworkCore;
using Snackis.Domain.Entities;
using Snackis.Domain.Interfaces;
using Snackis.Infrastructure.Data;

namespace Snackis.Infrastructure.Repositories
{
	public class PrivateMessageRepository : IPrivateMessageRepository
	{
		private readonly MyDbContext _myDbContext;

		public PrivateMessageRepository(MyDbContext myDbContext)
		{
			_myDbContext = myDbContext;
		}

		public async Task<List<PrivateMessage>> GetAllForUserAsync(string userId)
		{
			return await _myDbContext.PrivateMessages
				.Where(m => m.SenderId == userId || m.RecipientId == userId)
				.OrderByDescending(m => m.SentAt)
				.ToListAsync();
		}

		public async Task<List<PrivateMessage>> GetConversationAsync(string userId, string otherUserId)
		{
			return await _myDbContext.PrivateMessages
				.Where(m =>
					(m.SenderId == userId && m.RecipientId == otherUserId) ||
					(m.SenderId == otherUserId && m.RecipientId == userId))
				.OrderBy(m => m.SentAt)
				.ToListAsync();
		}

		public async Task CreateAsync(PrivateMessage message)
		{
			_myDbContext.PrivateMessages.Add(message);
			await _myDbContext.SaveChangesAsync();
		}
	}
}
