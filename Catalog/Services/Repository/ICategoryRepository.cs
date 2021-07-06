using Catalog.DataTransferObjects;
using Catalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Services.Repository
{
    public interface ICategoryRepository
    {
        public Task<IEnumerable<Category>> GetCategoriesAsync(bool trackChanges = false);
        public Task<bool> CategoryExists(int categoryId, bool trackChanges = false);
        public Task<Category> CreateCategoryAsync(CategoryDtoForCreation categoryDto);
        public Task UpdateCategoryAsync(CategoryDtoForCreation categoryDto, Category category);
        public Task DeleteCategoryAsync(Category category);
    }
}
