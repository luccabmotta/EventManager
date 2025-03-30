using EventManagementAPI.Data;
using EventManagementAPI.Interfaces;
using EventManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventManagementAPI.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly AppDbContext _context;

        public ArtistRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Artist> GetAll()
        {
            return _context.Artists.AsQueryable();
        }

        public async Task<Artist?> GetByIdAsync(int id)
        {
            return await _context.Artists.FindAsync(id);
        }

        public async Task AddAsync(Artist artist)
        {
            _context.Artists.Add(artist);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Artist artist)
        {
            _context.Entry(artist).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Artist artist)
        {
            _context.Artists.Remove(artist);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Artists.AnyAsync(e => e.Id == id);
        }
    }
}
