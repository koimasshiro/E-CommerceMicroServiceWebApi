using System.ComponentModel.DataAnnotations;

namespace ProductAPI.Model
{
	public class Product
	{
		[Key]
		public int ProductId { get; set; }
		public string ProductName { get; set; } = string.Empty;
		public string ProductDesc { get; set; } = string.Empty;
		public double ProductPrice { get; set; }
	}
}
