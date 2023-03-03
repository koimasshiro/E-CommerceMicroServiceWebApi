using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Dtos;
using ProductAPI.Model;
using ProductAPI.Services;
using ProductAPI.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Azure.Core;

namespace ProductAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductService _productService;
		public ProductController(IProductService productService)
		{
			_productService = productService;
		}

		[HttpGet]
		public async Task<ActionResult<List<Product>>> GetAllProducts()
		{
			return await _productService.GetAllProducts();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProductById(int id)
		{
			var result = await _productService.GetProductById(id);

			if (result == null)
				return NotFound("Product not found!!😞");

			return Ok(result);
		}

		[HttpPost]
		public async Task<Product> AddProduct(Product product)
		{
			var result = await _productService.AddProduct(product);

			return result;
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<List<Product>>> UpdateProduct(int id, ProductDto request)
		{
			var result = await _productService.UpdateProduct(id, request);

			if (result == null)
				return NotFound("Product not found!!😞");

			return Ok(result);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<string>> DeleteProduct(int id)
		{
			var result = await _productService.DeleteProduct(id);

			if (result == null)
				return NotFound("Product not found😞");

			return Ok(result);
		}
	}
}