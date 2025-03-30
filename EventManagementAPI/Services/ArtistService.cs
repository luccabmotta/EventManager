namespace EventManagementAPI.Services
{
    using global::EventManagementAPI.Interfaces;
    using global::EventManagementAPI.Models;
    using global::EventManagementAPI.Repositories;
    using Microsoft.AspNetCore.JsonPatch;
    using System.Collections.Generic;
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
                if (id != artist.Id || !await _artistRepository.ExistsAsync(id))
                {
                    return false;
                }

                await _artistRepository.UpdateAsync(artist);
                return true;
            }

            public async Task<bool> PatchArtistAsync(int id, JsonPatchDocument<Artist> patchDoc)
            {
                var artist = await _artistRepository.GetByIdAsync(id);

                if (artist == null)
                {
                    return false;
                }

                patchDoc.ApplyTo(artist);
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
