using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Snackis.Infrastructure.Identity;

namespace Snackis.Presentation.Components.Account
{
	internal static class IdentityComponentsEndpointRouteBuilderExtensions
	{
		public static IEndpointConventionBuilder MapAdditionalIdentityEndpoints(this IEndpointRouteBuilder endpoints)
		{
			ArgumentNullException.ThrowIfNull(endpoints);

			var accountGroup = endpoints.MapGroup("/Account");

			accountGroup.MapPost("/Logout", async (
				ClaimsPrincipal user,
				SignInManager<ApplicationUser> signInManager,
				[FromForm] string returnUrl) =>
			{
				await signInManager.SignOutAsync();
				return TypedResults.LocalRedirect($"~/{returnUrl.TrimStart('/')}");
			});

			return accountGroup;
		}
	}
}
