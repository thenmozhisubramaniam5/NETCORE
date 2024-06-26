using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Net;
using System.Reflection.Metadata.Ecma335;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalkController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalkController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        // create a new Walk
        // Post method
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

            walkDomainModel = await walkRepository.CreateAsync(walkDomainModel);

            var walkDto = mapper.Map<WalkDto>(walkDomainModel);

            return Ok(walkDto);
        }

        // Get the all walks
        // GET: /Api/Walks
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, string? sortby, bool isAcending = true, int pageZize = 1000, int pageNumber = 1)
        {
            //try
            //{
            //    throw new Exception("This is the Error");

                var walkDomainModel = await walkRepository.GetAllAsync(filterOn, filterQuery, sortby, isAcending, pageZize, pageNumber);

                var walkDto = mapper.Map<List<WalkDto>>(walkDomainModel);

                throw new Exception("This is the GetAll Error");

            return Ok(walkDto);
            //}
            //catch (Exception ex)
            //{
            //    // Log this exception
            //    return Problem("Something went wrong", null, (int)HttpStatusCode.InternalServerError);
            //    //throw;
            //}
                    

        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var walkDomainModel = await walkRepository.GetByIdAync(id);

            var walkDto = mapper.Map<WalkDto>(walkDomainModel);

            return Ok(walkDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {

            // map dto to domain
            var walkDomain = mapper.Map<Walk>(updateWalkRequestDto);

            walkDomain = await walkRepository.UpdateAsync(walkDomain, id);

            if (walkDomain == null)
            {
                return NotFound();
            }

            // map domain to dto
            var walkDto = mapper.Map<WalkDto>(walkDomain);

            return Ok(walkDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var walkDomain = await walkRepository.DeleteAsync(id);
            return Ok(mapper.Map<WalkDto>(walkDomain));
        }
    }
}
