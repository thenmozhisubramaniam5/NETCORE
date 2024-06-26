using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAcending = true, int pageSize = 1000, int pageNumber = 1);
        Task<Walk?> GetByIdAync(Guid id); // nullable Region
        Task<Walk?> UpdateAsync(Walk walk, Guid id);
        Task<Walk?> DeleteAsync(Guid id);
    }
}
