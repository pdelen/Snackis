using Microsoft.EntityFrameworkCore;
using Snackis.Application.Interfaces;
using Snackis.Infrastructure.Data;

namespace Snackis.Infrastructure.Identity
{
	public class UserLookupService : IUserLookupService
	{
		private const string UnknownUserDisplayName = "Okänd användare";

		private readonly SnackisDbContext _dbContext;

		public UserLookupService(SnackisDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<UserSummary> GetUserSummaryAsync(string userId)
		{
			var summaries = await GetUserSummariesAsync([userId]);
			return summaries.TryGetValue(userId, out var summary) ? summary : new UserSummary(UnknownUserDisplayName, null);
		}

		public async Task<Dictionary<string, UserSummary>> GetUserSummariesAsync(IEnumerable<string> userIds)
		{
			var ids = userIds.Distinct().ToList();

			return await _dbContext.Users
				.Where(u => ids.Contains(u.Id))
				.ToDictionaryAsync(u => u.Id, u => new UserSummary(u.DisplayName, u.ProfileImageUrl));
		}

		public async Task<string?> FindUserIdByDisplayNameAsync(string displayName)
		{
			return await _dbContext.Users
				.Where(u => u.DisplayName == displayName)
				.Select(u => u.Id)
				.FirstOrDefaultAsync();
		}
	}
}
