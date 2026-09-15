namespace Snackis.Domain.Entities
{
	public class PrivateMessage
	{
		public int Id { get; set; }
		public string Content { get; set; } = "";
		public DateTime SentAt { get; set; } = DateTime.UtcNow;

		public string SenderId { get; set; } = "";
		public string RecipientId { get; set; } = "";
	}
}
