using SongDiaryData.Entities.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongDiaryApplicationServices.Models
{
    public class ArtistDTO
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(1000)]
        public string? Bio { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Positions Positions { get; set; }
    }
}
