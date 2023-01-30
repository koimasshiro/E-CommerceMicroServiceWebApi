using ProductAPI.Dtos;
using ProductAPI.Model;

namespace ProductAPI.Services
{
	public interface IProductService
	{
		Task<List<Product>> GetAllProducts();
		Task<Product>? GetProductById(int id);
		Task<List<Product>> AddProduct(Product product);
		Task<List<Product>>? UpdateProduct(int id, Product request);
		Task<List<Product>>? DeleteProduct(int id);
	}
}
