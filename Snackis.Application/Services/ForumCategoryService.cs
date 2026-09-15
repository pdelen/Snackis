using Snackis.Application.Interfaces;
using Snackis.Domain.Entities;
using Snackis.Domain.Interfaces;

namespace Snackis.Application.Services
{
	public class ForumCategoryService : IForumCategoryService
	{
		private readonly IForumCategoryRepository _categoryRepository;

		public ForumCategoryService(IForumCategoryRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;
		}

		public async Task<List<ForumCategory>> GetAllCategoriesAsync()
		{
			return await _categoryRepository.GetAllAsync();
		}

		public async Task<ForumCategory> GetCategoryAsync(int categoryId)
		{
			return await _categoryRepository.GetOneAsync(categoryId);
		}

		public async Task SaveCategoryAsync(ForumCategory forumCategory)
		{
			await _categoryRepository.CreateAsync(forumCategory);
		}

		public async Task EditCategoryAsync(ForumCategory forumCategory)
		{
			await _categoryRepository.UpdateAsync(forumCategory);
		}

		public async Task DeleteCategoryAsync(int categoryId)
		{
			var forumCategory = await _categoryRepository.GetOneAsync(categoryId);
			await _categoryRepository.DeleteAsync(forumCategory);
		}
	}
}
