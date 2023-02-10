using ProductAPI.Model;
using System.ComponentModel.DataAnnotations;

namespace OrderAPI.Entities
{
	public class OrderModel
	{
		[Key]
		public int OrderId { get; set; }
		public int UserId { get; set; }
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public DateTime? OrderDate { get; set; }
	}
}
