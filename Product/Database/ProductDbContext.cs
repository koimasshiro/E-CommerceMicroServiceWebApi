﻿using Microsoft.EntityFrameworkCore;
using ProductAPI.Model;

namespace ProductAPI.Database
{
	public class ProductDbContext : DbContext
	{
		public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
		{

		}
		public DbSet<Product> Products => Set<Product>();
	}
}
