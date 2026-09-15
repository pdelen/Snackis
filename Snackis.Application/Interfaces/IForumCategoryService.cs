using Snackis.Domain.Entities;

namespace Snackis.Application.Interfaces
{
	public interface IForumCategoryService
	{
		Task DeleteCategoryAsync(int categoryId);
		Task EditCategoryAsync(ForumCategory forumCategory);
		Task<List<ForumCategory>> GetAllCategoriesAsync();
		Task<ForumCategory> GetCategoryAsync(int categoryId);
		Task SaveCategoryAsync(ForumCategory forumCategory);
	}
}
