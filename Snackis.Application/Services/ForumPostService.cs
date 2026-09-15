using Snackis.Application.Interfaces;
using Snackis.Domain.Entities;
using Snackis.Domain.Interfaces;

namespace Snackis.Application.Services
{
	public class ForumPostService : IForumPostService
	{
		private readonly IForumPostRepository _postRepository;

		public ForumPostService(IForumPostRepository postRepository)
		{
			_postRepository = postRepository;
		}

		public async Task<List<ForumPost>> GetPostsForCategoryAsync(int categoryId)
		{
			return await _postRepository.GetAllForCategoryAsync(categoryId);
		}

		public async Task<ForumPost> GetPostAsync(int postId)
		{
			return await _postRepository.GetOneAsync(postId);
		}

		public async Task CreatePostAsync(ForumPost post)
		{
			post.CreatedAt = DateTime.UtcNow;
			await _postRepository.CreateAsync(post);
		}

		public async Task DeletePostAsync(int postId)
		{
			var post = await _postRepository.GetOneAsync(postId);
			await _postRepository.DeleteAsync(post);
		}
	}
}
