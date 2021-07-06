using AutoMapper;
using Catalog.DataTransferObjects;
using Catalog.Infrastructure;
using Catalog.Models;
using Catalog.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Services.Repository
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(CatalogContext catalogContext, IMapper autoMapper, IImageRepository imageRepository) : base(catalogContext, autoMapper)
        {
            _imageRepository = imageRepository;
        }

        private IImageRepository _imageRepository { get; set; }

        public async Task<T> GetProductAsync<T>(int productId, bool trackChanges)
        {
            var product = await FindByCondition(p => p.Id == productId, trackChanges).Include(p => p.Images).SingleOrDefaultAsync();
            var productDto = _autoMapper.Map<T>(product);
            return productDto;
        }

        public async Task<PagedList<ProductDto>> GetProductsAsync(ProductParameters parameters, bool trackChanges)
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

        public async Task<Product> CreateProductAsync(ProductDtoForCreation productDto)
        {
            var newProduct = _autoMapper.Map<Product>(productDto);

            Create(newProduct);
            await _catalogContext.SaveChangesAsync();

            await _imageRepository.AddImagesAsync(newProduct.Id, productDto.Images.ToList());

            return newProduct;
        }

        public async Task UpdateProductAsync(ProductDtoForCreation productDto, Product product)
        {
            _autoMapper.Map(productDto, product);
            await _catalogContext.SaveChangesAsync();
            await _imageRepository.UpdateImagesAsync(product.Id, productDto.Images.ToList());
        }

        public async Task DeleteProductAsync(Product product)
        {
            Delete(product);
            await _catalogContext.SaveChangesAsync();
            await _imageRepository.DeleteImagesAsync(product.Id);
        }
    }
}
