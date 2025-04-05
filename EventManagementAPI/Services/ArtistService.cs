namespace EventManagementAPI.Services
{
    using global::EventManagementAPI.Interfaces;
    using global::EventManagementAPI.Models;
    using global::EventManagementAPI.Repositories;
    using Microsoft.AspNetCore.JsonPatch;
    using Newtonsoft.Json.Linq;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;

    namespace EventManagementAPI.Services
    {
        public class ArtistService : IArtistService
        {
            private readonly IArtistRepository _artistRepository;

            public ArtistService(IArtistRepository artistRepository)
            {
                _artistRepository = artistRepository;
            }

            public IQueryable<Artist> GetAll()
            {
                return _artistRepository.GetAll();
            }

            public async Task<Artist?> GetArtistByIdAsync(int id)
            {
                return await _artistRepository.GetByIdAsync(id);
            }

            public async Task<Artist> CreateArtistAsync(Artist artist)
            {
                await _artistRepository.AddAsync(artist);
                return artist;
            }

            public async Task<bool> UpdateArtistAsync(int id, Artist artist)
            {
                if (!await _artistRepository.ExistsAsync(id))
                {
                    return false;
                }

                artist.Id = id;

                await _artistRepository.UpdateAsync(artist);

                return true;
            }

            public async Task<bool> PatchArtistAsync(int id, JObject patch)
            {
                var artist = await _artistRepository.GetByIdAsync(id);

                if (artist == null) return false;

                JsonConvert.PopulateObject(patch.ToString(), artist);

                await _artistRepository.UpdateAsync(artist);

                return true;
            }

            public async Task<bool> DeleteArtistAsync(int id)
            {
                var artist = await _artistRepository.GetByIdAsync(id);

                if (artist == null)
                {
                    return false;
                }

                await _artistRepository.DeleteAsync(artist);
                return true;
            }
        }
    }

}
