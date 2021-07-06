using Catalog.DataTransferObjects;
using Catalog.Models;
using Catalog.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Services.Repository
{
    public interface IProductRepository
    {
        public Task<PagedList<ProductDto>> GetProductsAsync(ProductParameters parameters, bool trackChanges = false);
        public Task<T> GetProductAsync<T>(int productId, bool trackChanges = false);
        public Task<Product> CreateProductAsync(ProductDtoForCreation productDto);
        public Task UpdateProductAsync(ProductDtoForCreation productDto, Product product);
        public Task DeleteProductAsync(Product product);
    }
}
