using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SongDiaryApplicationServices.Interfaces;
using SongDiaryApplicationServices.Models;
using SongDiaryData.Entities;

namespace SongDiaryWebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly ISongService _songService;

        public SongController(ISongService songService)
        {
            _songService = songService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Song>>> GetSongs()
        {
            var songs = await _songService.GetSongs();
            return Ok(songs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Song>> GetSongById(int id)
        {
            var song = await _songService.GetSongById(id);
            if (song == null) return NotFound();
            return Ok(song);
        }



        [HttpPost]
        public async Task<ActionResult<Song>> Create([FromBody]Song song)
        {
            var createdSong = await _songService.Create(song);
            return CreatedAtAction(nameof(GetSongById), new { id = createdSong.Id }, createdSong);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Song song)
        {
            var updated = await _songService.Update(id, song);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _songService.Delete(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        [HttpGet("search")]
        public ActionResult<List<Song>> SearchSongs([FromQuery] string title, [FromQuery] string artist)
        {
            var songs=_songService.GetSongs(title, artist);
            return Ok(songs);
        }
    }
}
