using Catalog.Services.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesTestController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        public ImagesTestController(IImageRepository repository)
        {
            _imageRepository = repository;
        }

        [HttpPost("{productId}")]
        public async Task<IActionResult> UploadImages(int productId)
        {
            var fileContent = System.IO.File.ReadAllText(@"C:\Users\user\Desktop\test.txt");
            var imgs = fileContent.Split('\n').ToList();

            await _imageRepository.Create(productId, imgs);
            return Ok();
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetImages(int productId)
        {
            var images = await _imageRepository.GetAll(productId);
            return Ok(new { imgCount = images.Count() } );
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteImages(int productId)
        {
            await _imageRepository.Delete(productId);
            return Ok();
        }
    }
}
