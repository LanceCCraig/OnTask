using System;
using System.Collections.Generic;
using static OnTask.Common.Enumerations;

namespace OnTask.Common
{
    /// <summary>
    /// Provides generic extension methods.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Determines whether a target <see cref="TimeSpan"/> value is between two <see cref="TimeSpan"/> values.
        /// </summary>
        /// <param name="target">The <see cref="TimeSpan"/> value to compare with.</param>
        /// <param name="start">The left hand <see cref="TimeSpan"/> value to compare against.</param>
        /// <param name="end">The right hand <see cref="TimeSpan"/> value to compare against.</param>
        /// <returns><c>true</c> if the <paramref name="target"/> is between the two <see cref="TimeSpan"/> values; otherwise, <c>false</c>.</returns>
        public static bool Between(this TimeSpan target, TimeSpan start, TimeSpan end) =>
            target >= start &&
            target <= end;

        /// <summary>
        /// Adds a <see cref="TimeSpan"/> to a <see cref="DateTime"/>.
        /// </summary>
        /// <param name="date">The <see cref="DateTime"/> to use for the date.</param>
        /// <param name="time">The <see cref="TimeSpan"/> to use for the time.</param>
        /// <returns>A <see cref="DateTime"/> with the specified <see cref="TimeSpan"/> added.</returns>
        public static DateTime CombineTimeWithDate(this DateTime date, TimeSpan time) => date.Date.Add(time);

        /// <summary>
        /// Gets all <see cref="DateTime"/> values between a specified range.
        /// </summary>
        /// <param name="start">The start of the <see cref="DateTime"/> range.</param>
        /// <param name="end">The end of the <see cref="DateTime"/> range.</param>
        /// <returns>The <see cref="DateTime"/> values between the specified range.</returns>
        public static IEnumerable<DateTime> GetDateRange(DateTime start, DateTime end)
        {
            for (var day = start.Date; day.Date <= end.Date; day = day.AddDays(1))
            {
                yield return day;
            }
        }

        /// <summary>
        /// Gets the <see cref="DaysOfWeek"/> value from the <see cref="IEnumerable{T}"/> of texts.
        /// </summary>
        /// <param name="daysOfWeekTexts">The <see cref="DaysOfWeek"/> text values to parse.</param>
        /// <returns>The corresponding <see cref="DaysOfWeek"/> value.</returns>
        public static DaysOfWeek GetDaysOfWeek(this IEnumerable<string> daysOfWeekTexts)
        {
            var daysOfWeek = DaysOfWeek.None;
            foreach (var daysOfWeekText in daysOfWeekTexts)
            {
                if (Enum.TryParse(daysOfWeekText, out DaysOfWeek result))
                {
                    daysOfWeek |= result;
                }
            }
            return daysOfWeek;
        }

        /// <summary>
        /// Gets the <see cref="DaysOfWeek"/> value for the specified <see cref="DateTime"/>.
        /// </summary>
        /// <param name="date">The <see cref="DateTime"/> to base the return value off of.</param>
        /// <returns>The corresponding <see cref="DaysOfWeek"/> value.</returns>
        public static DaysOfWeek GetDaysOfWeek(this DateTime date)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return DaysOfWeek.Sunday;
                case DayOfWeek.Monday:
                    return DaysOfWeek.Monday;
                case DayOfWeek.Tuesday:
                    return DaysOfWeek.Tuesday;
                case DayOfWeek.Wednesday:
                    return DaysOfWeek.Wednesday;
                case DayOfWeek.Thursday:
                    return DaysOfWeek.Thursday;
                case DayOfWeek.Friday:
                    return DaysOfWeek.Friday;
                case DayOfWeek.Saturday:
                    return DaysOfWeek.Saturday;
            }
            throw new ArgumentException($"Could not determine the {nameof(DaysOfWeek)} value for '{date.ToString()}'.");
        }

        /// <summary>
        /// Determines whether the two parameters are equal or if <paramref name="y"/> is <c>null</c>.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> of the two parameters.</typeparam>
        /// <param name="x">The first parameter to compare to.</param>
        /// <param name="y">The second parameter to compare against.</param>
        /// <returns><c>true</c> if the parameters are equal or if <paramref name="y"/> is <c>null</c>; otherwise, <c>false</c>.</returns>
        public static bool IsParameterNullOrEqual<T>(this T x, T y) =>
            y == null ||
            (x != null && x.Equals(y));

        /// <summary>
        /// Determines whether the two parameters are equal or if <paramref name="y"/> is <c>null</c> for a non-nullable <paramref name="x"/>.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> of the two parameters.</typeparam>
        /// <param name="x">The first parameter to compare to.</param>
        /// <param name="y">The second parameter to compare against.</param>
        /// <returns><c>true</c> if the parameters are equal or if <paramref name="y"/> is <c>null</c>; otherwise, <c>false</c>.</returns>
        public static bool IsParameterNullOrEqualForNonNullable<T>(this T x, T? y) where T : struct =>
            y == null ||
            x.Equals(y);
    }
}
