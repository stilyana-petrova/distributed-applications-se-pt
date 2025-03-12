using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongDiaryData.Entities
{
    public class Song
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public required string Title { get; set; }
        [MinLength(5)]
        public required string Lyrics { get; set; }

        public DateTime ReleaseDate { get; set; }

        [ForeignKey(nameof(Artist))]
        public int ArtistId { get; set; }
        public Artist? Artist { get; set; }

        public required string Genre { get; set; }
    }
}
