using EventManagementAPI.Models;
using Newtonsoft.Json.Linq;

namespace EventManagementAPI.Interfaces
{
    public interface IEventService
    {
        IQueryable<Event> GetAll();
        Task<Event?> GetByIdAsync(int id);
        Task AddAsync(Event @event);
        Task<bool> UpdateAsync(int id, Event @event);
        Task<bool> PatchEventAsync(int id, JObject patch);
        Task DeleteAsync(int id);
    }
}
