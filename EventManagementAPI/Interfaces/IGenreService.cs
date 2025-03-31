using EventManagementAPI.Models;

namespace EventManagementAPI.Interfaces
{
    public interface IGenreService
    {
        Task<List<Genre>> GetAll();
    }
}
