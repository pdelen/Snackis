namespace Snackis.Presentation.Models
{
	public class PostApiDto
	{
		public int Id { get; set; }
		public string Content { get; set; } = "";
		public string AuthorDisplayName { get; set; } = "";
		public DateTime CreatedAt { get; set; }
	}
}
