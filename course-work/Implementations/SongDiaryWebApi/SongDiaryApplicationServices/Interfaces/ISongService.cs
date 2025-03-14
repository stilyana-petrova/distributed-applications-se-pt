using SongDiaryApplicationServices.Models;
using SongDiaryData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongDiaryApplicationServices.Interfaces
{
    public interface ISongService
    {
        Task<IEnumerable<Song>> GetSongs();
        Task<Song?> GetSongById(int id);
        Task<Song> Create(Song song);
        Task<bool> Update(int id, Song updatedSong);
        Task<bool> Delete(int id);
        List<Song> GetSongs(string searchByTitle, string searchByArtist);
        Task<PagedResult<SongDTO>> GetSongsAsync(int page, int size);


    }
}
