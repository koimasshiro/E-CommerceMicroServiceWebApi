using Microsoft.AspNetCore.Mvc;
using OrderAPI.Dto;
using OrderAPI.Entities;

namespace OrderAPI.Services
{
	public interface IOrderService
	{
		Task<dynamic> CreateOrder(OrderModel newOrder);
		Task<dynamic> GetOrderById(int id);
		Task<dynamic> GetAllOrder();
		Task<dynamic> UpdateOrder(int id, OrderDto request);
		Task<dynamic> DeleteOrder(int id);
	}
}
