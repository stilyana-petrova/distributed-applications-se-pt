using SongDiaryData.Entities.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongDiaryData.Entities
{
    public class User:BaseEntity
    {
        [MinLength(3)]
        public required string Username { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }
        public required Role Role { get; set; }

    }
}
