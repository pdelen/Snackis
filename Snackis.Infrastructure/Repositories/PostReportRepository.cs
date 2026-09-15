using Microsoft.EntityFrameworkCore;
using Snackis.Domain.Entities;
using Snackis.Domain.Interfaces;
using Snackis.Infrastructure.Data;

namespace Snackis.Infrastructure.Repositories
{
	public class PostReportRepository : IPostReportRepository
	{
		private readonly MyDbContext _myDbContext;

		public PostReportRepository(MyDbContext myDbContext)
		{
			_myDbContext = myDbContext;
		}

		public async Task<List<PostReport>> GetOpenReportsAsync()
		{
			return await _myDbContext.PostReports
				.Include(r => r.Post)
				.Where(r => !r.IsReviewed)
				.OrderBy(r => r.CreatedAt)
				.ToListAsync();
		}

		public async Task<PostReport?> GetOneAsync(int reportId)
		{
			return await _myDbContext.PostReports
				.Include(r => r.Post)
				.Where(r => r.Id == reportId)
				.SingleOrDefaultAsync();
		}

		public async Task CreateAsync(PostReport report)
		{
			_myDbContext.PostReports.Add(report);
			await _myDbContext.SaveChangesAsync();
		}

		public async Task UpdateAsync(PostReport report)
		{
			_myDbContext.Update(report);
			await _myDbContext.SaveChangesAsync();
		}
	}
}
