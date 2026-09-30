using Microsoft.AspNetCore.Identity;
using Snackis.Infrastructure.Identity;

namespace Snackis.Presentation.Extensions
{
	public static class DemoAdminExtensions
	{
		public const string AdminRole = "Admin";
		public const string DemoAdminEmail = "admin@snackis.local";
		public const string DemoAdminPassword = "Admin123!";

		// The admin pages are admin-only, so a fresh database has no way to get a first admin.
		// In Development, create the Admin role and a demo admin so anyone who clones the repo
		// can try every feature. Nothing is created in other environments.
		public static async Task SeedDemoAdminAsync(this WebApplication app)
		{
			if (!app.Environment.IsDevelopment())
			{
				return;
			}

			using var scope = app.Services.CreateScope();
			var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
			var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

			if (!await roleManager.RoleExistsAsync(AdminRole))
			{
				await roleManager.CreateAsync(new IdentityRole(AdminRole));
			}

			var admin = await userManager.FindByEmailAsync(DemoAdminEmail);
			if (admin is null)
			{
				admin = new ApplicationUser
				{
					UserName = DemoAdminEmail,
					Email = DemoAdminEmail,
					EmailConfirmed = true,
					DisplayName = "Admin"
				};

				var result = await userManager.CreateAsync(admin, DemoAdminPassword);
				if (!result.Succeeded)
				{
					throw new InvalidOperationException(
						"Kunde inte skapa demo-admin: " + string.Join(", ", result.Errors.Select(e => e.Description)));
				}
			}

			if (!await userManager.IsInRoleAsync(admin, AdminRole))
			{
				await userManager.AddToRoleAsync(admin, AdminRole);
			}
		}
	}
}
