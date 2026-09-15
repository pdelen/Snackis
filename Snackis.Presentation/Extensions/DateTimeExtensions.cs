namespace Snackis.Presentation.Extensions
{
	public static class DateTimeExtensions
	{
		public static string ToDisplayString(this DateTime utcValue)
		{
			return utcValue.ToLocalTime().ToString("yyyy-MM-dd HH:mm");
		}
	}
}
