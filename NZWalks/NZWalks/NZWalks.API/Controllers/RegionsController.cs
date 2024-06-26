using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Text.Json;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalkDbContext _dbcontext;
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(NZWalkDbContext dbContext, IRegionRepository regionRepository,IMapper mapper,ILogger<RegionsController> logger)
        {
            _dbcontext = dbContext;
            _regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
       // [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            logger.LogInformation("GetAll Region Action Method Invoked");
            logger.LogWarning("Warning");
            logger.LogError("Error");
            //var regions = new List<Region>
            //{
            //    new Region
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "Auckland Region",
            //        Code = "AKL",
            //        RegionImageUrl = "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpg"
            //    },
            //    new Region
            //    {
            //         Id = Guid.NewGuid(),
            //        Name = "Wellington Region",
            //        Code = "WLG",
            //        RegionImageUrl = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpg"
            //    }
            //};'

            //Get data from database --  Domain model
            //var regionDomain = await _dbcontext.Regions.ToListAsync();


            var regionDomain = await _regionRepository.GetAllAsync();

            logger.LogError($"GetAll Region Action Method Finished with Data : { JsonSerializer.Serialize(regionDomain)}");

            // Map domain model to dto using mapper
            var regionDto =  mapper.Map<List<RegionDto>>(regionDomain);

            //Map domain model to dto
            //var regionDto = new List<RegionDto>();
            //foreach (var region in regionDomain)
            //{
            //    regionDto.Add(new RegionDto()
            //    {
            //        Id = region.Id,
            //        Name = region.Name,
            //        Code = region.Code,
            //        RegionImageUrl = region.RegionImageUrl
            //    });
            //}

            // return DTOs
            return Ok(regionDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // var regionDomain = _dbcontext.Regions.Find(id); // only used id property, primary key
            //  var regionDomain = await _dbcontext.Regions.FirstOrDefaultAsync(x => x.Id == id);   // we can pass id,name,code etc.. 

            var regionDomain = await _regionRepository.GetByIdAync(id);

           var regionDto =  mapper.Map<RegionDto>(regionDomain);

            if (regionDomain == null)
            {
                return NotFound();
            }

            return Ok(regionDto);
        }

        // create Region
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Validation
            //if (ModelState.IsValid)
            //{
                //map from Dto to domain
                var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

                //var regionDomainModel = new Region
                //{
                //    Code = addRegionRequestDto.Code,
                //    Name = addRegionRequestDto.Name,
                //    RegionImageUrl = addRegionRequestDto.RegionImageUrl
                //};
                //await _dbcontext.Regions.AddAsync(regionDomainModel);
                //await _dbcontext.SaveChangesAsync();

                regionDomainModel = await _regionRepository.CreateAync(regionDomainModel);

                // map Domain model back to DTO
                var regionDto = mapper.Map<RegionDto>(regionDomainModel);

                //var regionDto = new RegionDto
                //{
                //    Id = regionDomainModel.Id,
                //    Name = regionDomainModel.Name,
                //    Code = regionDomainModel.Code,
                //    RegionImageUrl = regionDomainModel.RegionImageUrl
                //};
                return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
            //}
            //else
            //{
            //    return BadRequest(ModelState);
            //}


           
        }

        [HttpPost]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateRegionRequestDto updateRegionRequestDto)
        {
            // var regionDomainModel = await _dbcontext.Regions.FirstOrDefaultAsync(y => y.Id == id);
           var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

            //var regionDomainModel = new Region
            //{
            //    Code = updateRegionRequestDto.Code,
            //    Name = updateRegionRequestDto.Name,
            //    RegionImageUrl = updateRegionRequestDto.RegionImageUrl
            //};

            regionDomainModel = await _regionRepository.UpdateAync(regionDomainModel, id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //regionDomainModel.Code = updateRegionRequestDto.Code;
            //regionDomainModel.Name = updateRegionRequestDto.Name;
            //regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            //_dbcontext.Update(regionDomainModel);
            //await _dbcontext.SaveChangesAsync();

            // Map domain to Dto

            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            //var regionDto = new RegionDto
            //{
            //    Id = regionDomainModel.Id,
            //    Name = regionDomainModel.Name,
            //    Code = regionDomainModel.Code,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //};

            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
       // [Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            // var regionDomainModel = await _dbcontext.Regions.FirstOrDefaultAsync(y => y.Id == id);


            var regionDomainModel = await _regionRepository.DeleteAync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }
            //_dbcontext.Regions.Remove(regionDomainModel);
            //await _dbcontext.SaveChangesAsync();

            // domain to Dto

            var regionDto= mapper.Map<RegionDto>(regionDomainModel);

            //var regionDto = new RegionDto
            //{
            //    Id = regionDomainModel.Id,
            //    Name = regionDomainModel.Name,
            //    Code = regionDomainModel.Code,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //};

            return Ok(regionDto);

        }
    }
}
