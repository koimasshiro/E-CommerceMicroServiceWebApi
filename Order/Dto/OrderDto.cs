namespace OrderAPI.Dto
{
	public class OrderDto
	{
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public DateTime? OrderDate { get; set; }
	}
}
