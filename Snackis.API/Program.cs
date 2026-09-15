using Microsoft.EntityFrameworkCore;
using Snackis.Application.Interfaces;
using Snackis.Application.Services;
using Snackis.Domain.Interfaces;
using Snackis.Infrastructure.Data;
using Snackis.Infrastructure.Identity;
using Snackis.Infrastructure.Repositories;

namespace Snackis.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
			builder.Services.AddOpenApi();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddDbContext<MyDbContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnectionString")));

			builder.Services.AddScoped<IForumPostRepository, ForumPostRepository>();
			builder.Services.AddScoped<IForumPostService, ForumPostService>();
			builder.Services.AddScoped<IUserLookupService, UserLookupService>();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.MapOpenApi();
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
