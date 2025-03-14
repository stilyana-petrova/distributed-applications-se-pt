using Microsoft.EntityFrameworkCore;
using SongDiaryApplicationServices.Interfaces;
using SongDiaryApplicationServices.Models;
using SongDiaryData.Context;
using SongDiaryData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongDiaryApplicationServices.Services
{
    /// <summary>
    /// Provides CRUD operation for the artists
    /// </summary>
    public class ArtistService:IArtistService
    {
        /// <summary>
        /// Injects the database - SongDiaryDbContext
        /// </summary>
        private readonly SongDiaryDbContext _context;

        /// <summary>
        /// Initialize a new instance of the <see cref="ArtistService"/> class
        /// </summary>
        /// <param name="context">Database context to be injected</param>
        public ArtistService(SongDiaryDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a list of the artists
        /// </summary>
        /// <returns>List of <see cref="Artist"/> entity</returns>
        public async Task<IEnumerable<Artist>> GetArtists()
        {
            List<Artist> artists = _context.Artists.ToList();
            return artists;
        }

        /// <summary>
        /// Find artist by specific id
        /// </summary>
        /// <param name="id">Id of the artist</param>
        /// <returns>The <see cref="Artist"/> with the specific id</returns>
        public Artist GetArtistById(int id)
        {
            return _context.Artists.Find(id);
        }

        /// <summary>
        /// Creates a new artist and save it to the database
        /// </summary>
        /// <param name="name">Name of the artist</param>
        /// <param name="bio">The biography of the artist</param>
        /// <param name="dateOfBirth">Date of birth of the artist</param>
        /// <param name="position">The artist's position</param>
        /// <returns>The created <see cref="Artist"/> entity</returns>
        public async Task<Artist> Create(string name, string bio, DateTime dateOfBirth, string position)
        {
            Artist artist = new Artist
            {
                Name = name,
                Bio = bio,
                DateOfBirth = dateOfBirth,
                Position = position
            };
            _context.Artists.Add(artist);
            await _context.SaveChangesAsync();
            return artist;
        }

        /// <summary>
        /// Updates the existing artist's information
        /// </summary>
        /// <param name="id">The id of the artist</param>
        /// <param name="name">The name of the artist</param>
        /// <param name="bio">The biography of the artist</param>
        /// <param name="dateOfBirth">Date of birth of the artist</param>
        /// <param name="position">The position of the artist</param>
        /// <returns><c>true</c> if the update is successful, if not - <c>false</c></returns>
        public async Task<bool> Update(int id, string name, string bio, DateTime dateOfBirth, string position)
        {
            var artist = GetArtistById(id);
            if (artist == default(Artist)) return false;

            artist.Name = name;
            artist.Bio = bio;
            artist.DateOfBirth = dateOfBirth;
            artist.Position = position;

            _context.Update(artist);
            return _context.SaveChanges() != 0;
        }

        /// <summary>
        /// Deletes artist by the id
        /// </summary>
        /// <param name="id">The id of the artist</param>
        /// <returns><c>true</c> if the update is successfully deleted, otherwise <c>false</c></returns>
        public bool Delete(int id)
        {
            var artist = GetArtistById(id);
            if (artist == default(Artist))
            {
                return false;
            }
            _context.Remove(artist);
            return _context.SaveChanges() != 0;
        }

        /// <summary>
        /// Retrieves a list of the artist filtered by name
        /// </summary>
        /// <param name="searchByArtistName">The name or part of the name to filter by</param>
        /// <returns>A list of <see cref="Artist"/> entities that matches the search criteria.</returns>

        public List<Artist?> GetArtists(string searchByArtistName)
        {
            List<Artist> artists = _context.Artists.ToList();
            if (!String.IsNullOrEmpty(searchByArtistName))
            {
                artists = artists.Where(x => x.Name.ToLower().Contains(searchByArtistName.ToLower())).ToList();
            }
            return artists;
        }

        public async Task<PagedResult<ArtistDTO>> GetPagedArtists(int page, int size)
        {
            var query = _context.Artists.AsQueryable();
            var totalCount = await query.CountAsync();
            var artists = await query.Skip((page - 1) * size).Take(size)
                .Select(a => new ArtistDTO
                {
                    Id = a.Id,
                    Name = a.Name,
                    Bio = a.Bio,
                    DateOfBirth = a.DateOfBirth,
                    Positions = a.Position
                }).ToListAsync();

            return new PagedResult<ArtistDTO>
            {
                Items = artists,
                TotalCount = totalCount,
                PageNumber = page,
                PageSize = size
            };
        }
    }
}
