namespace PaymentAPI.Dtos
{
	public class InitializeResponseDto
	{
		public bool Status { get; set; }
		public string Message { get; set; } = string.Empty;
		public Data Data { get; set; } = default!;
	}
}
