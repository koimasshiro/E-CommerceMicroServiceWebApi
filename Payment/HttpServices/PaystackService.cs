using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PaymentAPI.Dtos;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace PaymentAPI.HttpServices
{
	public class PaystackService : IPaystackService
	{
		private readonly ILogger<PaystackService> _logger;
		private readonly IConfiguration _configuration;
		private readonly IHttpClientFactory _httpClientFactory;

		public PaystackService(ILogger<PaystackService> logger, IConfiguration configuration, IHttpClientFactory httpClientFactory)
		{
			_logger = logger;
			_configuration = configuration;
			_httpClientFactory = httpClientFactory;
		}

		public async Task<dynamic> InitializeTransaction(InitializeDto request)
		{
			var secretKey = _configuration["Paystack:secretKey"];
			var result = new InitializeResponseDto();

			var client = _httpClientFactory.CreateClient("Paystack");

			client.DefaultRequestHeaders.Add("Authorization", $"Bearer {secretKey}");

			var endPoint = "/transaction/initialize";

			var content = await FormatContent(request);

			var response = await client.PostAsync(endPoint, content);

			var resultString = await response.Content.ReadAsStringAsync();

			if (response.IsSuccessStatusCode)
			{
				_logger.LogInformation("Post successful");
				result = JsonConvert.DeserializeObject<InitializeResponseDto>(resultString);
			}
			else
			{
				_logger.LogInformation("Post Unsuccessful, content: {result}, statuscode : {code}",
					resultString, (int)response.StatusCode);
			}
			return result!;
		}

		public async Task<VerifyResponseDto> VerifyTransaction(string reference)
		{
			var result = new VerifyResponseDto();

			var client = _httpClientFactory.CreateClient("Paystack");

			var secretKey = _configuration["Paystack:secretKey"];

			client.DefaultRequestHeaders.Add("Authorization", $"Bearer {secretKey}");
				
			var endPoint = $"/transaction/verify/{reference}";

			var response = await client.GetAsync(endPoint);

			var resultString = await response.Content.ReadAsStringAsync();

			if (response.IsSuccessStatusCode)
			{
				_logger.LogInformation("Verification Successful");
				result = JsonConvert.DeserializeObject<VerifyResponseDto>(resultString); //JsonSerializer.Deserialize<VerifyResponseDto>(resultString);
			}
			else
			{
				_logger.LogInformation("Verification Unsuccessful, content: {result}, statuscode : {code}",
					resultString, (int)response.StatusCode);
			}
			return result!;
		}

		public async Task<ChargeResponseDto> ChargeTransaction(ChargeDto request)
		{
			var secretKey = _configuration["Paystack:secretKey"];

			var result = new ChargeResponseDto();

			var client = _httpClientFactory.CreateClient("Paystack");

			var serialize = JsonConvert.SerializeObject(request);

			client.DefaultRequestHeaders.Add("Authorization", $"Bearer {secretKey}");
			var endPoint = "/transaction/charge_authorization";

			var content = new StringContent(serialize, Encoding.UTF8, "application/json");

			var response = await client.PostAsync(endPoint, content);

			var resultString = await response.Content.ReadAsStringAsync();

			if (response.IsSuccessStatusCode)
			{
				_logger.LogInformation("Post successful");
				result = JsonConvert.DeserializeObject<ChargeResponseDto>(resultString);
			}
			else
			{
				_logger.LogInformation("Post Unsuccessful, content: {result}, statuscode : {code}",
					resultString, (int)response.StatusCode);
			}
			return result!;
		}

		private async Task<ByteArrayContent> FormatContent(object content)
		{
			var json = JsonConvert.SerializeObject(content, Formatting.None,
				new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

			var buffer = Encoding.UTF8.GetBytes(json);
			var byteContent = new ByteArrayContent(buffer);

			byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

			return await Task.FromResult(byteContent);
		}
	}
}
