using ProductAPI.Dtos;
using ProductAPI.Model;

namespace ProductAPI.Services
{
	public interface IProductService
	{
		Task<List<ProductModel>> GetAllProducts();
		Task<ProductModel>? GetProductById(int id);
		Task<List<ProductModel>> AddProduct(ProductModel product);
		Task<List<ProductModel>>? UpdateProduct(int id, ProductModel request);
		Task<List<ProductModel>>? DeleteProduct(int id);
	}
}
