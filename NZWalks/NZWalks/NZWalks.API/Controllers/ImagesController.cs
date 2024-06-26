using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }
        // POST: api/Images/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto  imageUploadRequestDto)
        {
            ValidateFileUpload(imageUploadRequestDto);
            if(ModelState.IsValid)
            {
               
                var imageDomainModel = new Images
                {
                    File = imageUploadRequestDto.File,
                    FileDescription = imageUploadRequestDto.FileDescription,
                    FileExtension = Path.GetExtension(imageUploadRequestDto.File.FileName),
                    FileName = imageUploadRequestDto.FileName,
                    FileSizeInBytes = imageUploadRequestDto.File.Length
                };

                // User repository to upload the file
                await imageRepository.Upload(imageDomainModel);
                return Ok (imageDomainModel);


            }
            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadRequestDto imageUploadRequestDto)
        {
            var allowedExtension = new string[] { ".jpg", ".jpeg", ".png" };

            if(!allowedExtension.Contains(Path.GetExtension(imageUploadRequestDto.File.FileName)))
            {
                ModelState.AddModelError("File", "Upsupported File Extension");
            }

            if(imageUploadRequestDto.File.Length > 10485760) // 10 MB to Bytes
            {
                ModelState.AddModelError("File", "File size more than 10MB. Please upload below 10B file.");
            }
        }
    }
}
