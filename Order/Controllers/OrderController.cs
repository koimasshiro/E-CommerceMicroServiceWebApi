
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderAPI.Dto;
using OrderAPI.Entities;
using OrderAPI.Services;
using ProductAPI.Database;

namespace Order.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IOrderService _orderService;

		public OrderController(IOrderService order)
		{
			_orderService = order;
		}


		//Create new order
		[HttpPost]
		public async Task<IActionResult> CreateOrder(OrderModel newOrder)
		{
			var result = await _orderService.CreateOrder(newOrder);

			return Ok(result);
		}
		
		//Get Orders by Id
		[HttpGet("{id}")]
		public async Task<dynamic> GetOrderById(int id)
		{
			var result =  await _orderService.GetOrderById(id);
			return result;
		}


		//Get all orders
		[HttpGet]
		public async Task<dynamic> GetAllOrder()
		{
			var result = await _orderService.GetAllOrder();

			return Ok(result);
		}

		//update an order
		[HttpPut("{id}")]
		public async Task<dynamic> UpdateOrder(int id, OrderDto request)
		{
			var result = await _orderService.UpdateOrder(id, request);

			if (result is null)
				return NotFound("Order not Found");

			return Ok(result);
		}

		//Delete an order
		[HttpDelete("{id}")]
		public async Task<dynamic> DeleteOrder(int id)
		{
			var result = await _orderService.DeleteOrder(id);

			if (result is null)
				return NotFound("Order not found!");

			return Ok(result);
		}
	}
        

}
