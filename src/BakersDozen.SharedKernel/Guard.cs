using System;

namespace BakersDozen.SharedKernel
{
	/// <summary>
	///     Represents a date range.
	///     Implementation taken from Steve Ardalis and Julie Lerman's Pluralsight course on DDD
	///     https://app.pluralsight.com/library/courses/domain-driven-design-fundamentals/table-of-contents.
	/// </summary>
	public static class Guard
	{
		/// <summary>
		///     Check to see if one date precedes another.
		/// </summary>
		/// <param name="value">The value to check.</param>
		/// <param name="dateToPrecede">The date to check for precedence.</param>
		/// <param name="parameterName">The name of the invalid parameter.</param>
		public static void ForPrecedesDate(
			DateTime value,
			DateTime dateToPrecede,
			string parameterName)
		{
			if (value >= dateToPrecede)
			{
				throw new ArgumentOutOfRangeException(parameterName);
			}
		}

		public static void AgainstNullEmpty(
			string input,
			string parameterName)
		{
			if (string.IsNullOrEmpty(input))
			{
				throw new ArgumentNullException(
					nameof(parameterName),
					"Value cannot be null");
			}
		}
	}
}