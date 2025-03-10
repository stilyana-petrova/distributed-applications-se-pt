﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SongDiaryApplicationServices.Interfaces;

namespace SongDiaryWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJWTAuthenticationsManager _jwtauthenticationsManager;

        public AuthController(IJWTAuthenticationsManager jwtauthenticationsManager)
        {
            _jwtauthenticationsManager = jwtauthenticationsManager;
        }

        [HttpPut]
        public async Task<AuthenticationResponse> Authenticate([FromQuery] string clientId, [FromQuery] string secret)
        {
            string? token = _jwtauthenticationsManager.Authenticate(clientId, secret);

            ArgumentNullException.ThrowIfNull(token);

            return await Task.FromResult(new AuthenticationResponse() { Token = token });
        }
    }
    public class AuthenticationResponse
    {
        required public string Token { get; set; }
    }
}
