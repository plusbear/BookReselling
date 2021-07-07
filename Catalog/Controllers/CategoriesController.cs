using Catalog.Infrastructure;
using Catalog.Models;
using Catalog.Services.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;

        public CategoriesController(CatalogContext catalogContext, IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await _repositoryManager.Category.GetAll();
            return Ok(categories);
        }
    }
}
