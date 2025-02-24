using SongDiaryData.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongDiaryData.Entities
{
    public class Artist:BaseEntity
    {
        public required string Name { get; set; }
        public string? Bio { get; set; } 
        public DateTime DateOfBirth { get; set; }
        public Positions Positions { get; set; }
        public virtual ICollection<Song>? Songs { get; set; }
    }
}
