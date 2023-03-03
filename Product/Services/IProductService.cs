using Microsoft.AspNetCore.Mvc;
using ProductAPI.Dtos;
using ProductAPI.Model;

namespace ProductAPI.Services
{
	public interface IProductService
	{
		Task<List<Product>> GetAllProducts();
		Task<Product>? GetProductById(int id);
		Task<Product> AddProduct(Product product);
		Task<ActionResult<List<Product>>>? UpdateProduct(int id, ProductDto request);
		Task<ActionResult<string>> DeleteProduct(int id);
	}
}
