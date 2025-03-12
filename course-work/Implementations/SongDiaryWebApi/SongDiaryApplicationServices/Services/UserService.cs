using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Generators;
using SongDiaryApplicationServices.Interfaces;
using SongDiaryData.Context;
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
    /// Service for handling user authentication and JWT token generation.
    /// </summary>
    public class UserService:IUserService
    {
        /// <summary>
        /// Database context for accessing user data.
        /// </summary>
        private readonly SongDiaryDbContext _context;

        /// <summary>
        /// Configuration settings for JWT.
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="context">The database context for user authentication</param>
        /// <param name="configuration">Configuration settings for JWT authentication</param>
        public UserService(SongDiaryDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        /// <summary>
        /// Authenticates a user by verifying the provided credentials and generates a JWT token if successful.
        /// </summary>
        /// <param name="username">The username of the user attempting to authenticate</param>
        /// <param name="password">The password entered by the user</param>
        /// <returns>A JWT token if authentication is successful, otherwise <c>null</c></returns>
        public string Authenticate(string username, string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
