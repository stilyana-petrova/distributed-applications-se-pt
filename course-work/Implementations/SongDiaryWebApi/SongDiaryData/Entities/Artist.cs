using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SongDiaryData.Entities
{
    public class Artist
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public required string Name { get; set; }
        [MaxLength(1000)]
        public string? Bio { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Position { get; set; }
        //[JsonIgnore]
        //public virtual ICollection<Song>? Songs { get; set; }

    }
}
