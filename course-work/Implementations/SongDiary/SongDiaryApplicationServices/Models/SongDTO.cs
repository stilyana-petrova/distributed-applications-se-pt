using SongDiaryData.Entities.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongDiaryApplicationServices.Models
{
    public class SongDTO
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        [MinLength(50)]
        public string Lyrics { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ArtistName { get; set; }
        public Genre Genre { get; set; }
        public bool IsPublic { get; set; }
    }
}
