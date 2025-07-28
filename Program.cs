using CoreSystem.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OttoNew.ApiClients;
using OttoNew.Mappers;
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
        // Test fetching product variations and creating products in Odoo
        var resultProducts = await ottoApiClient.GetProductVariations(); // Using accountId = 1 for testing
        var mappedProducts = DbModelMapper.MapToProductDto(resultProducts.Data);
        var x = await odooApiClient.CreateProductsInOdooAsync(mappedProducts.Take(10).ToList());

        //////////////////////////////////////////////
        ///
        // Test fetching orders and creating them in Odoo
        var resultOrders = await ottoApiClient.GetOrdersAsync(DateTime.Now.AddDays(-1), DateTime.Now); // Using accountId = 1 for testing
        var mappedOrders = DbModelMapper.MapToOrderDtoList(resultOrders.Data);
        


    }
    catch (Exception ex)
    {
        Console.WriteLine($"Exception during authentication: {ex.Message}");
    }
}

Console.WriteLine("Press any key to exit...");
Console.ReadKey();