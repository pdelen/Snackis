using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Snackis.Infrastructure.Data
{
	public static class SnackisDatabase
	{
		// SQLite resolves a relative Data Source against the process's current directory, which
		// differs between a terminal and an IDE. Resolving it against the project folder makes the
		// web app and the API share one database file wherever they are started from.
		public static void UseSnackisSqlite(this DbContextOptionsBuilder options, string? connectionString, string contentRootPath)
		{
			var builder = new SqliteConnectionStringBuilder(connectionString);
			builder.DataSource = Path.GetFullPath(builder.DataSource, contentRootPath);
			options.UseSqlite(builder.ToString());
		}

		// Creates the database on first run and applies any new migrations.
		public static void MigrateSnackisDatabase(this IServiceProvider services)
		{
			using var scope = services.CreateScope();
			scope.ServiceProvider.GetRequiredService<SnackisDbContext>().Database.Migrate();
		}
	}
}
