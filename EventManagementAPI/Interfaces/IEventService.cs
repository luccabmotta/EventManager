using EventManagementAPI.Models;

namespace EventManagementAPI.Interfaces
{
    public interface IEventService
    {
        IQueryable<Event> GetAll();
        Task<Event?> GetByIdAsync(int id);
        Task AddAsync(Event @event);
        Task UpdateAsync(Event @event);
        Task DeleteAsync(int id);
        Task AddArtistToEventAsync(int eventId, int artistId);
        Task RemoveArtistFromEventAsync(int eventId, int artistId);
    }
}
