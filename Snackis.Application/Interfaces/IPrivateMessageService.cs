using Snackis.Domain.Entities;

namespace Snackis.Application.Interfaces
{
	public interface IPrivateMessageService
	{
		Task<List<PrivateMessage>> GetConversationAsync(string userId, string otherUserId);
		Task<List<PrivateMessage>> GetInboxAsync(string userId);
		Task SendMessageAsync(PrivateMessage message);
	}
}
