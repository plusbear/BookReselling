using AutoMapper;
using Catalog.DataTransferObjects;
using Catalog.Infrastructure;
using Catalog.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Services.Repository
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(CatalogContext catalogContext, IMapper autoMapper) : base(catalogContext, autoMapper)
        {

        }

        public async Task<bool> CategoryExists(int categoryId, bool trackChanges)
        {
            var category = await FindByCondition(c => c.Id == categoryId, trackChanges).SingleOrDefaultAsync();
            if (category == null)
                return false;
            return true;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync(bool trackChanges)
        {
            var categories = await FindAll(trackChanges).ToListAsync();
            return categories;

        }

        public async Task<Category> CreateCategoryAsync(CategoryDtoForCreation categoryDto)
        {
            var newCategory = _autoMapper.Map<Category>(categoryDto);
            Create(newCategory);
            await _catalogContext.SaveChangesAsync();
            return newCategory;
        }

        public async Task UpdateCategoryAsync(CategoryDtoForCreation categoryDto, Category category)
        {
            _autoMapper.Map(categoryDto, category);
            await _catalogContext.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(Category category)
        {
            Delete(category);
            await _catalogContext.SaveChangesAsync();
        }
    }
}
