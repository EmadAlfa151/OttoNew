using CoreSystem.DAL.Context;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Configuration;
using OttoNew.ApiClients.ApiModels.Response;
using OttoNew.Common;
using OttoNew.Extensions;
using OttoNew.infrastructure;
using System.Buffers.Text;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;

namespace OttoNew.ApiClients
{
	public class OttoApiClient
	{
		private readonly HttpClient httpClient;
		private readonly Middleware_dbContext dbContext;

		public OttoApiClient(HttpClient httpClient, Middleware_dbContext dbContext)
		{
			this.httpClient = httpClient;
			this.dbContext = dbContext;
		}



		public async Task<Result<List<OttoProductVariation>>> GetProductVariations()
		{
			const int maxBatchSize = 100;
			var authenticationResult = await Authenticate(1); //handle different accounts from db
			if (!authenticationResult.IsSuccess)
			{
				Result.Failure(authenticationResult.ErrorMessage);
			}

			var results = new List<OttoProductVariation>();
			string? nextUrl = $"/v5/products?limit={maxBatchSize}";

			while (!string.IsNullOrEmpty(nextUrl))
			{
				var response = await httpClient.GetAsync(nextUrl);

				if (!response.IsSuccessStatusCode)
				{
					Debug.WriteLine($"❌ Error fetching products page: {response.StatusCode}");
					break;
				}

				var content = await response.Content.ReadAsStringAsync();
				var obj = JsonSerializer.Deserialize<OttoProductResponse>(content);
				results.AddRange(obj.ProductVariations);

				// Look for next page link
				nextUrl = null;
				if (obj.Links is { Count: > 0 })
				{
					foreach (var link in obj.Links)
					{
						if (link.Rel == nameof(PaginationLinkRelation.next) && !string.IsNullOrWhiteSpace(link.Href))
						{
							nextUrl = link.Href;
							break;
						}
					}
				}
			}

			return Result<List<OttoProductVariation>>.Success(results);
		}
		public async Task<Result> SetProductVariationQuantitiesAsync(List<ProductVariationQuantityDto> productVariations)
		{
			const int maxBatchSize = 200;
			if (productVariations == null || productVariations.Count == 0)
				return Result.Failure("No product variations to send.");

			var authenticationResult = await Authenticate(1); //handle different accounts from db
			if (!authenticationResult.IsSuccess)
			{
				Result.Failure(authenticationResult.ErrorMessage);
			}
			httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			var errors = new StringBuilder();
			for (int i = 0; i < productVariations.Count; i += maxBatchSize)
			{
				var batch = productVariations.Skip(i).Take(maxBatchSize).ToList();

				var json = JsonSerializer.Serialize(batch, new JsonSerializerOptions
				{
					PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
					DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
				});
				var content = new StringContent(json, Encoding.UTF8, "application/json");

				var response = await httpClient.PostAsync("/v1/availability/quantities", content);
				var responseBody = await response.Content.ReadAsStringAsync();

				if (response.StatusCode == HttpStatusCode.MultiStatus)
				{
					var responseObj = JsonSerializer.Deserialize<OttoUpdateQuantityResponse>(responseBody, new JsonSerializerOptions
					{
						PropertyNameCaseInsensitive = true
					});

					if (responseObj?.Errors != null)
					{
						foreach (var error in responseObj.Errors)
						{
							errors.AppendLine(error.Detail);
						}
					}
				}
				else if (response.StatusCode != HttpStatusCode.OK)
				{
					errors.AppendLine($"Batch {i / maxBatchSize + 1} failed: {(int)response.StatusCode} - {response.ReasonPhrase}: {responseBody}");
				}

			}

			if (errors.Length > 0)
			{
				return Result.Failure(errors.ToString());
			}

			return Result.Success();
		}
		/// <summary>
		/// Get orders within specific time range from otto
		/// </summary>
		/// <param name="fromDate"></param>
		/// <param name="toDate"></param>
		/// <returns></returns>
		public async Task<Result<List<OttoOrderResource>>> GetOrdersAsync(DateTime fromDate, DateTime toDate)
		{
			const int maxBatchSize = 256; //as per otto doc
			var authenticationResult = await Authenticate(1); //handle different accounts from db
			if (!authenticationResult.IsSuccess)
			{
				Result.Failure(authenticationResult.ErrorMessage);
			}

			var results = new List<OttoOrderResource>();
			string mainPath = "v4/orders";
			Uri uri = new Uri(httpClient.BaseAddress + mainPath)
				.AddQueryParam("limit", value: maxBatchSize.ToString())
				.AddQueryParam("orderColumnType", value: "ORDER_DATE")
				.AddQueryParam("fromDate", value: fromDate.ToString("O"))
				.AddQueryParam("toOrderDate", value: toDate.ToString("O"));


			bool allOrdersReceived = false;
			while (!allOrdersReceived)
			{
				var response = await httpClient.GetAsync(uri.PathAndQuery);

				if (!response.IsSuccessStatusCode)
				{
					Debug.WriteLine($"❌ Error fetching orders page: {response.StatusCode}");
					break;
				}

				var content = await response.Content.ReadAsStringAsync();
				var serializationOptions = new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				};
				serializationOptions.Converters.Add(new FlexibleDateTimeConverter());

				var obj = JsonSerializer.Deserialize<OttoGetOrdersResponse>(content, serializationOptions);
				results.AddRange(obj.Resources);

				// Look for next page link
				if (obj.Links is { Count: > 0 })
				{
					foreach (var link in obj.Links)
					{
						if (link.Rel == nameof(PaginationLinkRelation.next) && !string.IsNullOrWhiteSpace(link.Href))
						{
							var query = HttpUtility.ParseQueryString(new Uri("http://dummy.com" + link.Href).Query);
							string nextCursor = query["nextcursor"];
							if (string.IsNullOrWhiteSpace(nextCursor))
							{
								allOrdersReceived = true;
								break;
							}
							uri = uri.AddQueryParam("nextcursor", value: nextCursor);
							break;
						}
						else
						{
							allOrdersReceived = true;
						}
					}
				}
				else
				{
					allOrdersReceived = true;
				}


			}

