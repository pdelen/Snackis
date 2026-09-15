using Microsoft.AspNetCore.Identity;

namespace Snackis.Infrastructure.Identity
{
	public class ApplicationUser : IdentityUser
	{
		public string DisplayName { get; set; } = "";
		public string? ProfileImageUrl { get; set; }
	}
}
