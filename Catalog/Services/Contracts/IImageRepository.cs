using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Services.Repository
{
    public interface IImageRepository
    {
        public string Bucket { get; set; }
        public string Prefix { get; set; }
        public Task<List<string>> GetAll(int productId);
        public Task Create(int productId, List<string> b64Images);
        public Task Update(int productId, List<string> b64Images);
        public Task Delete(int productId);
        public Task DeleteByName(int productId, List<string> imagesNames);
    }
}
