using CoreSystem.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OttoNew.ApiClients;
using OttoNew.Services;

var builder = Host.CreateApplicationBuilder(args);

// Add configuration
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Add services
builder.Services.AddDbContext< AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));


builder.Services.AddHttpClient<OttoApiClient>(client =>
{
	var baseUrl = builder.Configuration["OttoConfiguration:BaseUrl"];
	
	if (string.IsNullOrWhiteSpace(baseUrl))
	{
		throw new InvalidOperationException("Missing OttoConfiguration:BaseUrl in appsettings.json");
	}

	client.BaseAddress = new Uri(baseUrl);
	client.Timeout = TimeSpan.FromSeconds(30);
});
builder.Services.AddScoped<OdooAccountService>();
builder.Services.AddScoped<OdooApiClient>();


var app = builder.Build();

// Application entry point
Console.WriteLine("Otto Marketplace Integration Started");

// Test the Authenticate method for debugging
using (var scope = app.Services.CreateScope())
{
    var ottoApiClient = scope.ServiceProvider.GetRequiredService<OttoApiClient>();
    var odooApiClient = scope.ServiceProvider.GetRequiredService<OdooApiClient>();
    
    Console.WriteLine("Testing Odoo ...");
    
    try
    {
        // Call the Authenticate method - you can set a breakpoint here
        var result = await ottoApiClient.GetOrdersAsync(DateTime.Now.AddDays(-1), DateTime.Now); // Using accountId = 1 for testing
        //var odooResult = await odooApiClient.GetProductVariationQuantities();
        
        //if (result.IsSuccess)
        //{
        //    Console.WriteLine("result test successful!");
        //}
        //else
        //{
        //    Console.WriteLine($"result test failed: {result.ErrorMessage}");
        //}
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Exception during authentication: {ex.Message}");
    }
}

Console.WriteLine("Press any key to exit...");
Console.ReadKey();