using Microsoft.EntityFrameworkCore;
using ProductAPI.Database;
using ProductAPI.Model;

namespace ProductAPI.Services
{
	public class ProductService : IProductService
	{
		private readonly ProductDbContext _context;
		public ProductService(ProductDbContext context)
		{
			_context = context;
		}

		public async Task<List<ProductModel>> AddProduct(ProductModel product)
		{
			_context.Products.Add(product);
			await _context.SaveChangesAsync();
			return await _context.Products.ToListAsync();
		}

		public async Task<List<ProductModel>>? DeleteProduct(int id)
		{
			var product = await _context.Products.FindAsync(id);

			if (product is null)
				return null;

			_context.Products.Remove(product);
			await _context.SaveChangesAsync();

			return await _context.Products.ToListAsync();
		}

		public async Task<List<ProductModel>> GetAllProducts()
		{
			var products = await _context.Products.ToListAsync();
			return products;
		}

		public async Task<ProductModel?> GetProductById(int id)
		{
			var product = await _context.Products.FindAsync(id);
			if (product is null)
				return null;

			return product;
		}

		public async Task<List<ProductModel?>> UpdateProduct(int id, ProductModel request)
		{
			var product = await _context.Products.FindAsync(id);

			if (product is null)
				return null;

			product.ProductName = request.ProductName;
			product.ProductDesc = request.ProductDesc;
			product.ProductPrice = request.ProductPrice;

			await _context.SaveChangesAsync();

			return await _context.Products.ToListAsync();
		}
	}
}
