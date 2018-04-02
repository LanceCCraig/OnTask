namespace OnTask.Common
{
    /// <summary>
    /// Provides logic for calculating hash codes.
    /// </summary>
    public static class HashCodeGenerator
    {
        #region Constants
        /// <summary>
        /// Gets the base value that is used for calculating a hash code.
        /// </summary>
        public const int HashBase = 27;
        private const int HashMultiplier = 13;
        #endregion

        #region Public Interface
        /// <summary>
        /// Computes the hash code from the individual provides property hash codes.
        /// </summary>
        /// <param name="values">The hash codes for the identifying properties.</param>
        /// <returns>The hash code for the object.</returns>
        public static int Compute(params int[] values)
        {
            unchecked
            {
                var hash = HashBase;
                foreach (var value in values)
                {
                    hash = Compute(hash, value);
                }
                return hash;
            }
        }
        #endregion

        #region Private Helpers
        private static int ComputeNext(int currentHashCode, int hashCodeToAdd) => (HashMultiplier * currentHashCode) + hashCodeToAdd;
        #endregion
    }
}
