using Amazon.S3;
using AutoMapper;
using Catalog.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Services.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        public RepositoryManager(CatalogContext catalogContext, IMapper autoMapper, IImageRepository imageRepository)
        {
            _catalogContext = catalogContext;
            _autoMapper = autoMapper;
            _imageRepository = imageRepository;
        }

        private CatalogContext _catalogContext;
        private IMapper _autoMapper;
        private IImageRepository _imageRepository;

        private IProductRepository _productRepository;
        private ICategoryRepository _categoryRepository;

        public IProductRepository Product
        {
            get
            {
                if (_productRepository == null)
                    _productRepository = new ProductRepository(_catalogContext, _autoMapper, _imageRepository);
                return _productRepository;
            }
        }

        public ICategoryRepository Category
        {
            get
            {
                if (_categoryRepository == null)
                    _categoryRepository = new CategoryRepository(_catalogContext, _autoMapper);
                return _categoryRepository;
            }
        }

        public async Task SaveAsync()
        {
            await _catalogContext.SaveChangesAsync();
        }
    }
}
