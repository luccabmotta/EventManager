using EventManagementAPI.Models;

namespace EventManagementAPI.Interfaces
{
    public interface IEventRepository
    {
        IQueryable<Event> GetEvents();
        Task<Event?> GetByIdAsync(int id);
        Task AddAsync(Event @event);
        Task UpdateAsync(Event @event);
        Task DeleteAsync(Event @event);
        Task<bool> ExistsAsync(int id);
        Task AddArtistToEventAsync(int eventId, int artistId);
        Task RemoveArtistFromEventAsync(int eventId, int artistId);
    }
}
