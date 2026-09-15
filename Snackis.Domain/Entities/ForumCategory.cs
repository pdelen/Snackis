namespace Snackis.Domain.Entities
{
	public class ForumCategory
	{
		public int Id { get; set; }
		public string Name { get; set; } = "";

		public int? ParentCategoryId { get; set; }
		public ForumCategory? ParentCategory { get; set; }
		public List<ForumCategory> SubCategories { get; set; } = [];

		public List<ForumPost> Posts { get; set; } = [];
	}
}
