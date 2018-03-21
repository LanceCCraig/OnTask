using System;

namespace OnTask.Common
{
    /// <summary>
    /// Provides enumeration values for the application.
    /// </summary>
    public static class Enumerations
    {
        /// <summary>
        /// Indicates the day(s) of the week.
        /// </summary>
        [Flags]
        public enum DaysOfWeek
        {
            /// <summary>
            /// Specifies that no days of the week are selected.
            /// </summary>
            None = 0,
            /// <summary>
            /// Specifies that Sunday is selected.
            /// </summary>
            Sunday = 1,
            /// <summary>
            /// Specifies that Monday is selected.
            /// </summary>
            Monday = 2,
            /// <summary>
            /// Specifies that Tuesday is selected.
            /// </summary>
            Tuesday = 4,
            /// <summary>
            /// Specifies that Wednesday is selected.
            /// </summary>
            Wednesday = 8,
            /// <summary>
            /// Specifies that Thursday is selected.
            /// </summary>
            Thursday = 16,
            /// <summary>
            /// Specifies that Friday is selected.
            /// </summary>
            Friday = 32,
            /// <summary>
            /// Specifies that Saturday is selected.
            /// </summary>
            Saturday = 64
        }
    }
}
