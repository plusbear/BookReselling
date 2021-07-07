using AutoMapper;
using Catalog.Infrastructure;
using System.Threading.Tasks;

namespace Catalog.Services.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly CatalogContext _catalogContext;
        private readonly IMapper _autoMapper;
        private IProductRepository _productRepository;
        private ICategoryRepository _categoryRepository;
        private IImageRepository _imageRepository;

        public RepositoryManager(CatalogContext catalogContext, IMapper autoMapper, IImageRepository imageRepository)
        {
            _catalogContext = catalogContext;
            _autoMapper = autoMapper;
            _imageRepository = imageRepository;
        }

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

        public async Task Save()
        {
            await _catalogContext.SaveChangesAsync();
        }
    }
}
