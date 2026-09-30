using Snackis.Domain.Entities;

namespace Snackis.Domain.Interfaces
{
	public interface IPostReportRepository
	{
		Task CreateAsync(PostReport report);
		Task<List<PostReport>> GetOpenReportsAsync();
		Task<PostReport?> GetOneAsync(int reportId);
		Task UpdateAsync(PostReport report);
	}
}