			return Result<List<OttoOrderResource>>.Success(results);
		}



		private async Task<Result> Authenticate(int accountId)
		{
			try
			{
				// Debug: Check HttpClient configuration
				Console.WriteLine($"Debug: HttpClient BaseAddress = '{httpClient.BaseAddress}'");

				// TODO: Get clientId and client secret from db
				var clientId = "473243df-5244-4442-beee-a5ea707776c7";
				var clientSecret = "25452b7a-2e76-4780-a5db-6d41c4e5a647";

				var url = "/oauth2/token";
				Console.WriteLine($"Debug: Making request to URL = '{url}'");

				var requestBody = new FormUrlEncodedContent(new Dictionary<string, string>
				{
					{ "grant_type", "client_credentials" },
					{ "client_id", clientId },
					{ "client_secret", clientSecret },
					{ "scope",  $"{OttoTokenScopes.Orders} {OttoTokenScopes.Products} {OttoTokenScopes.Availability} {OttoTokenScopes.Returns}"}
				});

				var response = await httpClient.PostAsync(url, requestBody);
				var responseContent = await response.Content.ReadAsStringAsync();
				var jsonDocument = JsonDocument.Parse(responseContent);

				if (!response.IsSuccessStatusCode)
				{
					string errorMessage = await response.Content.ReadAsStringAsync();
					return Result.Failure(errorMessage);
				}
				if (jsonDocument.RootElement.TryGetProperty("access_token", out var tokenProperty))
				{
					SetAuthorizationHeader(tokenProperty.ToString());
					return Result.Success();
				}
				else
				{
					string errorMessage = jsonDocument.RootElement.GetProperty("error_description").GetString();
					Debug.WriteLine($"Error response: {errorMessage}");
					return Result.Failure(errorMessage);
				}

			}
			catch (Exception ex)
			{
				return Result.Failure(ex);
			}
		}
		private void SetAuthorizationHeader(string token)
		{
			httpClient.DefaultRequestHeaders.Authorization =
				new AuthenticationHeaderValue("Bearer", token);
		}
	}


}