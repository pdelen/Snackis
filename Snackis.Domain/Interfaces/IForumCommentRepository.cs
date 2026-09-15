using Snackis.Domain.Entities;

namespace Snackis.Domain.Interfaces
{
	public interface IForumCommentRepository
	{
		Task CreateAsync(ForumComment comment);
		Task DeleteAsync(ForumComment comment);
		Task<List<ForumComment>> GetAllForPostAsync(int postId);
		Task<ForumComment> GetOneAsync(int commentId);
	}
}
