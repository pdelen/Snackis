namespace Snackis.Domain.Entities
{
	public class ForumComment
	{
		public int Id { get; set; }
		public string Content { get; set; } = "";
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		public string AuthorId { get; set; } = "";

		public int ForumPostId { get; set; }
		public ForumPost Post { get; set; } = null!;
	}
}
