using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SongDiaryApplicationServices.Interfaces;
using SongDiaryData.Entities;

namespace SongDiaryWebApi.Controllers
{
    /// <summary>
    /// Controller for managing songs.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        /// <summary>
        /// injects the <see cref="ISongService"/> service
        /// </summary>
        private readonly ISongService _songService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SongController"/> class.
        /// </summary>
        /// <param name="songService">Service for handling song operations</param>
        public SongController(ISongService songService)
        {
            _songService = songService;
        }

        /// <summary>
        /// Retrieves all songs
        /// </summary>
        /// <returns>list of songs</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Song>>> GetSongs()
        {
            var songs = await _songService.GetSongs();
            return Ok(songs);
        }

        /// <summary>
        /// Retrieves a song by id
        /// </summary>
        /// <param name="id">The song id</param>
        /// <returns>The requested song</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Song>> GetSongById(int id)
        {
            var song = await _songService.GetSongById(id);
            if (song == null) return NotFound();
            return Ok(song);
        }

        /// <summary>
        /// Creates a new song
        /// </summary>
        /// <param name="song">The song details</param>
        /// <returns>The created song</returns>
        [HttpPost]
        public async Task<ActionResult<Song>> Create([FromBody] Song song)
        {
            var createdSong = await _songService.Create(song);
            return CreatedAtAction(nameof(GetSongById), new { id = createdSong.Id }, createdSong);
        }

        /// <summary>
        /// Updates an existing song
        /// </summary>
        /// <param name="id">The song id</param>
        /// <param name="song">Updated song details.</param>
        /// <returns>No content if update is successful, if not - Not Found</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Song song)
        {
            var updated = await _songService.Update(id, song);
            if (!updated) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Deletes a song by id
        /// </summary>
        /// <param name="id">the song id</param>
        /// <returns>No content if deletion is successful, if it's unsuccessful - Not Found</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _songService.Delete(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Searches songs by title or artist
        /// </summary>
        /// <param name="title">Search by title</param>
        /// <param name="artist">Search by artist</param>
        /// <returns>List of matching songs</returns>
        [HttpGet("search")]
        public ActionResult<List<Song>> SearchSongs([FromQuery] string title, [FromQuery] string artist)
        {
            var songs = _songService.GetSongs(title, artist);
            return Ok(songs);
        }

        /// <summary>
        /// Retrieves paginated songs
        /// </summary>
        /// <param name="page">The page number (must be greater than 0)</param>
        /// <param name="size">The page size (musit be greater than 0)</param>
        /// <returns>A paginated list of songs</returns>
        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedSongs([FromQuery] int page = 1, [FromQuery] int size = 3)
        {
            if (page < 1 || size < 1)
            {
                return BadRequest("Page number and page size must be greater than 0.");
            }

            var result = await _songService.GetSongsAsync(page, size);
            return Ok(result);
        }
    }
}
