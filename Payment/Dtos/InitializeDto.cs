namespace PaymentAPI.Dtos
{
	public class InitializeDto
	{
		public string email { get; set; } = string.Empty;
		public decimal amount { get; set; }
		public string currency { get; set; } = "NGN";
	}
}
