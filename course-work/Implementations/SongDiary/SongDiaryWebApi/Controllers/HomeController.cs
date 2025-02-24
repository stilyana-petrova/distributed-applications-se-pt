using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SongDiaryWebApi.Controllers
{
    //Home Controller
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
