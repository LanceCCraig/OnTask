using System;

namespace OnTask.Business.Models.Account.Jwt
{
    /// <summary>
    /// Represents a token for refreshing a JSON Web Token's access.
    /// </summary>
    public class RefreshToken
    {
        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// Gets or sets the token value.
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// Gets or sets the expiration date.
        /// </summary>
        public DateTime Expires { get; set; }
    }
}
