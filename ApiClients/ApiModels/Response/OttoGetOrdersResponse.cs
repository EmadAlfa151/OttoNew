using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;


namespace OttoNew.ApiClients.ApiModels.Response
{
	
	public class OttoGetOrdersResponse
	{
		[JsonPropertyName("resources")]
		public List<OttoOrderResource> Resources { get; set; }

		[JsonPropertyName("links")]
		public List<Link> Links { get; set; }
	}

	public class OttoOrderResource
	{
		[JsonPropertyName("salesOrderId")]
		public string SalesOrderId { get; set; }

		[JsonPropertyName("orderNumber")]
		public string OrderNumber { get; set; }

		[JsonPropertyName("orderDate")]
		public DateTime OrderDate { get; set; }

		[JsonPropertyName("lastModifiedDate")]
		public DateTime LastModifiedDate { get; set; }

		[JsonPropertyName("positionItems")]
		public List<OttoPositionItem> PositionItems { get; set; }

		[JsonPropertyName("orderLifecycleInformation")]
		public OttoOrderLifecycleInformation OrderLifecycleInformation { get; set; }

		[JsonPropertyName("payment")]
		public OttoPayment Payment { get; set; }

		[JsonPropertyName("initialDeliveryFees")]
		public List<OttoInitialDeliveryFee> InitialDeliveryFees { get; set; }

		[JsonPropertyName("links")]
		public List<Link> Links { get; set; }
	}

	public class OttoPositionItem
	{
		[JsonPropertyName("positionItemId")]
		public string PositionItemId { get; set; }

		[JsonPropertyName("fulfillmentStatus")]
		public string FulfillmentStatus { get; set; }

		[JsonPropertyName("dealName")]
		public string DealName { get; set; }

		[JsonPropertyName("dealId")]
		public string DealId { get; set; }

		[JsonPropertyName("itemValueGrossPrice")]
		public OttoPrice ItemValueGrossPrice { get; set; }

		[JsonPropertyName("product")]
		public OttoProduct Product { get; set; }
	}

	public class OttoPrice
	{
		[JsonPropertyName("amount")]
		public decimal Amount { get; set; }

		[JsonPropertyName("currency")]
		public string Currency { get; set; }
	}

	public class OttoProduct
	{
		[JsonPropertyName("sku")]
		public string Sku { get; set; }

		[JsonPropertyName("productTitle")]
		public string ProductTitle { get; set; }

		[JsonPropertyName("articleNumber")]
		public string ArticleNumber { get; set; }

		[JsonPropertyName("ean")]
		public string Ean { get; set; }

		[JsonPropertyName("vatRate")]
		public decimal VatRate { get; set; }
	}

	public class OttoOrderLifecycleInformation
	{
		[JsonPropertyName("lifecycleChangeDate")]
		public DateTime LifecycleChangeDate { get; set; }
	}

	public class OttoPayment
	{
		[JsonPropertyName("paymentMethod")]
		public string PaymentMethod { get; set; }
	}

	public class OttoInitialDeliveryFee
	{
		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("deliveryFeeAmount")]
		public OttoPrice DeliveryFeeAmount { get; set; }

		[JsonPropertyName("positionItemIds")]
		public List<string> PositionItemIds { get; set; }

		[JsonPropertyName("vatRate")]
		public decimal VatRate { get; set; }
	}



}
