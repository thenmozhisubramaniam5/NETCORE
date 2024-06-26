using AutoMapper.Configuration.Annotations;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalkDbContext _nZWalkDbContext;

        public SQLWalkRepository(NZWalkDbContext nZWalkDbContext)
        {
            _nZWalkDbContext = nZWalkDbContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await _nZWalkDbContext.Walks.AddAsync(walk);
            await _nZWalkDbContext.SaveChangesAsync();
            return walk;
        }
        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var walkDomain = await _nZWalkDbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (walkDomain == null)
            {
                return null;
            }
            else
            {
                _nZWalkDbContext.Walks.Remove(walkDomain);
                await _nZWalkDbContext.SaveChangesAsync();
                return walkDomain;
            }
        }
        public async Task<List<Walk>> GetAllAsync(string? filterOn=null, string? filterQuery=null, string? soryBy = null, bool isAcending = true, int pageSize = 1000, int pageNumber = 1)
        {
            //return await _nZWalkDbContext.Walks.Include("Region").Include("Difficulty").ToListAsync();

            var walks = _nZWalkDbContext.Walks.Include("Region").Include("Difficulty").AsQueryable();

            //filtering
            if(string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }

            //sorting
            if (string.IsNullOrWhiteSpace(soryBy) == false)
            {
                if (soryBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAcending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                else
                {
                    walks = isAcending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            // Paging
            var skipResults = (pageNumber - 1) * pageSize;
            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();

          //  return await walks.ToListAsync();

        }

        public async Task<Walk?> GetByIdAync(Guid id)
        {
            return await _nZWalkDbContext.Walks.Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Walk walk, Guid id)
        {
            var exisitngWalk = await _nZWalkDbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (exisitngWalk == null)
            {
                return null;
            }

            exisitngWalk.Name = walk.Name;
            exisitngWalk.Description = walk.Description;
            exisitngWalk.LengthInKm = walk.LengthInKm;
            exisitngWalk.WalkImageIrl = walk.WalkImageIrl;
            exisitngWalk.DifficultyId = walk.DifficultyId;
            exisitngWalk.RegionId = walk.RegionId;

            await _nZWalkDbContext.SaveChangesAsync();

            return exisitngWalk;
        }
    }
}
