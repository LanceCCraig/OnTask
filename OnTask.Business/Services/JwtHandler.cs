using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnTask.Business.Models.Account.Jwt;
using OnTask.Business.Services.Interfaces;
using OnTask.Data.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnTask.Business.Services
{
    /// <summary>
    /// Provides functionality to create JSON Web Tokens.
    /// </summary>
    public class JwtHandler : IJwtHandler
    {
        #region Fields
        private readonly JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        private readonly JwtHeader header;
        private readonly SecurityKey securityKey;
        private readonly JwtSettings settings;
        private readonly SigningCredentials signingCredentials;
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="JwtHandler"/> class.
        /// </summary>
        /// <param name="settingsOptions">The settings for creating JWTs.</param>
        public JwtHandler(IOptions<JwtSettings> settingsOptions)
        {
            settings = settingsOptions.Value;
            securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Key));
            signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            header = new JwtHeader(signingCredentials);
        }
        #endregion

        #region Public Interface
        /// <summary>
        /// Creates a <see cref="JsonWebToken"/>.
        /// </summary>
        /// <param name="user">The <see cref="User"/> to create the <see cref="JsonWebToken"/> for.</param>
        /// <returns>The newly created <see cref="JsonWebToken"/>.</returns>
        public JsonWebToken Create(User user)
        {
            var now = DateTime.Now;
            var expires = now.Add(TimeSpan.FromDays(settings.ExpireDays));
            // TODO: Change expiration to 15 minutes and utilize refresh tokens.

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email)
            };
            var payload = new JwtPayload(
                settings.Issuer,
                settings.Audience,
                claims,
                notBefore: now,
                expires: expires,
                issuedAt: now);
            var securityToken = new JwtSecurityToken(header, payload);
            var accessToken = jwtSecurityTokenHandler.WriteToken(securityToken);

            return new JsonWebToken
            {
                AccessToken = accessToken,
                Expires = expires
            };
        }
        #endregion
    }
}
