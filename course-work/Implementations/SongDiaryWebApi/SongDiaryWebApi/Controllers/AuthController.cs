using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using SongDiaryApplicationServices.Interfaces;
using SongDiaryApplicationServices.Services;

namespace SongDiaryWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            var token = _userService.Authenticate(model.Username, model.Password);
            if (token == null)
                return Unauthorized(new { message = "Invalid username or password" });

            return Ok(new { Token = token });
        }
    }
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
