using EventManagementAPI.Interfaces;
using EventManagementAPI.Models;

namespace EventManagementAPI.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public IQueryable<Event> GetAll()
        {
            return _eventRepository.GetEvents();
        }

        public async Task<Event?> GetByIdAsync(int id)
        {
            return await _eventRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Event @event)
        {
            await _eventRepository.AddAsync(@event);
        }

        public async Task UpdateAsync(Event @event)
        {
            await _eventRepository.UpdateAsync(@event);
        }

        public async Task DeleteAsync(int id)
        {
            var evento = await _eventRepository.GetByIdAsync(id);

            if (evento != null)
            {
                await _eventRepository.DeleteAsync(evento);
            }
        }

        public async Task AddArtistToEventAsync(int eventId, int artistId)
        {
            await _eventRepository.AddArtistToEventAsync(eventId, artistId);
        }

        public async Task RemoveArtistFromEventAsync(int eventId, int artistId)
        {
            await _eventRepository.RemoveArtistFromEventAsync(eventId, artistId);
        }
    }
}
