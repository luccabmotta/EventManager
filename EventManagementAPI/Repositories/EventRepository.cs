using EventManagementAPI.Data;
using EventManagementAPI.Interfaces;
using EventManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventManagementAPI.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly AppDbContext _context;

        public EventRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Event> GetEvents()
        {
            return _context.Events.AsQueryable();
        }

        public async Task<Event?> GetByIdAsync(int id)
        {
            return await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task AddAsync(Event @event)
        {
            _context.Events.Add(@event);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Event @event)
        {
            _context.Entry(@event).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Event @event)
        {
            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Events.AnyAsync(e => e.Id == id);
        }

        public async Task AddArtistToEventAsync(int eventId, int artistId)
        {
            var currentEvent = await _context.Events.FindAsync(eventId);
            var artist = await _context.Artists.FindAsync(artistId);

            if (currentEvent == null || artist == null) return;

            if (!currentEvent.ArtistIds.Contains(artistId))
            {
                currentEvent.ArtistIds.Add(artistId);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveArtistFromEventAsync(int eventId, int artistId)
        {
            var currentEvent = await _context.Events.FindAsync(eventId);
            var artist = await _context.Artists.FindAsync(artistId);

            if (currentEvent == null || artist == null) return;

            if (currentEvent.ArtistIds.Contains(artistId))
            {
                currentEvent.ArtistIds.Remove(artistId);
                await _context.SaveChangesAsync();
            }
        }

    }
}
