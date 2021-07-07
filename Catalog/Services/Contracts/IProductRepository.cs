using Catalog.DataTransferObjects;
using Catalog.Models;
using Catalog.RequestFeatures;
using System.Threading.Tasks;

namespace Catalog.Services.Repository
{
    public interface IProductRepository
    {
        public Task<T> Get<T>(int productId, bool trackChanges = false);
        public Task<PagedList<ProductDto>> Get(ProductParameters parameters, bool trackChanges = false);
        public Task<Product> Create(ProductDtoForCreation productDto);
        public Task Update(ProductDtoForCreation productDto, Product product);
        public Task Delete(Product product);
    }
}
