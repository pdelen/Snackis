using Snackis.Application.Interfaces;
using Snackis.Domain.Entities;
using Snackis.Domain.Interfaces;

namespace Snackis.Application.Services
{
	public class PostReportService : IPostReportService
	{
		private readonly IPostReportRepository _reportRepository;
		private readonly IForumPostRepository _postRepository;

		public PostReportService(IPostReportRepository reportRepository, IForumPostRepository postRepository)
		{
			_reportRepository = reportRepository;
			_postRepository = postRepository;
		}

		public async Task ReportPostAsync(int postId, string reportedByUserId, string reason)
		{
			await _reportRepository.CreateAsync(new PostReport
			{
				ForumPostId = postId,
				ReportedByUserId = reportedByUserId,
				Reason = reason
			});
		}

		public async Task<List<PostReport>> GetOpenReportsAsync()
		{
			return await _reportRepository.GetOpenReportsAsync();
		}

		public async Task MarkReviewedAsync(int reportId)
		{
			var report = await _reportRepository.GetOneAsync(reportId)
				?? throw new InvalidOperationException($"Hittade ingen anmälan med id {reportId}.");

			report.IsReviewed = true;
			await _reportRepository.UpdateAsync(report);
		}

		public async Task DeleteReportedPostAsync(int reportId)
		{
			var report = await _reportRepository.GetOneAsync(reportId)
				?? throw new InvalidOperationException($"Hittade ingen anmälan med id {reportId}.");

			report.IsReviewed = true;
			await _reportRepository.UpdateAsync(report);

			var post = await _postRepository.GetOneAsync(report.ForumPostId);
			if (post is not null)
			{
				await _postRepository.DeleteAsync(post);
			}
		}
	}
}
