using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PaymentAPI.Dtos;
using PaymentAPI.HttpServices;
using System.Net.Http;
using System.Text;

namespace PaymentAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PaymentController : ControllerBase
	{
		private readonly IPaystackService _paystackService;
		private readonly ILogger<PaystackService> _logger;
		private readonly IConfiguration _configuration;
		private readonly IHttpClientFactory _httpClientFactory;


		public PaymentController(IPaystackService paystackService, IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<PaystackService> logger)
		{
			_paystackService = paystackService;
			_logger = logger;
			_configuration = configuration;
			_httpClientFactory = httpClientFactory;
		}

		[HttpPost("InitializeTransaction")]
		public async Task<dynamic> InitializeTransaction(InitializeDto request)
		{
			var result = await _paystackService.InitializeTransaction(request);

			return result!;
		}

		[HttpGet("VerifyTransaction")]
		public async Task<VerifyResponseDto> VerifyTransaction(string reference)
		{
			var result = await _paystackService.VerifyTransaction(reference);

			return result;
		}

		[HttpPost("ChargeTransaction")]
		public async Task<ChargeResponseDto> ChargeTransaction(ChargeDto request)
		{
			var result =  await _paystackService.ChargeTransaction(request);
			return result;
		}

		
	}
}
