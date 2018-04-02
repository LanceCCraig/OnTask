using System;

namespace OnTask.Business.Models.Account.Jwt
{
    /// <summary>
    /// Represents a JSON Web Token.
    /// </summary>
    public class JsonWebToken
    {
        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// Gets or sets the refresh token.
        /// </summary>
        public string RefreshToken { get; set; }
        /// <summary>
        /// Gets or sets the expiration date.
        /// </summary>
        public DateTime Expires { get; set; }
    }
}
