namespace Snackis.Domain.Entities
{
	public class ForumPost
	{
		public int Id { get; set; }
		public string Content { get; set; } = "";
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		public string AuthorId { get; set; } = "";

		public int ForumCategoryId { get; set; }
		public ForumCategory Category { get; set; } = null!;

		public List<ForumComment> Comments { get; set; } = [];
	}
}
