using SongDiaryApplicationServices.Models;
using SongDiaryData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongDiaryApplicationServices.Interfaces
{
    public interface IArtistService
    {
        Task<IEnumerable<Artist>> GetArtists();
        Artist GetArtistById(int id);
        Task<Artist> Create(string name, string bio, DateTime dateOfBirth, string position);
        Task<bool> Update(int id, string name, string bio, DateTime dateOfBirth, string position);
        bool Delete(int id);

        List<Artist?> GetArtists(string searchByArtistName);
        Task<PagedResult<ArtistDTO>> GetPagedArtists(int page, int size);


    }
}
