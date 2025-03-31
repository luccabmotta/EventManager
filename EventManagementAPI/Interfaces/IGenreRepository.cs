using EventManagementAPI.Models;

namespace EventManagementAPI.Interfaces
{
    public interface IGenreRepository
    {
        Task<List<Genre>> GetAll();
    }
}
