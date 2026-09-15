using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;

namespace Snackis.Presentation.Components.Account
{
	internal sealed class IdentityRedirectManager(NavigationManager navigationManager)
	{
		public const string StatusCookieName = "Identity.StatusMessage";

		private static readonly CookieBuilder StatusCookieBuilder = new()
		{
			SameSite = SameSiteMode.Strict,
			HttpOnly = true,
			IsEssential = true,
			MaxAge = TimeSpan.FromSeconds(5),
		};

		[DoesNotReturn]
		public void RedirectTo(string? uri)
		{
			uri ??= "";

			if (!Uri.IsWellFormedUriString(uri, UriKind.Relative))
			{
				uri = navigationManager.ToBaseRelativePath(uri);
			}

			navigationManager.NavigateTo(uri);
			throw new InvalidOperationException($"{nameof(IdentityRedirectManager)} can only be used during static rendering.");
		}

		[DoesNotReturn]
		public void RedirectToWithStatus(string uri, string message, HttpContext context)
		{
			context.Response.Cookies.Append(StatusCookieName, message, StatusCookieBuilder.Build(context));
			RedirectTo(uri);
		}
	}
}
