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
    /// Service for CRUD operation of the songs
    /// </summary>
    public class SongService : ISongService
    {
        /// <summary>
        /// Database context for accessing song data.
        /// </summary>
        private readonly SongDiaryDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="SongService"/> class.
        /// </summary>
        /// <param name="context">The databese context</param>
        public SongService(SongDiaryDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new song in the database
        /// </summary>
        /// <param name="song">The song entity to create</param>
        /// <returns>The created song</returns>
        /// <exception cref="Exception">If the specific artist does not exist</exception>
        public async Task<Song> Create(Song song)
        {
            var artist = await _context.Artists.FindAsync(song.ArtistId);
            if (artist == null) throw new Exception("Invalid ArtistId. Artist does not exist");
            _context.Songs.Add(song);
            await _context.SaveChangesAsync();
            return song;
        }

        /// <summary>
        /// Deletes a song by its Id
        /// </summary>
        /// <param name="id">the id of the song to delete</param>
        /// <returns><c>true</c> if the song was successfully deleted, otherwise <c>false</c></returns>
        public async Task<bool> Delete(int id)
        {
            var song = await _context.Songs.FindAsync(id);
            if (song == null) return false;

            _context.Songs.Remove(song);
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Finds a song by its id
        /// </summary>
        /// <param name="id">the id of the song</param>
        /// <returns>The song with the specific id. If it's not found returns <c>null</c></returns>
        public async Task<Song?> GetSongById(int id)
        {
            return await _context.Songs.Include(s => s.Artist).FirstOrDefaultAsync(s => s.Id == id);
        }

        /// <summary>
        /// Retrieves a list of all the songs in the database, including their associated artists
        /// </summary>
        /// <returns>Collection of songs</returns>
        public async Task<IEnumerable<Song>> GetSongs()
        {
            return await _context.Songs.Include(s => s.Artist).ToListAsync();
        }

        /// <summary>
        /// Searches for songs with title and artist name
        /// </summary>
        /// <param name="searchByTitle">The search for song's title</param>
        /// <param name="searchByArtist">The search for the artist's name</param>
        /// <returns>a list of songs that mathes the search</returns>
        public List<Song> GetSongs(string searchByTitle, string searchByArtist)
        {
            IQueryable<Song> query = _context.Songs.Include(s => s.Artist);

            if (!String.IsNullOrEmpty(searchByTitle))
            {
                query = query.Where(s => s.Title.ToLower().Contains(searchByTitle.ToLower()));
            }
            if (!String.IsNullOrEmpty(searchByArtist))
            {
                query = query.Where(s => s.Artist != null && s.Artist.Name.ToLower().Contains(searchByArtist.ToLower()));
            }
            return query.ToList();
        }

        /// <summary>
        /// Updates an existing song
        /// </summary>
        /// <param name="id">the id of the song</param>
        /// <param name="updatedSong">The updated song entity</param>
        /// <returns><c>true</c> if the update was successful, otherwise <c>false</c></returns>
        public async Task<bool> Update(int id, Song updatedSong)
        {
            var song = await _context.Songs.FindAsync(id);
            if (song == null) return false;

            song.Title = updatedSong.Title;
            song.Lyrics = updatedSong.Lyrics;
            song.ReleaseDate = updatedSong.ReleaseDate;
            song.Genre = updatedSong.Genre;
            song.ArtistId = updatedSong.ArtistId;

            _context.Songs.Update(song);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<PagedResult<SongDTO>> GetSongsAsync(int page, int size)
        {
            var query = _context.Songs.AsQueryable();

            int totalCount = await query.CountAsync();

            var songs = await query
                .Skip((page - 1) * size)
                .Take(size)
                .Select(s => new SongDTO
                {
                    Id = s.Id,
                    Title = s.Title,
                    ArtistName=s.Artist.Name // Assuming a relationship
                })
            .ToListAsync();

            return new PagedResult<SongDTO>
            {
                Items = songs,
                TotalCount = totalCount,
                PageNumber = page,
                PageSize = size
            };
        }
    }
}
