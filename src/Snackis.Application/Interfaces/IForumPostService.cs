using Snackis.Domain.Entities;

namespace Snackis.Application.Interfaces
{
	public interface IForumPostService
	{
		Task CreatePostAsync(ForumPost post);
		Task DeletePostAsync(int postId);
		Task<ForumPost> GetPostAsync(int postId);
		Task<List<ForumPost>> GetPostsForCategoryAsync(int categoryId);
	}
}
