using Snackis.Domain.Entities;

namespace Snackis.Domain.Interfaces
{
	public interface IForumPostRepository
	{
		Task CreateAsync(ForumPost post);
		Task DeleteAsync(ForumPost post);
		Task<List<ForumPost>> GetAllAsync();
		Task<List<ForumPost>> GetAllForCategoryAsync(int categoryId);
		Task<ForumPost> GetOneAsync(int postId);
		Task UpdateAsync(ForumPost post);
	}
}