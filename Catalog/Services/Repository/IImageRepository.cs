using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Services.Repository
{
    public interface IImageRepository
    {
        public string Bucket { get; set; }
        public Task<List<string>> GetImagesAsync(int productId);
        public Task AddImagesAsync(int productId, List<string> b64Images);
        public Task UpdateImagesAsync(int productId, List<string> b64Images);
        public Task DeleteImagesAsync(int productId);
        public Task DeleteImagesAsync(int productId, List<string> imagesNames);
    }
}
