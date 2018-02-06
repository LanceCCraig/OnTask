using OnTask.Data.Entities;

namespace OnTask.Business.Services.Interfaces
{
    /// <summary>
    /// Exposes the base implementation of a service.
    /// </summary>
    public interface IBaseService
    {
        /// <summary>
        /// Gets the current user for the <see cref="IBaseService"/> class.
        /// </summary>
        User ApplicationUser { get; }
        /// <summary>
        /// Sets the <see cref="ApplicationUser"/> for the <see cref="IBaseService"/> class.
        /// </summary>
        /// <param name="applicationUser">The current <see cref="User"/> of the application.</param>
        void AddApplicationUser(User applicationUser);
    }
}
