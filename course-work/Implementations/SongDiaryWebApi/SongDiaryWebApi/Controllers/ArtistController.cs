using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SongDiaryApplicationServices.Interfaces;
using SongDiaryData.Entities;

namespace SongDiaryWebApi.Controllers
{
    /// <summary>
    /// API controller for managing artists
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        /// <summary>
        /// Injects the <see cref="IArtistService"/> service
        /// </summary>
        private readonly IArtistService _artistService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArtistController"/> class.
        /// </summary>
        /// <param name="artistService">Service for handling artist operations</param>
        public ArtistController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        /// <summary>
        /// Retrieves all artists
        /// </summary>
        /// <returns>a list of artists</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artist>>> GetArtists()
        {
            var artists = await _artistService.GetArtists();
            return Ok(artists);
        }

        /// <summary>
        /// Retrieves artist by specific id
        /// </summary>
        /// <param name="id">the id of the artist</param>
        /// <returns>The artist with the specific id</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Artist>> GetById(int id)
        {
            var artist = _artistService.GetArtistById(id);
            if (artist == null) return NotFound();
            return Ok(artist);
        }

        /// <summary>
        /// Creates a new artist
        /// </summary>
        /// <param name="artist">The artist data</param>
        /// <returns>The created artist</returns>
        [HttpPost]
        public async Task<ActionResult<Artist>> Create([FromBody] Artist artist)
        {
            var createdArtist = await _artistService.Create(artist.Name, artist.Bio, artist.DateOfBirth, artist.Position);
            return CreatedAtAction(nameof(GetById), new { id = createdArtist.Id }, createdArtist);
        }

        /// <summary>
        /// Updates an existing artist
        /// </summary>
        /// <param name="id">the id of the artist</param>
        /// <param name="artist">Updated artist information</param>
        /// <returns>No content if the update is successful and Not Found when cannot update</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Artist artist)
        {
            var updated = await _artistService.Update(id, artist.Name, artist.Bio, artist.DateOfBirth, artist.Position);
            if (!updated) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Delete an artist by id
        /// </summary>
        /// <param name="id">the id of the artist to delete</param>
        /// <returns>No content if the deletion is successful and Not Found when cannot delete</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = _artistService.Delete(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// searches for artists by name
        /// </summary>
        /// <param name="name">the name or part of the name</param>
        /// <returns>A list of matching artists</returns>
        [HttpGet("search")]
        public ActionResult<List<Artist?>> SearchArtists([FromQuery] string name)
        {
            var artists = _artistService.GetArtists(name);
            return Ok(artists);
        }
    }
}
