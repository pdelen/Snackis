using Snackis.Domain.Entities;

namespace Snackis.Domain.Interfaces
{
	public interface IPrivateMessageRepository
	{
		Task CreateAsync(PrivateMessage message);
		Task<List<PrivateMessage>> GetAllForUserAsync(string userId);
		Task<List<PrivateMessage>> GetConversationAsync(string userId, string otherUserId);
	}
}
