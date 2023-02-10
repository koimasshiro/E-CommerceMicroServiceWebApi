using PaymentAPI.HttpServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient("Paystack", config =>
{

	config.DefaultRequestHeaders.Clear();
	config.DefaultRequestHeaders.Add("accept", "application/json");
	config.BaseAddress = new Uri(builder.Configuration["Paystack:BaseUrl"]);
});

builder.Services.AddScoped<IPaystackService, PaystackService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//app.Use(async (context, next) =>
//{
//	context.Response.Headers.Add("Authorization", $"Bearer sk_test_b22663b0e6cd60f262fad52fc7fa877166e80bec" );
//	await next();
//});

app.MapControllers();

app.Run();
