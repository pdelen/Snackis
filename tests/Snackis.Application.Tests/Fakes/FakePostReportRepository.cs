using Snackis.Domain.Entities;
using Snackis.Domain.Interfaces;

namespace Snackis.Application.Tests.Fakes
{
	public class FakePostReportRepository : IPostReportRepository
	{
		public List<PostReport> Reports { get; } = [];

		public Task CreateAsync(PostReport report)
		{
			report.Id = Reports.Count + 1;
			Reports.Add(report);
			return Task.CompletedTask;
		}

		public Task<List<PostReport>> GetOpenReportsAsync()
		{
			return Task.FromResult(Reports.Where(r => !r.IsReviewed).ToList());
		}

		public Task<PostReport?> GetOneAsync(int reportId)
		{
			return Task.FromResult(Reports.FirstOrDefault(r => r.Id == reportId));
		}

		public Task UpdateAsync(PostReport report)
		{
			return Task.CompletedTask;
		}
	}
}
