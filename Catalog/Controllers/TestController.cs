using Catalog.Services.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public TestController(IImageRepository repository)
        {
            ImageRepository = repository;
            //ImageRepository.Bucket = "mybucket";
        }
        public IImageRepository ImageRepository { get; set; }

        [HttpPost("{productId}")]
        public async Task<IActionResult> UploadImages(int productId)
        {
            var fileContent = System.IO.File.ReadAllText(@"C:\Users\user\Desktop\test.txt");
            var imgs = fileContent.Split('\n').ToList();
            //var img = Convert.ToBase64String(data);

            await ImageRepository.AddImagesAsync(productId, imgs);
            return Ok();
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetImages(int productId)
        {
            var images = await ImageRepository.GetImagesAsync(productId);
            //foreach (var img in images)
                //System.IO.File.WriteAllBytes($"{counter++}.jpg", Convert.FromBase64String(img));
            return Ok(new { imgCount = images.Count() } );
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteImages(int productId)
        {
            await ImageRepository.DeleteImagesAsync(productId);
            return Ok();
        }
    }
}
