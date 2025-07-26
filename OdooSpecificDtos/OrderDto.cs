using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OttoNew.OdooSpecificDtos
{
	public class OrderDto
	{
		public string SalesOrderId { get; set; }
		public string OrderNumber { get; set; }
		public DateTime? OrderDate { get; set; }
		public DateTime? LastModifiedDate { get; set; }
		public string PaymentMethod { get; set; }
		public List<OrderItemDto> Items { get; set; } = new();
		public List<DeliveryFeeDto> DeliveryFees { get; set; } = new();
	}

	public class OrderItemDto
	{
		public string PositionItemId { get; set; }
		public string FulfillmentStatus { get; set; }
		public string Sku { get; set; }
		public string ProductTitle { get; set; }
		public string ArticleNumber { get; set; }
		public string Ean { get; set; }
		public decimal ItemValueGrossPrice { get; set; }
		public string Currency { get; set; }
		public int VatRate { get; set; }
		public string ShopUrl { get; set; }

		public string TrackingNumber { get; set; }  // New
		public string Color { get; set; }           // New
		public string Size { get; set; }            // New
	}

	public class DeliveryFeeDto
	{
		public string Name { get; set; }
		public decimal DeliveryFeeAmount { get; set; }
		public string Currency { get; set; }
		public int VatRate { get; set; }
		public List<string> PositionItemIds { get; set; } = new List<string>();
	}
	public class OdooOrder
	{
		public string OrderNumber { get; set; }
		public string SalesOrderId { get; set; }
		public DateTime? OrderDate { get; set; }
		public string PaymentMethod { get; set; }
		public List<OdooOrderItem> OrderItems { get; set; }
	}

	public class OdooOrderItem
	{
		public int ProductId { get; set; }
		public string ProductSku { get; set; }
		public string ProductTitle { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; }

		// Optional data for Odoo enrichment
		public string FulfillmentStatus { get; set; }
		public string ArticleNumber { get; set; }
		public string Ean { get; set; }
		public string Currency { get; set; }
		public int VatRate { get; set; }
		public string ShopUrl { get; set; }

		public string TrackingNumber { get; set; }
		public string Color { get; set; }
		public string Size { get; set; }
	}
	public class OdooCategoryDto
	{
		[JsonPropertyName("id")]
		public int Id { get; set; }
		[JsonPropertyName("name")]
		public string Name { get; set; }
	}

	public class OdooTaxDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public decimal Amount { get; set; }
		public string Type { get; set; } // 'sale', 'purchase', 'none'
		public bool Active { get; set; }
	}

}
