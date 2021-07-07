using AutoMapper;
using Catalog.DataTransferObjects;
using Catalog.Infrastructure;
using Catalog.Models;
using Catalog.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Services.Repository
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        private readonly IImageRepository _imageRepository;

        public ProductRepository(CatalogContext catalogContext, IMapper autoMapper, IImageRepository imageRepository) : base(catalogContext, autoMapper)
        {
            _imageRepository = imageRepository;
        }

        public async Task<T> Get<T>(int productId, bool trackChanges)
        {
            var product = await FindByCondition(p => p.Id == productId, trackChanges).Include(p => p.Images).SingleOrDefaultAsync();
            var productDto = _autoMapper.Map<T>(product);
            return productDto;
        }

        public async Task<PagedList<ProductDto>> Get(ProductParameters parameters, bool trackChanges)
        {
            var products = new List<Product>();
            if (parameters.IsCategorySet)
            {
                products = await FindAll(trackChanges)
                    .Where(p => p.CategoryId == parameters.CategoryId)
                    .Include(p => p.Images)
                    .ToListAsync();
            }
            else
            {
                products = await FindAll(trackChanges).Include(p => p.Images).ToListAsync();
            }

            var productsDto = _autoMapper.Map<IEnumerable<ProductDto>>(products);

            var pagedProducts = PagedList<ProductDto>.ToPagedList(productsDto, parameters.PageNumber, parameters.PageSize);
            return pagedProducts;
        }

        public async Task<Product> Create(ProductDtoForCreation productDto)
        {
            var newProduct = _autoMapper.Map<Product>(productDto);

            CreateEntity(newProduct);
            await _catalogContext.SaveChangesAsync();

            await _imageRepository.Create(newProduct.Id, productDto.Images.ToList());

            return newProduct;
        }

        public async Task Update(ProductDtoForCreation productDto, Product product)
        {
            _autoMapper.Map(productDto, product);
            await _catalogContext.SaveChangesAsync();
            await _imageRepository.Update(product.Id, productDto.Images.ToList());
        }

        public async Task Delete(Product product)
        {
            DeleteEntity(product);
            await _catalogContext.SaveChangesAsync();
            await _imageRepository.Delete(product.Id);
        }
    }
}
