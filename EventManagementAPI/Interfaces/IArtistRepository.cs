using EventManagementAPI.Models;

namespace EventManagementAPI.Interfaces
{
    public interface IArtistRepository
    {
        IQueryable<Artist> GetAll();
        Task<Artist?> GetByIdAsync(int id);
        Task AddAsync(Artist artist);
        Task UpdateAsync(Artist artist);
        Task DeleteAsync(Artist artist);
        Task<bool> ExistsAsync(int id);
    }
}
