using EventManagementAPI.Interfaces;
using EventManagementAPI.Models;

namespace EventManagementAPI.Services
{
    public class GenreService : IGenreService
    {

        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<List<Genre>> GetAll()
        {
            return await _genreRepository.GetAll();
        }

    }
}
