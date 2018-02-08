using System;

namespace OnTask.Business.Services.Interfaces
{
    /// <summary>
    /// Exposes mappings from entity to model classes.
    /// </summary>
    public interface IMapperService
    {
        /// <summary>
        /// Maps the <paramref name="source"/> to the result object.
        /// </summary>
        /// <typeparam name="TResult">The <see cref="Type"/> of the result object.</typeparam>
        /// <param name="source">The source <see cref="object"/> to map from.</param>
        /// <param name="tag">The <see cref="object"/> used to send additional parameters for the mapping.</param>
        /// <returns>The mapped result object.</returns>
        TResult Map<TResult>(object source, object tag = null);
    }
}
