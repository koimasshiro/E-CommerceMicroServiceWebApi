using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderAPI.Entities;

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

		[HttpGet("{id}")]
		public async Task<IActionResult> GetAsync(int id)
		{
			var order = await _context.Orders.FindAsync(id);

			if(order == null)
				return NotFound();

			return Ok(order);
		}

		[HttpGet]
		public async Task<IActionResult> GetAsync()
		{
			var orders = await _context.Orders.ToListAsync();
			return Ok(orders);
		}

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
	}


}
