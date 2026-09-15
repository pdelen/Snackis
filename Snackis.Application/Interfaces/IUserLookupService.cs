namespace Snackis.Application.Interfaces
{
	public class UserSummary
	{
		public UserSummary(string displayName, string? profileImageUrl)
		{
			DisplayName = displayName;
			ProfileImageUrl = profileImageUrl;
		}

		public string DisplayName { get; }
		public string? ProfileImageUrl { get; }
	}

	public interface IUserLookupService
	{
		Task<UserSummary> GetUserSummaryAsync(string userId);
		Task<Dictionary<string, UserSummary>> GetUserSummariesAsync(IEnumerable<string> userIds);
		Task<string?> FindUserIdByDisplayNameAsync(string displayName);
	}
}
