using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Snackis.Application.Interfaces;
using Snackis.Application.Services;
using Snackis.Domain.Interfaces;
using Snackis.Infrastructure.Data;
using Snackis.Infrastructure.Identity;
using Snackis.Infrastructure.Repositories;
using Snackis.Presentation.Components;
using Snackis.Presentation.Components.Account;

namespace Snackis.Presentation
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddDbContext<MyDbContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnectionString")));

			builder.Services.AddHttpClient("SnackisApi", client =>
			{
				client.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"] ?? "http://localhost:5196");
			});

			builder.Services.AddScoped<IForumCategoryRepository, ForumCategoryRepository>();
			builder.Services.AddScoped<IForumPostRepository, ForumPostRepository>();
			builder.Services.AddScoped<IForumCommentRepository, ForumCommentRepository>();
			builder.Services.AddScoped<IPrivateMessageRepository, PrivateMessageRepository>();
			builder.Services.AddScoped<IPostReportRepository, PostReportRepository>();

			builder.Services.AddScoped<IForumCategoryService, ForumCategoryService>();
			builder.Services.AddScoped<IForumPostService, ForumPostService>();
			builder.Services.AddScoped<IForumCommentService, ForumCommentService>();
			builder.Services.AddScoped<IPrivateMessageService, PrivateMessageService>();
			builder.Services.AddScoped<IPostReportService, PostReportService>();
			builder.Services.AddScoped<IUserLookupService, UserLookupService>();

			builder.Services.AddCascadingAuthenticationState();
			builder.Services.AddScoped<IdentityUserAccessor>();
			builder.Services.AddScoped<IdentityRedirectManager>();
			builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

			builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
				.AddIdentityCookies();

			builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
				.AddRoles<IdentityRole>()
				.AddEntityFrameworkStores<MyDbContext>()
				.AddSignInManager()
				.AddDefaultTokenProviders();

			builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

			builder.Services.AddRazorComponents()
				.AddInteractiveServerComponents();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseAntiforgery();

			app.MapStaticAssets();
			app.MapRazorComponents<App>()
				.AddInteractiveServerRenderMode();

			app.MapAdditionalIdentityEndpoints();

			app.Run();
		}
	}
}
