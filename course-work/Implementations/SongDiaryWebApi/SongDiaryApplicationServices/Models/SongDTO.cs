using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongDiaryApplicationServices.Models
{
    /// <summary>
    /// DTO for the song and its details
    /// </summary>
    public class SongDTO
    {
        /// <summary>
        /// The unique identifier of the song
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The title of the song with max length 100 characters
        /// </summary>
        [MaxLength(100)]
        public string? Title { get; set; }

        /// <summary>
        /// The lyrics of the song with min length 5 characters
        /// </summary>
        [MinLength(5)]
        public string? Lyrics { get; set; }

        /// <summary>
        /// The Release date of the song
        /// </summary>
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// The artist performing the song
        /// </summary>
        public string? ArtistName { get; set; }

        /// <summary>
        /// The genre of the song
        /// </summary>
        public string? Genre { get; set; }

    }
}
