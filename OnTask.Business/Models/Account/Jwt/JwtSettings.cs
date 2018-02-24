namespace OnTask.Business.Models.Account.Jwt
{
    /// <summary>
    /// Provides settings for JSON Web Tokens.
    /// </summary>
    public class JwtSettings
    {
        /// <summary>
        /// Gets or sets the JWT audience.
        /// </summary>
        public string Audience { get; set; }
        /// <summary>
        /// Gets or sets the JWT expiration time in days.
        /// </summary>
        public double ExpireDays { get; set; }
        /// <summary>
        /// Gets or sets the JWT issuer.
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// Gets or sets the JWT key.
        /// </summary>
        public string Key { get; set; }
    }
}
