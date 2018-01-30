using OnTask.Business.Services.Interfaces;
using OnTask.Data.Entities;

namespace OnTask.Business.Services
{
    /// <summary>
    /// Provides the base implementation of a service.
    /// </summary>
    public class BaseService : IBaseService
    {
        #region Properties
        /// <summary>
        /// Gets the current user for the <see cref="BaseService"/> class.
        /// </summary>
        public User ApplicationUser { get; private set; }
        #endregion

        #region Public Interface
        /// <summary>
        /// Sets the <see cref="ApplicationUser"/> for the <see cref="BaseService"/> class.
        /// </summary>
        /// <param name="applicationUser">The current <see cref="User"/> of the application.</param>
        public void AddApplicationUser(User applicationUser)
        {
            ApplicationUser = applicationUser;
        }
        #endregion
    }
}
