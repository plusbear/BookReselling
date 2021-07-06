using AutoMapper;
using Catalog.DataTransferObjects;
using Catalog.Infrastructure;
using Catalog.Models;
using Catalog.RequestFeatures;
using Catalog.Services.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public ProductsController(IRepositoryManager repositoryManager)
        {
            RepositoryManager = repositoryManager;
        }

        public IRepositoryManager RepositoryManager { get; set; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts([FromQuery] ProductParameters parameters)
        {
            if (parameters.IsCategorySet)
            {
                var categoryExists = await RepositoryManager.Category.CategoryExists(parameters.CategoryId.Value);
                if (!categoryExists)
                    return NotFound();
            }

            var pagedProducts = await RepositoryManager.Product.GetProductsAsync(parameters);
            Response.Headers.Add("X-Pagination",JsonSerializer.Serialize(pagedProducts.MetaData));

            return Ok(pagedProducts);
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int productId)
        {
            var product = await RepositoryManager.Product.GetProductAsync<ProductDto>(productId);
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDtoForCreation productDto)
        {
            var categoryExists = await RepositoryManager.Category.CategoryExists(productDto.CategoryId);
            if (!categoryExists)
                return BadRequest("No such category exists");

            try
            {
                var newProduct = await RepositoryManager.Product.CreateProductAsync(productDto);
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

            var product = await RepositoryManager.Product.GetProductAsync<Product>(productId);
            if (product == null)
                return NotFound();

            await RepositoryManager.Product.UpdateProductAsync(productDto, product);
            return NoContent();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var product = await RepositoryManager.Product.GetProductAsync<Product>(productId);
            if (product == null)
                return NotFound();

            await RepositoryManager.Product.DeleteProductAsync(product);
            return NoContent();
        }
    }
}
