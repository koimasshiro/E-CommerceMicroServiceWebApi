namespace PaymentAPI.Dtos
{
	public class VerifyResponseDto
	{
		public bool status { get; set; }
		public string message { get; set; } 
		public DataDto data { get; set; } 
	}
}
