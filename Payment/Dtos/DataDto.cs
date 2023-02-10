using Paystack.Net.SDK;

namespace PaymentAPI.Dtos
{
	public class DataDto
	{
		public long id { get; set; }
		public string domain { get; set; }
		public string status { get; set; } 
		public string reference { get; set; } 
		public int amount { get; set; }
		public string message { get; set; } 
		public string gateway_response { get; set; }
		public DateTime? paid_at { get; set; }
		public DateTime created_at { get; set; }
		public string channel { get; set; }
		public string currency { get; set; } 
		public string ip_address { get; set; } 
		public string metadata { get; set; } 
		public Log log { get; set; } 
		public object fees { get; set; }
		public Authorization authorization { get; set; } 
		public Customer customer { get; set; } 
		public string plan { get; set; }
		//public string FeesSplit { get; set; } = string.Empty;
		//public string OrderId { get; set; } = string.Empty;
		//public int RequestedAmount { get; set; }
		//public object PosTransactionData { get; set; } = default!;
		//public object Source { get; set; } = default!;
		//public object FeesBreakdown { get; set; } = default!;
		//public DateTime TransactionDate { get; set; } = default!;
	}
}
