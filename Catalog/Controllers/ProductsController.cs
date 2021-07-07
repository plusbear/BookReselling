using Catalog.DataTransferObjects;
using Catalog.Models;
using Catalog.RequestFeatures;
using Catalog.Services.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        
        public ProductsController(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }        

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts([FromQuery] ProductParameters parameters)
        {
            var pagedProducts = await _repositoryManager.Product.Get(parameters);
            Response.Headers.Add("X-Pagination",JsonSerializer.Serialize(pagedProducts.MetaData));

            return Ok(pagedProducts);
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int productId)
        {
            var product = await _repositoryManager.Product.Get<ProductDto>(productId);
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDtoForCreation productDto)
        {
            var categoryExists = await _repositoryManager.Category.IsExist(productDto.CategoryId);
            if (!categoryExists)
                return BadRequest("No such category exists");

            try
            {
                var newProduct = await _repositoryManager.Product.Create(productDto);
                return CreatedAtAction("GetProduct", new { categoryId = newProduct.CategoryId, productId = newProduct.Id }, newProduct);
            }
            catch
            {
                return UnprocessableEntity();
            }
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProduct(int productId, [FromBody] ProductDtoForCreation productDto)
        {
            if (productDto == null)
                return BadRequest("productDto was null");

            var product = await _repositoryManager.Product.Get<Product>(productId);
            if (product == null)
                return NotFound();

            await _repositoryManager.Product.Update(productDto, product);
            return NoContent();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var product = await _repositoryManager.Product.Get<Product>(productId);
            if (product == null)
                return NotFound();

            await _repositoryManager.Product.Delete(product);
            return NoContent();
        }
    }
}
