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

		[HttpGet("GetAllProducts")]
		public async Task<ActionResult<List<Product>>> GetAllProducts()
		{
			return await _productService.GetAllProducts();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProductById(int id)
		{
			var result = await _productService.GetProductById(id);

			if (result is null)
				return NotFound("Product not found");

			return Ok(result);
		}

		[HttpPost("AddProduct")]
		public async Task<ActionResult<List<Product>>> AddProduct(Product product)
		{
			var result = await _productService.AddProduct(product);
			return Ok(result);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<List<Product>>> UpdateProduct(int id, Product request)
		{
			var result = await _productService.UpdateProduct(id, request);

			if (result is null)
				return NotFound("Product not found");

			return Ok(result);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<List<Product>>> DeleteProduct(int id)
		{
			var result = await _productService.DeleteProduct(id);

			if (result is null)
				return NotFound("Product not found");

			return Ok(result);
		}


	}
}