using Microsoft.EntityFrameworkCore;
using Snackis.Domain.Entities;
using Snackis.Domain.Interfaces;
using Snackis.Infrastructure.Data;

namespace Snackis.Infrastructure.Repositories
{
	public class PostReportRepository : IPostReportRepository
	{
		private readonly SnackisDbContext _dbContext;

		public PostReportRepository(SnackisDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<List<PostReport>> GetOpenReportsAsync()
		{
			return await _dbContext.PostReports
				.Include(r => r.Post)
				.Where(r => !r.IsReviewed)
				.OrderBy(r => r.CreatedAt)
				.ToListAsync();
		}

		public async Task<PostReport?> GetOneAsync(int reportId)
		{
			return await _dbContext.PostReports
				.Include(r => r.Post)
				.Where(r => r.Id == reportId)
				.SingleOrDefaultAsync();
		}

		public async Task CreateAsync(PostReport report)
		{
			_dbContext.PostReports.Add(report);
			await _dbContext.SaveChangesAsync();
		}

		public async Task UpdateAsync(PostReport report)
		{
			_dbContext.Update(report);
			await _dbContext.SaveChangesAsync();
		}
	}
}
