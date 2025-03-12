using Microsoft.EntityFrameworkCore;
using SongDiaryApplicationServices.Interfaces;
using SongDiaryData.Context;
using SongDiaryData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongDiaryApplicationServices.Services
{
    public class SongService:ISongService
    {
        private readonly SongDiaryDbContext _context;

        public SongService(SongDiaryDbContext context)
        {
            _context = context;
        }

        public async Task<Song> Create(Song song)
        {
            var artist = await _context.Artists.FindAsync(song.ArtistId);
            if (artist == null) throw new Exception("Invalid ArtistId. Artist does not exist");
            _context.Songs.Add(song);
            await _context.SaveChangesAsync();
            return song;
        }

        public async Task<bool> Delete(int id)
        {
            var song = await _context.Songs.FindAsync(id);
            if (song == null) return false;

            _context.Songs.Remove(song);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Song?> GetSongById(int id)
        {
            return await _context.Songs.Include(s => s.Artist).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Song>> GetSongs()
        {
            return await _context.Songs.Include(s => s.Artist).ToListAsync();
        }

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
    }
}
