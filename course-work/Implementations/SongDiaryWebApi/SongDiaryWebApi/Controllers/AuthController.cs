using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using SongDiaryApplicationServices.Interfaces;
using SongDiaryApplicationServices.Services;

namespace SongDiaryWebApi.Controllers
{
    /// <summary>
    /// Controller for user authentication
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// injects the <see cref="IUserService"/> service
        /// </summary>
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="userService">Service for handling user authentication</param>
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Authenticates a user and returns a JWT token.
        /// </summary>
        /// <param name="model">User login credentials</param>
        /// <returns>A JWT token if authentication is successful</returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            var token = _userService.Authenticate(model.Username, model.Password);
            if (token == null)
                return Unauthorized(new { message = "Invalid username or password" });

            return Ok(new { Token = token });
        }
    }
    /// <summary>
    /// Represents user login credentials.
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// The username of the user
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The password of the user
        /// </summary>
        public string Password { get; set; }
    }
}
