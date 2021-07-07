using AutoMapper;
using Catalog.DataTransferObjects;
using Catalog.Infrastructure;
using Catalog.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Services.Repository
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(CatalogContext catalogContext, IMapper autoMapper) : base(catalogContext, autoMapper)
        {

        }

        public async Task<bool> IsExist(int categoryId, bool trackChanges)
        {
            var category = await FindByCondition(c => c.Id == categoryId, trackChanges).SingleOrDefaultAsync();
            if (category == null)
                return false;
            return true;
        }

        public async Task<IEnumerable<Category>> GetAll(bool trackChanges)
        {
            var categories = await FindAll(trackChanges).ToListAsync();
            return categories;

        }

        public async Task<Category> Create(string name)
        {
            var newCategory = new Category { Name = name };
            CreateEntity(newCategory);
            await _catalogContext.SaveChangesAsync();
            return newCategory;
        }

        public async Task Update(string name, Category category)
        {
            category.Name = name;
            await _catalogContext.SaveChangesAsync();
        }

        public async Task Delete(Category category)
        {
            DeleteEntity(category);
            await _catalogContext.SaveChangesAsync();
        }
    }
}
