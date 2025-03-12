using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SongDiaryApplicationServices.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SongDiaryApplicationServices.Services
{
    /// <summary>
    /// Service for generating JWT authentication tokens.
    /// </summary>
    public class JwtTokenService: IJwtTokenService
    {
        /// <summary>
        /// Configuration settings for JWT.
        /// </summary>
        private readonly IConfiguration _config;

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtTokenService"/> class.
        /// </summary>
        /// <param name="config">The application configuration containing JWT settings.</param>
        public JwtTokenService(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Generates a JWT token for the specified user.
        /// </summary>
        /// <param name="username">The username for whom the token is generated</param>
        /// <param name="role">the role of the user</param>
        /// <returns>A JWT token as a string</returns>
        public string GenerateToken(string username, string role)
        {
            var jwtSettings = _config.GetSection("JwtSettings");
            var secretKey = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]);

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role)
        };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpiryMinutes"])),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
