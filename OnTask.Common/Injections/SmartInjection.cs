using System.Reflection;

namespace OnTask.Common.Injections
{
    /// <summary>
    /// Provides injection where the target property is only set if it's different than the source.
    /// </summary>
    public class SmartInjection : NullableInjection
    {
        #region Overrides
        /// <summary>
        /// Sets the target property to the source property if they are not equal.
        /// </summary>
        /// <param name="source">The source <see cref="object"/> to inject from.</param>
        /// <param name="target">The target <see cref="object"/> to inject to.</param>
        /// <param name="sp">The <see cref="PropertyInfo"/> for the <paramref name="source"/> parameter.</param>
        /// <param name="tp">The <see cref="PropertyInfo"/> for the <paramref name="target"/> parameter.</param>
        protected override void SetValue(object source, object target, PropertyInfo sp, PropertyInfo tp)
        {
            var sourceValue = sp.GetValue(source, null);
            var targetValue = tp.GetValue(target, null);

            var sourceIsNullAndDoesNotMatch = sourceValue == null && targetValue != null;
            var sourceIsNotNullAndDoesNotMatch = sourceValue != null && !sourceValue.Equals(targetValue);

            if (sourceIsNullAndDoesNotMatch || sourceIsNotNullAndDoesNotMatch)
            {
                tp.SetValue(target, sourceValue, null);
            }
        }
        #endregion
    }
}
