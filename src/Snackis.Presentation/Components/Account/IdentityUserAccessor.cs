using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Snackis.Infrastructure.Identity;

namespace Snackis.Presentation.Components.Account
{
	internal sealed class IdentityUserAccessor(UserManager<ApplicationUser> userManager, IdentityRedirectManager redirectManager)
	{
		public async Task<ApplicationUser> GetRequiredUserAsync(HttpContext context)
		{
			var user = await userManager.GetUserAsync(context.User);

			if (user is null)
			{
				redirectManager.RedirectToWithStatus("/", $"Fel: kunde inte hitta användare med ID '{userManager.GetUserId(context.User)}'.", context);
			}

			return user!;
		}
	}
}
