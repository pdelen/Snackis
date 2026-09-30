namespace Snackis.Domain.Entities
{
	public class PostReport
	{
		public int Id { get; set; }
		public string Reason { get; set; } = "";
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public bool IsReviewed { get; set; }

		public string ReportedByUserId { get; set; } = "";

		public int ForumPostId { get; set; }
		public ForumPost Post { get; set; } = null!;
	}
}
