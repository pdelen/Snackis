using Snackis.Application.Interfaces;
using Snackis.Domain.Entities;
using Snackis.Domain.Interfaces;

namespace Snackis.Application.Services
{
	public class PrivateMessageService : IPrivateMessageService
	{
		private readonly IPrivateMessageRepository _messageRepository;

		public PrivateMessageService(IPrivateMessageRepository messageRepository)
		{
			_messageRepository = messageRepository;
		}

		public async Task<List<PrivateMessage>> GetInboxAsync(string userId)
		{
			var all = await _messageRepository.GetAllForUserAsync(userId);

			return all
				.GroupBy(m => m.SenderId == userId ? m.RecipientId : m.SenderId)
				.Select(g => g.OrderByDescending(m => m.SentAt).First())
				.OrderByDescending(m => m.SentAt)
				.ToList();
		}

		public async Task<List<PrivateMessage>> GetConversationAsync(string userId, string otherUserId)
		{
			return await _messageRepository.GetConversationAsync(userId, otherUserId);
		}

		public async Task SendMessageAsync(PrivateMessage message)
		{
			message.SentAt = DateTime.UtcNow;
			await _messageRepository.CreateAsync(message);
		}
	}
}
