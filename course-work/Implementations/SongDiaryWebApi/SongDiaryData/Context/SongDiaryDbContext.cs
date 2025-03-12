using Microsoft.EntityFrameworkCore;
using SongDiaryData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongDiaryData.Context
{
    public class SongDiaryDbContext:DbContext
    {
        public SongDiaryDbContext(DbContextOptions<SongDiaryDbContext> options) : base(options) { }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
