using Snackis.Application.Interfaces;
using Snackis.Domain.Entities;
using Snackis.Domain.Interfaces;

namespace Snackis.Application.Services
{
	public class ForumCommentService : IForumCommentService
	{
		private readonly IForumCommentRepository _commentRepository;

		public ForumCommentService(IForumCommentRepository commentRepository)
		{
			_commentRepository = commentRepository;
		}

		public async Task<List<ForumComment>> GetCommentsForPostAsync(int postId)
		{
			return await _commentRepository.GetAllForPostAsync(postId);
		}

		public async Task CreateCommentAsync(ForumComment comment)
		{
			comment.CreatedAt = DateTime.UtcNow;
			await _commentRepository.CreateAsync(comment);
		}

		public async Task DeleteCommentAsync(int commentId)
		{
			var comment = await _commentRepository.GetOneAsync(commentId);
			await _commentRepository.DeleteAsync(comment);
		}
	}
}
