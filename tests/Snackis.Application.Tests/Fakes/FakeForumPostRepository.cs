using Snackis.Domain.Entities;
using Snackis.Domain.Interfaces;

namespace Snackis.Application.Tests.Fakes
{
	public class FakeForumPostRepository : IForumPostRepository
	{
		public List<ForumPost> Posts { get; } = [];

		public Task CreateAsync(ForumPost post)
		{
			Posts.Add(post);
			return Task.CompletedTask;
		}

		public Task DeleteAsync(ForumPost post)
		{
			Posts.Remove(post);
			return Task.CompletedTask;
		}

		public Task<List<ForumPost>> GetAllAsync()
		{
			return Task.FromResult(Posts.ToList());
		}

		public Task<List<ForumPost>> GetAllForCategoryAsync(int categoryId)
		{
			return Task.FromResult(Posts.Where(p => p.ForumCategoryId == categoryId).ToList());
		}

		// The interface says non-null, but the real repository returns null for a missing id.
		public Task<ForumPost> GetOneAsync(int postId)
		{
			return Task.FromResult(Posts.FirstOrDefault(p => p.Id == postId)!);
		}

		public Task UpdateAsync(ForumPost post)
		{
			return Task.CompletedTask;
		}
	}
}
