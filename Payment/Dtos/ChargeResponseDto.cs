namespace PaymentAPI.Dtos
{
	public class ChargeResponseDto
	{
		public bool Status { get; set; }
		public string Message { get; set; } = string.Empty;
		public Data Data { get; set; } = default!;
	}
}
