using EventManagementAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace EventManagementAPI.Interfaces
{
    public interface IArtistService
    {
        IQueryable<Artist> GetAll();
        Task<Artist?> GetArtistByIdAsync(int id);
        Task<Artist> CreateArtistAsync(Artist artist);
        Task<bool> UpdateArtistAsync(int id, Artist artist);
        Task<bool> PatchArtistAsync(int id, JObject mergePatch);
        Task<bool> DeleteArtistAsync(int id);
    }
}
