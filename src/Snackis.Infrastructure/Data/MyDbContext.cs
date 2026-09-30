using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Snackis.Domain.Entities;
using Snackis.Infrastructure.Identity;

namespace Snackis.Infrastructure.Data
{
	public class MyDbContext : IdentityDbContext<ApplicationUser>
	{
		public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
		{
		}

		public DbSet<ForumCategory> Categories { get; set; }
		public DbSet<ForumPost> Posts { get; set; }
		public DbSet<ForumComment> Comments { get; set; }
		public DbSet<PrivateMessage> PrivateMessages { get; set; }
		public DbSet<PostReport> PostReports { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<ForumCategory>()
				.HasOne(c => c.ParentCategory)
				.WithMany(c => c.SubCategories)
				.HasForeignKey(c => c.ParentCategoryId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
