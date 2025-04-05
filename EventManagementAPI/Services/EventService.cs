using EventManagementAPI.Interfaces;
using EventManagementAPI.Models;
using EventManagementAPI.Repositories;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

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

        public async Task<bool> UpdateAsync(int id, Event @event)
        {
            if (!await _eventRepository.ExistsAsync(id))
            {
                return false;
            }

            @event.Id = id;

            await _eventRepository.UpdateAsync(@event);

            return true;
        }

        public async Task<bool> PatchEventAsync(int id, JObject patch)
        {
            var @event = await _eventRepository.GetByIdAsync(id);

            if (@event == null) return false;

            JsonConvert.PopulateObject(patch.ToString(), @event);

            await _eventRepository.UpdateAsync(@event);

            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var evento = await _eventRepository.GetByIdAsync(id);

            if (evento != null)
            {
                await _eventRepository.DeleteAsync(evento);
            }
        }
    }
}
