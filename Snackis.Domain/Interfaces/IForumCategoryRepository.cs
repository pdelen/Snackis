using Snackis.Domain.Entities;

namespace Snackis.Domain.Interfaces
{
	public interface IForumCategoryRepository
	{
		Task CreateAsync(ForumCategory category);
		Task DeleteAsync(ForumCategory category);
		Task<List<ForumCategory>> GetAllAsync();
		Task<ForumCategory> GetOneAsync(int categoryId);
		Task UpdateAsync(ForumCategory category);
	}
}