using Microsoft.EntityFrameworkCore;
using SongDiaryData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongDiaryData.Context
{
    /// <summary>
    /// Song diary database context
    /// </summary>
    public class SongDiaryDbContext:DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SongDiaryDbContext"/> class.
        /// </summary>
        /// <param name="options">Database context options</param>
        public SongDiaryDbContext(DbContextOptions<SongDiaryDbContext> options) : base(options) { }

        /// <summary>
        /// Gets or sets artists and songs dbset collection.
        /// </summary>
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<User> Users { get; set; }

      
    }
}
