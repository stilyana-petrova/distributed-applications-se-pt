﻿using SongDiaryApplicationServices.Interfaces;
using SongDiaryApplicationServices.Models;
using SongDiaryData.Context;
using SongDiaryData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SongDiaryApplicationServices.Models;

namespace SongDiaryApplicationServices.Implementation
{
    public class ArtistService : IArtistService
    {
        private readonly SongDiaryDbContext _context;

        public ArtistService(SongDiaryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Artist>> GetArtists()
        {
            List<Artist> artists = _context.Artists.ToList();
            return artists;
        }
        public Artist GetArtistById(int id)
        {
            return _context.Artists.Find(id);
        }
        public async Task<Artist> Create(string name, string bio, DateTime dateOfBirth)
        {
            Artist artist = new Artist
            {
                Name = name,
                Bio = bio,
                DateOfBirth = dateOfBirth,
            };
            _context.Artists.Add(artist);
            return artist;
        }
        public async Task<bool> Update(int id, string name, string bio, DateTime dateOfBirth)
        {
            var artist = GetArtistById(id);
            if (artist == default(Artist)) return false;

            artist.Name = name;
            artist.Bio = bio;
            artist.DateOfBirth = dateOfBirth;

            _context.Update(artist);
            return _context.SaveChanges() != 0;
        }
        public bool Delete(int id)
        {
            var artist = GetArtistById(id);
            if (artist == default(Artist))
            {
                return false;
            }
            _context.Remove(artist);
            return _context.SaveChanges() != 0;
        }

        public List<Artist?> GetArtists(string searchByArtistName)
        {
            List<Artist> artists = _context.Artists.ToList();
            if (!String.IsNullOrEmpty(searchByArtistName))
            {
                artists = artists.Where(x => x.Name.ToLower().Contains(searchByArtistName.ToLower())).ToList();
            }
            return artists;
        }
    }
}
