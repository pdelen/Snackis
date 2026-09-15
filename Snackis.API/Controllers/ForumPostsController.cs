using Microsoft.AspNetCore.Mvc;
using Snackis.API.Models;
using Snackis.Application.Interfaces;

namespace Snackis.API.Controllers
{
	[ApiController]
	[Route("api/categories/{categoryId}/posts")]
	public class ForumPostsController : ControllerBase
	{
		private readonly IForumPostService _postService;
		private readonly IUserLookupService _userLookupService;

		public ForumPostsController(IForumPostService postService, IUserLookupService userLookupService)
		{
			_postService = postService;
			_userLookupService = userLookupService;
		}

		[HttpGet]
		public async Task<ActionResult<List<PostDto>>> GetPostsForCategory(int categoryId)
		{
			var posts = await _postService.GetPostsForCategoryAsync(categoryId);
			var authorIds = posts.Select(p => p.AuthorId);
			var authors = await _userLookupService.GetUserSummariesAsync(authorIds);

			var result = posts.Select(post => new PostDto
			{
				Id = post.Id,
				Content = post.Content,
				AuthorDisplayName = authors.TryGetValue(post.AuthorId, out var author) ? author.DisplayName : "Okänd användare",
				CreatedAt = post.CreatedAt
			}).ToList();

			return Ok(result);
		}
	}
}
