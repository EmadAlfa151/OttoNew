using CoreSystem.DAL.Context;
using CoreSystem.Shared.DTOs;
using OttoNew.OdooSpecificDtos;
using OttoNew.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OttoNew.ApiClients
{
	public class OdooApiClient
	{
		private readonly HttpClient _httpClient;
		private readonly OdooAccountService odooAccountService;
		private string _url;
		private string _db;
		private string _username;
		private string _password;
		private int _requestId = 1;
		private int? _uid;

		public OdooApiClient(HttpClient httpClient, OdooAccountService odooAccountService)
		{
			_httpClient = httpClient;
			this.odooAccountService = odooAccountService;
			
		}

		

		public async Task<List<OdooCategoryDto>> GetProductCategoriesAsync()
		{
			await SetAccountValues();
			var payload = new
			{
				jsonrpc = "2.0",
				method = "call",
				@params = new
				{
					service = "object",
					method = "execute_kw",
					args = new object[]
					{
						_db,
						await Authenticate(),
						_password,
						"product.category",
						"search_read",
						new object[] {},
						new { fields = new[] { "id", "name" } }
					}
				},
				id = _requestId++
			};

			var response = await SendJsonRpcAsync(payload);
			return JsonSerializer.Deserialize<List<OdooCategoryDto>>(response.GetProperty("result").ToString());
		}

		public async Task<List<OdooProductDto>> GetProductsAsync()
		{
			await SetAccountValues();
			try
			{
				var payload = new
				{
					jsonrpc = "2.0",
					method = "call",
					@params = new
					{
						service = "object",
						method = "execute_kw",
						args = new object[]
						{
					_db,
					await Authenticate(),
					_password,
					"product.product",
					"search_read",
					new object[] { },
					new { fields = new[] { "id", "name", "default_code", "categ_id" } }
						}
					},
					id = _requestId++
				};

				var response = await SendJsonRpcAsync(payload);
				var products = JsonSerializer.Deserialize<List<JsonElement>>(response.GetProperty("result").ToString());

				var result = new List<OdooProductDto>();
				foreach (var p in products)
				{
					var id = p.GetProperty("id").GetInt32();
					var name = p.GetProperty("name").GetString();
					var defaultCode = p.TryGetProperty("default_code", out var dcProp) && dcProp.ValueKind != JsonValueKind.Null
						? dcProp.GetString()
						: "";

					var catArray = p.GetProperty("categ_id");

					int categoryId = 0;
					string categoryName = "";

					if (catArray.ValueKind == JsonValueKind.Array && catArray.GetArrayLength() == 2)
					{
						categoryId = catArray[0].GetInt32();
						categoryName = catArray[1].GetString();
					}

					result.Add(new OdooProductDto
					{
						Id = id,
						Name = name,
						DefaultCode = defaultCode,
						CategoryId = categoryId,
						CategoryName = categoryName,
						SourceSystem = "Odoo"
					});
				}


				return result;
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Error in GetProductsAsync: {ex.Message}");
				throw;
			}
		}

		public async Task<int> TestLoginAsync()
		{
			await SetAccountValues();
			var payload = new
			{
				jsonrpc = "2.0",
				method = "call",
				@params = new
				{
					service = "common",
					method = "login",
					args = new object[] { _db, _username, _password }
				},
				id = _requestId++
			};

			var response = await SendJsonRpcAsync(payload);
			return response.GetProperty("result").GetInt32();
		}

		public async Task<(bool success, string errorMessage)> CreateProductInOdoo(ProductDto product, int? odooCategoryId)
		{
			await SetAccountValues();
			try
			{
				string categoryName = product.ProductDescription?.Category ?? "Uncategorized";
				int categoryId = await GetCategoryIdByName(categoryName);

				var values = new Dictionary<string, object>
				{
					{
						"name",
						!string.IsNullOrWhiteSpace(product.ProductDescription?.Description)
							? product.ProductDescription.Description
							: !string.IsNullOrWhiteSpace(product.ProductReference)
								? product.ProductReference
								: !string.IsNullOrWhiteSpace(product.Sku)
									? product.Sku
									: "Unnamed Product"
					},
					{ "default_code", product.Sku ?? "" },
					{ "barcode", product.Ean ?? "" },
					{ "description", product.ProductDescription?.Description ?? "" },
					{ "list_price", product.Pricing?.StandardPrice?.Amount ?? 0m },
					{ "type", "product" },
					{ "categ_id", categoryId },
				};

				//// Important: Add only if category is selected
				//if (odooCategoryId.HasValue)
				//{
				//    values["categ_id"] = odooCategoryId.Value;
				//}

				// Debug all keys being sent
				foreach (var kv in values)
				{
					Debug.WriteLine($"Sending to Odoo - Key: {kv.Key}, Value: {kv.Value}");
				}

				var payload = new
				{
					jsonrpc = "2.0",
					method = "call",
					@params = new
					{
						service = "object",
						method = "execute_kw",
						args = new object[]
						{
					_db,
					await Authenticate(),
					_password,
					"product.template",
					"create",
					new object[] { values }
						}
					},
					id = _requestId++
				};

				var response = await SendJsonRpcAsync(payload);

				int newProductId = response.GetProperty("result").GetInt32();
				Debug.WriteLine($"Odoo product created with ID: {newProductId}");

				return (true, "");
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Error creating product in Odoo: {ex.Message}");
				return (false, ex.Message);
			}
		}

		public async Task<int> GetCategoryIdByName(string categoryName)
		{
			await SetAccountValues();
			// Ensure categoryName isn’t empty
			if (string.IsNullOrWhiteSpace(categoryName))
				categoryName = "Uncategorized";

			// Build the payload with domain: [[["name", "=", categoryName]]]
			var payloadSearch = new
			{
				jsonrpc = "2.0",
				method = "call",
				@params = new
				{
					service = "object",
					method = "execute_kw",
					args = new object[]
					{
				_db,
				await Authenticate(),
				_password,
				"product.category",
				"search",
                // Note the extra array nesting here:
                new object[]
				{
					new object[]
					{
						new object[] { "name", "=", categoryName }
					}
				}
					}
				},
				id = _requestId++
			};

			var response = await SendJsonRpcAsync(payloadSearch);
			var result = response.GetProperty("result");

			if (result.ValueKind == JsonValueKind.Array && result.GetArrayLength() > 0)
				return result[0].GetInt32();

			// If not found, create the category
			var payloadCreate = new
			{
				jsonrpc = "2.0",
				method = "call",
				@params = new
				{
					service = "object",
					method = "execute_kw",
					args = new object[]
					{
				_db,
				await Authenticate(),
				_password,
				"product.category",
				"create",
				new object[]
				{
					new Dictionary<string, object>
					{
						{ "name", categoryName }
					}
				}
					}
				},
				id = _requestId++
			};

			var createResponse = await SendJsonRpcAsync(payloadCreate);
			return createResponse.GetProperty("result").GetInt32();
		}

		public async Task<bool> ProductExists(string sku = "", string barcode = "")
		{
			await SetAccountValues();
			try
			{
				var payload = new
				{
					jsonrpc = "2.0",
					method = "call",
					@params = new
					{
						service = "object",
						method = "execute_kw",
						args = new object[]
						{
							_db,
							await Authenticate(),
							_password,
							"product.template",
							"search",
							new object[]
							{
								new object[]
								{
									"|",
									new object[] { "default_code", "=", sku },
									new object[] { "barcode", "=", barcode }
								}
							}
						}
					},
					id = _requestId++
				};

				var response = await SendJsonRpcAsync(payload);

				var result = response.GetProperty("result");
				return result.GetArrayLength() > 0;
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Error checking if product exists: {ex.Message}");
				return false; // Safe default: assume it doesn't exist to avoid duplicate creation
			}
		}
		public async Task<bool> ProductExistsByBarcode(string barcode)
		{
			await SetAccountValues();
			try
			{
				var payload = new
				{
					jsonrpc = "2.0",
					method = "call",
					@params = new
					{
						service = "object",
						method = "execute_kw",
						args = new object[]
						{
					_db,
					await Authenticate(),
					_password,
					"product.product",
					"search",
					new object[]
					{
						new object[] { new object[] { "barcode", "=", barcode } }
					}
						}
					},
					id = _requestId++
				};
				var response = await SendJsonRpcAsync(payload);
				var result = response.GetProperty("result");
				return result.GetArrayLength() > 0;
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Error checking if product exists by barcode: {ex.Message}");
				return false; // Safe default: assume it doesn't exist to avoid duplicate creation
			}
		}


		//public async Task<(bool success, string errorMessage)> CreateOrderInOdoo(OdooOrder odooOrder)
		//{
		//    try
		//    {
		//        var values = new Dictionary<string, object>
		//{
		//    { "partner_id", 1 },
		//    { "name", odooOrder.OrderNumber },
		//    { "date_order", odooOrder.OrderDate?.ToString("yyyy-MM-dd HH:mm:ss") },
		//    { "note", $"Order created from OTTO - Order ID: {odooOrder.SalesOrderId}" }
		//};

		//        var orderLines = new List<object>();

		//        // ✅ Example test item (in case real ones are empty or invalid)
		//        var testProductId = await GetOdooProductIdBySku("TEST-SKU-123");
		//        if (testProductId == 0)
		//        {
		//            // Create a sample product if not found
		//            var createPayload = new
		//            {
		//                jsonrpc = "2.0",
		//                method = "call",
		//                @params = new
		//                {
		//                    service = "object",
		//                    method = "execute_kw",
		//                    args = new object[]
		//                    {
		//                _db,
		//                await Authenticate(),
		//                _password,
		//                "product.product",
		//                "create",
		//                new object[]
		//                {
		//                    new Dictionary<string, object>
		//                    {
		//                        { "name", "Test Product" },
		//                        { "default_code", "TEST-SKU-123" },
		//                        { "list_price", 10 },
		//                        { "type", "product" }
		//                    }
		//                }
		//                    }
		//                },
		//                id = _requestId++
		//            };

		//            var createResponse = await SendJsonRpcAsync(createPayload);
		//            testProductId = createResponse.GetProperty("result").GetInt32();
		//        }

		//        orderLines.Add(new object[]
		//        {
		//    0, 0,
		//    new Dictionary<string, object>
		//    {
		//        { "product_id", testProductId },
		//        { "name", "Test Product" },
		//        { "product_uom_qty", 1 },
		//        { "price_unit", 10 },
		//        { "tax_id", new[] { 1 } }
		//    }
		//        });

		//        values["order_line"] = orderLines;

		//        var payload = new
		//        {
		//            jsonrpc = "2.0",
		//            method = "call",
		//            @params = new
		//            {
		//                service = "object",
		//                method = "execute_kw",
		//                args = new object[]
		//                {
		//            _db,
		//            await Authenticate(),
		//            _password,
		//            "sale.order",
		//            "create",
		//            new object[] { values }
		//                }
		//            },
		//            id = _requestId++
		//        };

		//        var response = await SendJsonRpcAsync(payload);
		//        int newOrderId = response.GetProperty("result").GetInt32();
		//        Debug.WriteLine($"✅ Odoo order created with ID: {newOrderId}");

		//        return (true, "Order successfully created in Odoo.");
		//    }
		//    catch (Exception ex)
		//    {
		//        Debug.WriteLine($"❌ Error creating order in Odoo: {ex.Message}");
		//        return (false, $"Error creating order in Odoo: {ex.Message}");
		//    }
		//}

		// Function to get Odoo Product ID by SKU
		public async Task<(bool success, string errorMessage)> CreateOrderInOdoo(OdooOrder odooOrder)
		{
			await SetAccountValues();
			try
			{
				var taxes = await GetTaxesAsync();
				Debug.WriteLine($"🛒 Processing Order: {odooOrder.OrderNumber}, Items: {odooOrder.OrderItems?.Count}");

				var values = new Dictionary<string, object>
				{
					{ "partner_id", 1 },
					{ "name", odooOrder.OrderNumber },
					{ "date_order", odooOrder.OrderDate?.ToString("yyyy-MM-dd HH:mm:ss") },
					{ "note", $"Order created from OTTO - Order ID: {odooOrder.SalesOrderId}" }
				};

				var orderLines = new List<object>();

				foreach (var item in odooOrder.OrderItems)
				{
					//var x = await GetTaxesAsync();
					Debug.WriteLine($"➡ Checking item: SKU='{item.ProductSku}', Qty={item.Quantity}, Price={item.Price}, Title='{item.ProductTitle}'");

					if (string.IsNullOrWhiteSpace(item.ProductSku) || item.Quantity <= 0)
					{
						Debug.WriteLine($"⛔ SKIPPED: Empty SKU or invalid quantity. SKU='{item.ProductSku}', Qty={item.Quantity}");
						continue;
					}

					int productId = await GetOdooProductIdBySku(item.ProductSku);
					if (productId == 0)
					{
						productId = await GetOdooProductIdByBarcode(item.Ean);
					}



					if (productId == 0)
					{
						// Step 1: Check if exists in template
						bool existsInTemplate = await ProductExists(sku: item.ProductSku, barcode: item.Ean);
						if (existsInTemplate)
						{
							Debug.WriteLine($"⚠️ SKIPPED: SKU '{item.ProductSku}' exists in product.template but no product.product linked.");
							continue;
						}

						// Step 2: Try create product
						var productDto = new ProductDto
						{
							Sku = item.ProductSku,
							Ean = item.Ean,
							ProductDescription = new ProductDescriptionDto { Description = item.ProductTitle },
							Pricing = new ProductPricingDto
							{
								StandardPrice = new PriceDto { Amount = item.Price > 0 ? item.Price : 1 }
							}
						};

						var creationResult = await CreateProductInOdoo(productDto, null);
						if (!creationResult.success)
						{
							Debug.WriteLine($"❌ SKIPPED: Failed to create product. SKU='{item.ProductSku}', Reason: {creationResult.errorMessage}");
							continue;
						}

						// Step 3: Try get product again
						productId = await GetOdooProductIdBySku(item.ProductSku);
						if (productId == 0)
						{
							Debug.WriteLine($"❌ SKIPPED: Product ID not found even after creation. SKU='{item.ProductSku}'");
							continue;
						}

						Debug.WriteLine($"✅ Product created & found: ID={productId}, SKU='{item.ProductSku}'");
					}

					var tax = taxes.FirstOrDefault(t => t.Active && t.Type == "sale" && t.Amount == item.VatRate);
					// Step 4: Add line
					var line = new object[]
					{ 0, 0, new Dictionary<string, object>
							{
									{ "product_id", productId },
									{ "name", string.IsNullOrWhiteSpace(item.ProductTitle) ? item.ProductSku : item.ProductTitle },
									{ "product_uom_qty", item.Quantity },
									{ "price_unit", item.Price > 0 ? item.Price : 1 },
									{ "tax_id", new[] { tax?.Id } }
							}
					};

					orderLines.Add(line);
					Debug.WriteLine($"✅ Order line added: SKU='{item.ProductSku}', Qty={item.Quantity}, ProductId={productId}");
				}

				Debug.WriteLine($"📦 Final orderLines count = {orderLines.Count}");

				if (orderLines.Count == 0)
				{
					return (false, $"No valid order lines could be created. line:\n{odooOrder.OrderNumber}");
				}

				values["order_line"] = orderLines;

				var payload = new
				{
					jsonrpc = "2.0",
					method = "call",
					@params = new
					{
						service = "object",
						method = "execute_kw",
						args = new object[]
						{
					_db,
					await Authenticate(),
					_password,
					"sale.order",
					"create",
					new object[] { values }
						}
					},
					id = _requestId++
				};

				var response = await SendJsonRpcAsync(payload);
				int newOrderId = response.GetProperty("result").GetInt32();
				Debug.WriteLine($"✅ Odoo order created with ID: {newOrderId}");

				return (true, $"Order created successfully in Odoo with ID {newOrderId}.");
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"❌ Error creating order in Odoo: {ex.Message}");
				return (false, $"Error creating order in Odoo: {ex.Message}");
			}
		}

		public async Task<int> GetOdooProductIdBySku(string sku)
		{
			await SetAccountValues();
			try
			{
				var payload = new
				{
					jsonrpc = "2.0",
					method = "call",
					@params = new
					{
						service = "object",
						method = "execute_kw",
						args = new object[]
						{
					_db,  // Odoo database name
                    await Authenticate(),  // Odoo user ID (authenticate)
                    _password,  // Odoo user password
                    "product.product", // Odoo model for products
                    "search_read",  // ✅ Use search_read instead of search
                    new object[]
					{
						new object[]
						{
							new object[] { "default_code", "=", sku }
						}
					},
					new Dictionary<string, object>
					{
						{ "fields", new[] { "id" } },
						{ "limit", 1 }
					}
						}
					},
					id = _requestId++
				};

				var response = await SendJsonRpcAsync(payload);

				if (response.TryGetProperty("result", out var resultArray) &&
					resultArray.ValueKind == JsonValueKind.Array &&
					resultArray.GetArrayLength() > 0)
				{
					var firstProduct = resultArray[0];
					if (firstProduct.TryGetProperty("id", out var idProperty))
					{
						return idProperty.GetInt32();
					}
				}

				return 0; // Not found
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Error while fetching product by SKU: {ex.Message}");
				return 0;
			}
		}
		public async Task<int> GetOdooProductIdByBarcode(string barcode)
		{
			await SetAccountValues();
			try
			{
				var payload = new
				{
					jsonrpc = "2.0",
					method = "call",
					@params = new
					{
						service = "object",
						method = "execute_kw",
						args = new object[]
						{
					_db,  // Odoo database name
                    await Authenticate(),  // Odoo user ID (authenticate)
                    _password,  // Odoo user password
                    "product.product", // Odoo model for products
                    "search_read",  // ✅ Use search_read instead of search
                    new object[]
					{
						new object[]
						{
							new object[] { "barcode", "=", barcode }
						}
					},
					new Dictionary<string, object>
					{
						{ "fields", new[] { "id" } },
						{ "limit", 1 }
					}
						}
					},
					id = _requestId++
				};

				var response = await SendJsonRpcAsync(payload);

				if (response.TryGetProperty("result", out var resultArray) &&
					resultArray.ValueKind == JsonValueKind.Array &&
					resultArray.GetArrayLength() > 0)
				{
					var firstProduct = resultArray[0];
					if (firstProduct.TryGetProperty("id", out var idProperty))
					{
						return idProperty.GetInt32();
					}
				}

				return 0; // Not found
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Error while fetching product by barcode: {ex.Message}");
				return 0;
			}
		}

		public async Task<List<ProductVariationQuantityDto>> GetProductVariationQuantities()
		{
			await SetAccountValues();
			try
			{
				var payload = new
				{
					jsonrpc = "2.0",
					method = "call",
					@params = new
					{
						service = "object",
						method = "execute_kw",
						args = new object[]
						{
							_db,
							await Authenticate(),
							_password,
							"product.product",
							"search_read",
							new object[] { },
							new { fields = new[] { "qty_available", "default_code" } }
						}
					},
					id = _requestId++
				};
				var response = await SendJsonRpcAsync(payload);
				var products = JsonSerializer.Deserialize<List<JsonElement>>(response.GetProperty("result").ToString());
				var result = new List<ProductVariationQuantityDto>();
				foreach (var p in products)
				{
					if (!p.TryGetProperty("qty_available", out var qtyProp) || !p.TryGetProperty("default_code", out var codeProp))
					{
						Debug.WriteLine($"Skipping product due to missing properties.");
						continue;
					}
					int quantity = (int)qtyProp.GetDecimal();
					string sku = codeProp.ValueKind == JsonValueKind.String ? codeProp.GetString() : string.Empty;

					result.Add(new ProductVariationQuantityDto
					{
						Quantity = 2,
						Sku = sku
					});
				}
				return result;
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Error in GetProductVariationQuantities: {ex.Message}");
				throw;
			}

		}

		public async Task<List<OdooOrder>> GetOrdersReadyForShipmentAsync()
		{
			await SetAccountValues();
			try
			{
				var payload = new
				{
					jsonrpc = "2.0",
					method = "call",
					@params = new
					{
						service = "object",
						method = "execute_kw",
						args = new object[]
						{
							_db,
							await Authenticate(),
							_password,
							"sale.order",
							"search_read",
							new object[] { new object[] { new object[] { "state", "=", "sale" } } },
							new { fields = new[] { "name", "id", "date_order", "order_line" } }
						}
					},
					id = _requestId++
				};
				var response = await SendJsonRpcAsync(payload);
				var ordersData = JsonSerializer.Deserialize<List<JsonElement>>(response.GetProperty("result").ToString());
				var orders = new List<OdooOrder>();
				foreach (var orderData in ordersData)
				{
					var order = new OdooOrder
					{
						OrderNumber = orderData.GetProperty("name").GetString(),
						SalesOrderId = orderData.GetProperty("id").GetInt32().ToString(),
						OrderDate = orderData.TryGetProperty("date_order", out var dateProp) && dateProp.ValueKind != JsonValueKind.Null
							? dateProp.GetDateTime()
							: null,
						OrderItems = new List<OdooOrderItem>()
					};
					if (orderData.TryGetProperty("order_line", out var linesArray) && linesArray.ValueKind == JsonValueKind.Array)
					{
						foreach (var line in linesArray.EnumerateArray())
						{
							if (line.TryGetProperty("product_id", out var productIdProp) &&
								line.TryGetProperty("product_uom_qty", out var qtyProp) &&
								line.TryGetProperty("price_unit", out var priceProp))
							{
								order.OrderItems.Add(new OdooOrderItem
								{
									ProductId = productIdProp.GetInt32(),
									Quantity = qtyProp.GetInt32(),
									Price = priceProp.GetDecimal()
								});
							}
						}
					}
					orders.Add(order);
				}
				return orders;
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Error in GetOrdersReadyForShipmentAsync: {ex.Message}");
				throw;
			}
		}


		public async Task<List<OdooTaxDto>> GetTaxesAsync()
		{
			await SetAccountValues();
			try
			{
				var payload = new
				{
					jsonrpc = "2.0",
					method = "call",
					@params = new
					{
						service = "object",
						method = "execute_kw",
						args = new object[]
						{
					_db,
					await Authenticate(),
					_password,
					"account.tax",
					"search_read",
					new object[] { }, // no filter
                    new
					{
						fields = new[] { "id", "name", "amount", "type_tax_use", "active" }
					}
						}
					},
					id = _requestId++
				};

				var response = await SendJsonRpcAsync(payload);
				var taxes = JsonSerializer.Deserialize<List<JsonElement>>(response.GetProperty("result").ToString());

				var result = new List<OdooTaxDto>();
				foreach (var tax in taxes)
				{
					result.Add(new OdooTaxDto
					{
						Id = tax.GetProperty("id").GetInt32(),
						Name = tax.GetProperty("name").GetString(),
						Amount = tax.GetProperty("amount").GetDecimal(),
						Type = tax.GetProperty("type_tax_use").GetString(),
						Active = tax.GetProperty("active").GetBoolean()
					});
				}

				return result;
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Error in GetTaxesAsync: {ex.Message}");
				throw;
			}
		}



		private async Task SetAccountValues()
		{
			var accountResult = await odooAccountService.GetFirstAccount();
			if (accountResult.IsSuccess)
			{
				var account = accountResult.Data;
				_url = account.Url.EndsWith("/jsonrpc") ? account.Url : account.Url.TrimEnd('/') + "/jsonrpc";
				_db = account.DataBaseName;
				_username = account.UserName;
				_password = account.Password;
			}
		}
		private async Task<int> Authenticate()
		{
			await SetAccountValues();
			if (_uid.HasValue) return _uid.Value;

			var payload = new
			{
				jsonrpc = "2.0",
				method = "call",
				@params = new
				{
					service = "common",
					method = "login",
					args = new object[] { _db, _username, _password }
				},
				id = _requestId++
			};

			var response = await SendJsonRpcAsync(payload);
			_uid = response.GetProperty("result").GetInt32();
			return _uid.Value;
		}
		private async Task<JsonElement> SendJsonRpcAsync(object payload)
		{
			await SetAccountValues();
			var jsonRequest = JsonSerializer.Serialize(payload);
			Debug.WriteLine("ODoo JSON-RPC Request: " + jsonRequest);

			var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync(_url, content);
			var jsonResponse = await response.Content.ReadAsStringAsync();

			Debug.WriteLine("Odoo JSON-RPC Response: " + jsonResponse);

			var doc = JsonDocument.Parse(jsonResponse);
			var root = doc.RootElement;

			if (root.TryGetProperty("error", out var error))
			{
				var msg = error.TryGetProperty("message", out var m) ? m.GetString() : "Unknown Odoo error";
				throw new Exception($"Odoo JSON-RPC Error: {msg}");
			}

			return root;
		}

	}

	
}
