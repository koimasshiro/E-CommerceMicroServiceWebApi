namespace PaymentAPI.Dtos
{
	public class ChargeDto
	{
		public string Email { get; set; } = string.Empty;
		public string Amount { get; set; } = string.Empty;
		public string AuthorizationCode { get; set; } = string.Empty;
	}
}
