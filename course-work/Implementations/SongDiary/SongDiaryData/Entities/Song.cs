using SongDiaryData.Entities.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongDiaryData.Entities
{
    public class Song:BaseEntity
    {
        public required string Title { get; set; }
        public required string Lyrics { get; set; }

        public DateTime ReleaseDate { get; set; }

        [ForeignKey(nameof(Artist))]
        public int ArtistId { get; set; }
        public Artist? Artist { get; set; }

        public required Genre Genre { get; set; }
        public required bool IsPublic {  get; set; }
    }
}
