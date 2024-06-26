using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();
        Task<Region?> GetByIdAync(Guid id); // nullable Region

        Task<Region> CreateAync(Region region);

        Task<Region?> UpdateAync(Region region,Guid id);
        Task<Region> DeleteAync(Guid id);

    }
}
