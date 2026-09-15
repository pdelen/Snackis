using Snackis.Domain.Entities;

namespace Snackis.Application.Interfaces
{
	public interface IForumCommentService
	{
		Task CreateCommentAsync(ForumComment comment);
		Task DeleteCommentAsync(int commentId);
		Task<List<ForumComment>> GetCommentsForPostAsync(int postId);
	}
}
