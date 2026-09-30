using Snackis.Domain.Entities;

namespace Snackis.Application.Interfaces
{
	public interface IPostReportService
	{
		Task DeleteReportedPostAsync(int reportId);
		Task<List<PostReport>> GetOpenReportsAsync();
		Task MarkReviewedAsync(int reportId);
		Task ReportPostAsync(int postId, string reportedByUserId, string reason);
	}
}
