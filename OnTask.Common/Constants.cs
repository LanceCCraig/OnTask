using System.Diagnostics.CodeAnalysis;

namespace OnTask.Common
{
    /// <summary>
    /// Provides constant values for the application.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class Constants
    {
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
