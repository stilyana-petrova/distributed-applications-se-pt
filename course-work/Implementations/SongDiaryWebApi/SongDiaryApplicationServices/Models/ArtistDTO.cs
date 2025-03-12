using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongDiaryApplicationServices.Models
{
    /// <summary>
    /// DTO for Artist and its details
    /// </summary>
    public class ArtistDTO
    {
        /// <summary>
        /// The unique identifier of the artist
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the artist with maximum length 50 characters
        /// </summary>
        [MaxLength(50)]
        public string? Name { get; set; }

        /// <summary>
        /// Biography of the artist with maximum length 1000 characters
        /// </summary>
        [MaxLength(1000)]
        public string? Bio { get; set; }

        /// <summary>
        /// Date of birth of the artist
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// The position of the artist - singer, guitarist, pianist etc.
        /// </summary>
        public string? Positions { get; set; }

    }
}
