using Catalog.DataTransferObjects;
using Catalog.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Services.Repository
{
    public interface ICategoryRepository
    {
        public Task<bool> IsExist(int categoryId, bool trackChanges = false);
        public Task<IEnumerable<Category>> GetAll(bool trackChanges = false);
        public Task<Category> Create(string name);
        public Task Update(string name, Category category);
        public Task Delete(Category category);
    }
}
