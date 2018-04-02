using OnTask.Business.Models.Account.Jwt;
using OnTask.Data.Entities;

namespace OnTask.Business.Services.Interfaces
{
    /// <summary>
    /// Exposes functionality to create JSON Web Tokens.
    /// </summary>
    public interface IJwtHandler
    {
        /// <summary>
        /// Creates a <see cref="JsonWebToken"/>.
        /// </summary>
        /// <param name="user">The <see cref="User"/> to create the <see cref="JsonWebToken"/> for.</param>
        /// <returns>The newly created <see cref="JsonWebToken"/>.</returns>
        JsonWebToken Create(User user);
    }
}