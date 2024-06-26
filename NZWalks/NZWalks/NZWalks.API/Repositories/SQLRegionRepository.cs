
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalkDbContext _dbcontext;
        public SQLRegionRepository(NZWalkDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Region> CreateAync(Region region)
        {
            await _dbcontext.Regions.AddAsync(region);
            await _dbcontext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteAync(Guid id)
        {
            var existingRegion = await _dbcontext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }
            else
            {
                 _dbcontext.Regions.Remove(existingRegion);
                await _dbcontext.SaveChangesAsync();
                return existingRegion;
            }
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await _dbcontext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAync(Guid id)
        {
            return await _dbcontext.Regions.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Region?> UpdateAync(Region region, Guid id)
        {
            var existingRegion = await _dbcontext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }
            else
            {
                existingRegion.Code = region.Code;
                existingRegion.Name = region.Name;
                existingRegion.RegionImageUrl = region.RegionImageUrl;

                _dbcontext.Regions.Update(existingRegion);
                await _dbcontext.SaveChangesAsync();
                return existingRegion;
            }
        }
    }
}
