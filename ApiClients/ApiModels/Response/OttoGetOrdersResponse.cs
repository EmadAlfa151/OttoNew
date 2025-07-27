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
		[JsonPropertyName("deliveryAddress")]
		public OttoAddress DeliveryAddress { get; set; }
		[JsonPropertyName("invoiceAddress")]
		public OttoAddress InvoiceAddress { get; set; }

		[JsonPropertyName("links")]
		public List<Link> Links { get; set; }
	}
	public class OttoInitialDiscount
	{
		[JsonPropertyName("name")]
		public string Name { get; set; }
		[JsonPropertyName("discountId")]
		public string DiscountId { get; set; }
		[JsonPropertyName("name")]
		public string DiscountType { get; set; }
		[JsonPropertyName("discountName")]
		public string DiscountName { get; set; }
		[JsonPropertyName("positionItemIds")]
		public List<string> PositionItemIds { get; set; }


		[JsonPropertyName("discountAmount")]
		public OttoPrice DiscountAmount { get; set; }
		[JsonPropertyName("vatRate")]
		public decimal VatRate { get; set; }
	}
	public class OttoAddress
	{
		[JsonPropertyName("firstName")]
		public string FirstName { get; set; }
		[JsonPropertyName("lastName")]
		public string LastName { get; set; }
		[JsonPropertyName("street")]
		public string Street { get; set; }
		[JsonPropertyName("houseNumber")]
		public string HouseNumber { get; set; }
		[JsonPropertyName("zipCode")]
		public string ZipCode { get; set; }
		[JsonPropertyName("city")]
		public string City { get; set; }
		[JsonPropertyName("countryCode")]
		public string CountryCode { get; set; }
		[JsonPropertyName("email")]
		public string Email { get; set; }
		[JsonPropertyName("phoneNumber")]
		public string PhoneNumber { get; set; }
		[JsonPropertyName("salutation")]
		public string Salutation { get; set; }
		[JsonPropertyName("addition")]
		public string Addition { get; set; }
	}

	public class OttoPositionItem
	{
		[JsonPropertyName("cancellationDate")]
		public DateTime? CancellationDate { get; set; }
		[JsonPropertyName("cancellationReason")]
		public string CancellationReason { get; set; }

		[JsonPropertyName("dealId")]
		public string DealId { get; set; }

		[JsonPropertyName("dealName")]
		public string DealName { get; set; }

		[JsonPropertyName("expectedDeliveryDate")]
		public DateTime? ExpectedDeliveryDate { get; set; }

		[JsonPropertyName("positionItemId")]
		public string PositionItemId { get; set; }

		[JsonPropertyName("fulfillmentStatus")]
		public OttoFulfillmentStatus FulfillmentStatus { get; set; }
		[JsonPropertyName("itemValueDiscount")]
		public OttoPrice ItemValueDiscount { get; set; }

		[JsonPropertyName("itemValueGrossPrice")]
		public OttoPrice ItemValueGrossPrice { get; set; }
		[JsonPropertyName("itemValueReducedGrossPrice")]
		public OttoPrice ItemValueReducedGrossPrice { get; set; }

		[JsonPropertyName("product")]
		public OttoProduct Product { get; set; }
		[JsonPropertyName("sentDate")]
		public DateTime? SentDate { get; set; }
		[JsonPropertyName("processableDate")]
		public DateTime? ProcessableDate { get; set; }
		[JsonPropertyName("returnAcceptedByMarketplace")]
		public bool ReturnAcceptedByMarketplace { get; set; }

		[JsonPropertyName("returnedDate")]
		public DateTime? ReturnedDate { get; set; }
		[JsonPropertyName("trackingInfo")]
		public OttoOrderTrackingInfo TrackingInfo { get; set; }
		[JsonPropertyName("weeePickup")]
		public bool WeeePickup { get; set; }
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
		[JsonPropertyName("articleNumber")]
		public string ArticleNumber { get; set; }
		[JsonPropertyName("dimensions")]
		public List<OttoProductDimensions>? Dimensions { get; set; }
		[JsonPropertyName("sku")]
		public string Sku { get; set; }

		[JsonPropertyName("productTitle")]
		public string ProductTitle { get; set; }
		[JsonPropertyName("shopUrl")]
		public string ShopUrl { get; set; }

		[JsonPropertyName("ean")]
		public string Ean { get; set; }

		[JsonPropertyName("vatRate")]
		public decimal VatRate { get; set; }
	}

	public class OttoOrderTrackingInfo
	{
		[JsonPropertyName("trackingNumber")]
		public string TrackingNumber { get; set; }
		[JsonPropertyName("carrier")]
		public string Carrier { get; set; }
		[JsonPropertyName("carrierServiceCode")]
		public string CarrierServiceCode { get; set; }

	}
	public class OttoProductDimensions
	{
		[JsonPropertyName("displayName")]
		public string DisplayName { get; set; }
		[JsonPropertyName("type")]
		public string Type { get; set; }
		[JsonPropertyName("value")]
		public string Value { get; set; }
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
