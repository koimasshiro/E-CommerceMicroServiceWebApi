using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderAPI.Dto;
using OrderAPI.Entities;

namespace OrderAPI.Services
{
	public class OrderService : IOrderService
	{
		private readonly OrderDbContext _context;

		public OrderService(OrderDbContext context)
		{
			_context = context;
		}

		public async Task<dynamic> CreateOrder(OrderModel newOrder)
		{
			OrderModel order = new();

			order.OrderId = newOrder.OrderId;
			order.UserId = newOrder.UserId;
			order.ProductId = newOrder.ProductId;
			order.ProductName = newOrder.ProductName;
			order.OrderDate = newOrder.OrderDate;

			_context.Orders.Add(order);
		    await _context.SaveChangesAsync();

			return "Order created successfully";
		}

		public async Task<dynamic> GetOrderById(int id)
		{
			var order = await _context.Orders.FindAsync(id);

			if (order == null)
				return null;

			return order;
		}

		public async Task<dynamic> GetAllOrder()
		{
			var orders = await _context.Orders.ToListAsync();
			return orders;
		}

		public async Task<dynamic> UpdateOrder(int id, OrderDto request)
		{
			var order = await _context.Orders.FindAsync(id);

			if (order is null)
				return null;

			order.ProductName = request.ProductName;
			order.ProductId = request.ProductId;
			order.OrderDate = request.OrderDate;

			await _context.SaveChangesAsync();

			return await _context.Orders.ToListAsync();
		}

		public async Task<dynamic> DeleteOrder(int id)
		{
			var order = await _context.Orders.FindAsync(id);

			if (order is null)
				return null;

			_context.Orders.Remove(order);
			await _context.SaveChangesAsync();

			return await _context.Orders.ToListAsync();
		}
	}
}
