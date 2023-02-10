namespace PaymentAPI.Dtos
{
	public class Log
	{
		public int time_spent { get; set; }
		public int attempts { get; set; }
		public string authentication { get; set; }
		public int errors { get; set; }
		public bool success { get; set; }
		public bool mobile { get; set; }
		public object[] input { get; set; }
		public object channel { get; set; }
		public History[] history { get; set; }
	}
}
