using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SongDiaryApplicationServices.Interfaces;
using SongDiaryData.Entities;

namespace SongDiaryWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistService _artistService;

        public ArtistController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artist>>> GetArtists()
        {
            var artists = await _artistService.GetArtists();
            return Ok(artists);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Artist>> GetById(int id)
        {
            var artist = _artistService.GetArtistById(id);
            if (artist == null) return NotFound();
            return Ok(artist);
        }

        [HttpPost]
        public async Task<ActionResult<Artist>> Create([FromBody] Artist artist)
        {
            var createdArtist = await _artistService.Create(artist.Name, artist.Bio, artist.DateOfBirth, artist.Position);
            return CreatedAtAction(nameof(GetById), new { id = createdArtist.Id }, createdArtist);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Artist artist)
        {
            var updated = await _artistService.Update(id, artist.Name, artist.Bio, artist.DateOfBirth, artist.Position);
            if (!updated) return NotFound();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = _artistService.Delete(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        [HttpGet("search")]
        public ActionResult<List<Artist?>> SearchArtists([FromQuery] string name)
        {
            var artists = _artistService.GetArtists(name);
            return Ok(artists);
        }
    }
}
