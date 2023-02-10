
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderAPI.Entities;
using ProductAPI.Database;

namespace Order.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly OrderDbContext _context;

		public OrderController(OrderDbContext context)
		{
			_context = context;
		}


		//Create new order
		[HttpPost]
		public async Task<IActionResult> PostAsync(OrderModel newOrder)
		{ 
			_context.Orders.Add(newOrder);
			await _context.SaveChangesAsync();
			return CreatedAtAction("Get", new
			{
				OrderId = newOrder.ProductId,
			}, newOrder);
		}
		
		//Get Orders by Id
		[HttpGet("{id}")]
		public async Task<IActionResult> GetAsync(int id)
		{
			var order = await _context.Orders.FindAsync(id);

			if(order == null)
				return NotFound();

			return Ok(order);
		}


		//Get all orders
		[HttpGet]
		public async Task<IActionResult> GetAsync()
		{
			var orders = await _context.Orders.ToListAsync();
			return Ok(orders);
		}

		//update an order
		[HttpPut("{id}")]
		public async Task<IActionResult> PutAsync(int id, OrderModel request)
		{
			var order = await _context.Orders.FindAsync(id);

			if (order is null)
				return NotFound("Order not Found");

			order.ProductName = request.ProductName;
			order.ProductId = request.ProductId;
			order.OrderDate = request.OrderDate;
			order.UserId = request.UserId;

			await _context.SaveChangesAsync();

			return Ok(await _context.Orders.ToListAsync());
		}

		//Delete an order
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAsync(int id)
		{
			var order = await _context.Orders.FindAsync(id);

			if (order is null)
				return NotFound("Order not found!");

			_context.Orders.Remove(order);
			await _context.SaveChangesAsync();

			return Ok(await _context.Orders.ToListAsync());
		}
	}
        

}
