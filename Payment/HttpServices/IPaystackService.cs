using PaymentAPI.Dtos;

namespace PaymentAPI.HttpServices
{
	public interface IPaystackService
	{
		Task<dynamic> InitializeTransaction(InitializeDto request);
		Task<VerifyResponseDto> VerifyTransaction(string reference);
		Task<ChargeResponseDto> ChargeTransaction(ChargeDto request);
	}
}
