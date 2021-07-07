using Amazon.S3;
using Amazon.S3.Model;
using Catalog.Infrastructure;
using Catalog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Services.Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly IAmazonS3 _S3;
        private readonly CatalogContext _catalogContext;

        public ImageRepository([FromServices] CatalogContext catalogContext, [FromServices] IAmazonS3 s3)
        {
            _S3 = s3;
            _catalogContext = catalogContext;
        }

        public string Bucket { get; set; } = "mybucket";
        public string Prefix { get; set; } = "product-";

        private async Task CreateBucketAsync(string bucket)
        {
            var request = new PutBucketRequest()
            {
                BucketName = bucket,
                UseClientRegion = true,
                CannedACL = S3CannedACL.PublicRead
            };
            await _S3.PutBucketAsync(request);
            Bucket = bucket;
        }

        public async Task<List<string>> GetAll(int productId)
        {
            return await _catalogContext.Images.Where(i => i.ProductId == productId).Select(i => i.ImageRef).ToListAsync();
        }

        public async Task Create(int productId, List<string> b64Images)
        {
            if (!await _S3.DoesS3BucketExistAsync(Bucket))
                await CreateBucketAsync(Bucket);

            var counter = 0;

            foreach (var img in b64Images)
            {
                //define filename for new image
                var ext = "";
                if (img.StartsWith("iVBORw0KGgo"))
                    ext = ".png";
                else if (img.StartsWith("/9j/"))
                    ext = ".jpg";
                else throw new Exception("Unsupported image type.");

                var imgName = $"{counter++}{ext}";
                var key = $"{Prefix}{productId}/" + imgName;

                //create stream for image
                byte[] bArray = Convert.FromBase64String(img);
                using var ms = new MemoryStream(bArray);     

                //compose a put request for image 
                var request = new PutObjectRequest
                {
                    BucketName = Bucket,
                    CannedACL = S3CannedACL.PublicRead,
                    Key = key,
                    InputStream = ms,
                    UseChunkEncoding = false
                };

                //save image to S3, then to db
                await _S3.PutObjectAsync(request);
                var imgUrl = _S3.Config.ServiceURL + '/' + Bucket + '/' + key;
                var image = new Image { ProductId = productId, ImageName = imgName, ImageRef = imgUrl };
                _catalogContext.Add(image);
                await _catalogContext.SaveChangesAsync();
            }
        }

        public async Task Delete(int productId)
        {
            if (!await _S3.DoesS3BucketExistAsync(Bucket))
                return;

            var productImages = _catalogContext.Images.Where(i => i.ProductId == productId);
            if (!productImages.Any())
                return;

            foreach (var image in productImages)
            {
                var request = new DeleteObjectRequest
                {
                    BucketName = Bucket,
                    Key = $"{Prefix}{productId}" + "/" + image.ImageName
                };
                
                await _S3.DeleteObjectAsync(request);
                _catalogContext.Images.Remove(image);    
            }

            await _catalogContext.SaveChangesAsync();
        }

        public async Task DeleteByName(int productId, List<string> imagesNames)
        {
            if (!await _S3.DoesS3BucketExistAsync(Bucket))
                return;

            var productImages = _catalogContext.Images.Where(i => i.ProductId == productId && imagesNames.Contains(i.ImageName));
            if (productImages.Count() == 0)
                return;

            foreach (var image in productImages)
            {
                var request = new DeleteObjectRequest
                {
                    BucketName = Bucket,
                    Key = $"{Prefix}{productId}" + "/" + image.ImageName
                };

                await _S3.DeleteObjectAsync(request);
                _catalogContext.Images.Remove(image);
            }

            await _catalogContext.SaveChangesAsync();
        }

        public async Task Update(int productId, List<string> b64Images)
        {
            await Delete(productId);
            await Create(productId, b64Images);
        }
    }
}
