using Omu.ValueInjecter.Injections;
using System;

namespace OnTask.Common.Injections
{
    /// <summary>
    /// Provides injection from nullable to non-nullable properties and the inverse.
    /// </summary>
    public class NullableInjection : LoopInjection
    {
        #region Overrides
        /// <summary>
        /// Determines whether the source <see cref="Type"/> is equal to the target <see cref="Type"/>.
        /// </summary>
        /// <param name="source">The <see cref="Type"/> of the source property.</param>
        /// <param name="target">The <see cref="Type"/> of the target property.</param>
        /// <returns><c>true</c> if the <see cref="Type"/> of the two properties are equal, ignoring nullability; otherwise, <c>false</c>.</returns>
        protected override bool MatchTypes(Type source, Type target)
        {
            var underlyingSource = Nullable.GetUnderlyingType(source);
            var underlyingTarget = Nullable.GetUnderlyingType(target);

            var baseMatches = base.MatchTypes(source, target);
            var underlyingSourceMatches = underlyingSource != null && underlyingSource == target;
            var underlyingTargetMatches = underlyingTarget != null && underlyingTarget == source;

            return
                baseMatches ||
                underlyingSourceMatches ||
                underlyingTargetMatches;
        }
        #endregion
    }
}
