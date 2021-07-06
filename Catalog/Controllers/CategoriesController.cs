using AutoMapper;
using Catalog.DataTransferObjects;
using Catalog.Infrastructure;
using Catalog.Models;
using Catalog.Services.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        public CategoriesController(CatalogContext catalogContext, IRepositoryManager repositoryManager)
        {
            CatalogContext = catalogContext;
            RepositoryManager = repositoryManager;
        }

        public CatalogContext CatalogContext { get; set; }
        public IRepositoryManager RepositoryManager { get; set; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await RepositoryManager.Category.GetCategoriesAsync();
            return Ok(categories);
        }
    }
}
