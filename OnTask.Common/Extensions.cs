using System;

namespace OnTask.Common
{
    /// <summary>
    /// Provides generic extension methods.
    /// </summary>
    public static class Extensions
    {
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
    }
}
