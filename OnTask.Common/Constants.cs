using System;
using System.Diagnostics.CodeAnalysis;
using static OnTask.Common.Enumerations;

namespace OnTask.Common
{
    /// <summary>
    /// Provides constant values for the application.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class Constants
    {
        /// <summary>
        /// Gets the <see cref="DaysOfWeek"/> values that are enumerated through for time calculation logic.
        /// </summary>
        public static DaysOfWeek[] EnumeratedDaysOfWeek = new[]
        {
            DaysOfWeek.Sunday,
            DaysOfWeek.Monday,
            DaysOfWeek.Tuesday,
            DaysOfWeek.Wednesday,
            DaysOfWeek.Thursday,
            DaysOfWeek.Friday,
            DaysOfWeek.Saturday
        };
        /// <summary>
        /// Gets the minimum <see cref="TimeSpan"/> for representing clock time.
        /// </summary>
        public static TimeSpan MinimumTimeSpan = new TimeSpan(0, 0, 0);
        /// <summary>
        /// Gets the maximum <see cref="TimeSpan"/> for representing clock time.
        /// </summary>
        public static TimeSpan MaximumTimeSpan = new TimeSpan(24, 0, 0);
        /// <summary>
        /// Gets the minimum length of a password.
        /// </summary>
        public const int MinimumPasswordLength = 8;
        /// <summary>
        /// Gets the maximum length of a password.
        /// </summary>
        public const int MaximumPasswordLength = 100;
        /// <summary>
        /// Gets the mode for a delete or update operation on multiple by models by group.
        /// </summary>
        public const string ModeByGroup = "Group";
        /// <summary>
        /// Gets the mode for a delete or update operation on multiple by models by parent.
        /// </summary>
        public const string ModeByParent = "Parent";
        /// <summary>
        /// Gets the mode for a delete or update operation on multiple by models by type.
        /// </summary>
        public const string ModeByType = "Type";
        /// <summary>
        /// Gets the RuleSet name for an insert operation.
        /// </summary>
        public const string RuleSetNameForInsert = "Insert";
        /// <summary>
        /// Gets the RuleSet name for an update operation.
        /// </summary>
        public const string RuleSetNameForUpdate = "Update";
    }
}
