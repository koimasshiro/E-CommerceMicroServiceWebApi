using Microsoft.EntityFrameworkCore;

namespace OrderAPI.Entities
{
	public class OrderDbContext : DbContext
	{
		public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
		{
				
		}

		public DbSet<OrderModel> Orders => Set<OrderModel>();
	}
}
