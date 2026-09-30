using Snackis.Application.Services;
using Snackis.Application.Tests.Fakes;
using Snackis.Domain.Entities;

namespace Snackis.Application.Tests
{
	public class PostReportServiceTests
	{
		private readonly FakePostReportRepository _reports = new();
		private readonly FakeForumPostRepository _posts = new();
		private readonly PostReportService _service;

		public PostReportServiceTests()
		{
			_service = new PostReportService(_reports, _posts);
		}

		[Fact]
		public async Task ReportPostAsync_CreatesOpenReportForThePost()
		{
			await _service.ReportPostAsync(postId: 7, reportedByUserId: "user-1", reason: "Spam");

			var report = Assert.Single(_reports.Reports);
			Assert.Equal(7, report.ForumPostId);
			Assert.Equal("user-1", report.ReportedByUserId);
			Assert.Equal("Spam", report.Reason);
			Assert.False(report.IsReviewed);
		}

		[Fact]
		public async Task MarkReviewedAsync_MarksTheReportAsReviewed()
		{
			var report = new PostReport { Id = 1, ForumPostId = 7 };
			_reports.Reports.Add(report);

			await _service.MarkReviewedAsync(1);

			Assert.True(report.IsReviewed);
		}

		[Fact]
		public async Task MarkReviewedAsync_Throws_WhenReportDoesNotExist()
		{
			await Assert.ThrowsAsync<InvalidOperationException>(() => _service.MarkReviewedAsync(99));
		}

		[Fact]
		public async Task DeleteReportedPostAsync_DeletesThePostAndMarksTheReportAsReviewed()
		{
			var post = new ForumPost { Id = 7 };
			_posts.Posts.Add(post);
			var report = new PostReport { Id = 1, ForumPostId = 7 };
			_reports.Reports.Add(report);

			await _service.DeleteReportedPostAsync(1);

			Assert.DoesNotContain(post, _posts.Posts);
			Assert.True(report.IsReviewed);
		}

		[Fact]
		public async Task DeleteReportedPostAsync_StillMarksReviewed_WhenThePostIsAlreadyGone()
		{
			var report = new PostReport { Id = 1, ForumPostId = 7 };
			_reports.Reports.Add(report);

			await _service.DeleteReportedPostAsync(1);

			Assert.True(report.IsReviewed);
		}

		[Fact]
		public async Task DeleteReportedPostAsync_Throws_WhenReportDoesNotExist()
		{
			await Assert.ThrowsAsync<InvalidOperationException>(() => _service.DeleteReportedPostAsync(99));
		}
	}
}
