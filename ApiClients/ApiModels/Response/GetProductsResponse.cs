using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OttoNew.ApiClients.ApiModels.Response
{
	public class OttoProductResponse
	{
		[JsonPropertyName("productVariations")]
		public List<OttoProductVariation> ProductVariations { get; set; }

		[JsonPropertyName("links")]
		public List<Link> Links { get; set; }
	}

	public class OttoProductVariation
	{
		[JsonPropertyName("ean")]
		public string Ean { get; set; }

		[JsonPropertyName("productReference")]
		public string ProductReference { get; set; }

		[JsonPropertyName("sku")]
		public string Sku { get; set; }

		[JsonPropertyName("productDescription")]
		public ProductDescription ProductDescription { get; set; }

		[JsonPropertyName("pricing")]
		public Pricing Pricing { get; set; }

		[JsonPropertyName("mediaAssets")]
		public List<MediaAsset> MediaAssets { get; set; }

		[JsonPropertyName("compliance")]
		public Compliance Compliance { get; set; }

		[JsonPropertyName("pzn")]
		public string Pzn { get; set; }

		[JsonPropertyName("mpn")]
		public string Mpn { get; set; }

		[JsonPropertyName("moin")]
		public string Moin { get; set; }

		[JsonPropertyName("releaseDate")]
		public DateTime ReleaseDate { get; set; }

		[JsonPropertyName("order")]
		public Order Order { get; set; }

		[JsonPropertyName("logistics")]
		public Logistics Logistics { get; set; }
	}

	public class ProductDescription
	{
		[JsonPropertyName("brandId")]
		public string BrandId { get; set; }

		[JsonPropertyName("category")]
		public string Category { get; set; }

		[JsonPropertyName("productLine")]
		public string ProductLine { get; set; }

		[JsonPropertyName("productionDate")]
		public DateTime ProductionDate { get; set; }

		[JsonPropertyName("multiPack")]
		public bool MultiPack { get; set; }

		[JsonPropertyName("bundle")]
		public bool Bundle { get; set; }

		[JsonPropertyName("fscCertified")]
		public bool FscCertified { get; set; }

		[JsonPropertyName("disposal")]
		public bool Disposal { get; set; }

		[JsonPropertyName("productUrl")]
		public string ProductUrl { get; set; }

		[JsonPropertyName("description")]
		public string Description { get; set; }

		[JsonPropertyName("bulletPoints")]
		public List<string> BulletPoints { get; set; }

		[JsonPropertyName("attributes")]
		public List<ProductAttribute> Attributes { get; set; }
	}

	public class ProductAttribute
	{
		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("values")]
		public List<string> Values { get; set; }

		[JsonPropertyName("additional")]
		public bool Additional { get; set; }
	}

	public class Pricing
	{
		[JsonPropertyName("standardPrice")]
		public PriceAmount StandardPrice { get; set; }

		[JsonPropertyName("vat")]
		public string Vat { get; set; }

		[JsonPropertyName("msrp")]
		public PriceAmount Msrp { get; set; }

		[JsonPropertyName("sale")]
		public SaleInfo Sale { get; set; }

		[JsonPropertyName("normPriceInfo")]
		public NormPriceInfo NormPriceInfo { get; set; }
	}

	public class PriceAmount
	{
		[JsonPropertyName("amount")]
		public decimal Amount { get; set; }

		[JsonPropertyName("currency")]
		public string Currency { get; set; }
	}

	public class SaleInfo
	{
		[JsonPropertyName("salePrice")]
		public PriceAmount SalePrice { get; set; }

		[JsonPropertyName("startDate")]
		public DateTime StartDate { get; set; }

		[JsonPropertyName("endDate")]
		public DateTime EndDate { get; set; }
	}

	public class NormPriceInfo
	{
		[JsonPropertyName("normAmount")]
		public int NormAmount { get; set; }

		[JsonPropertyName("normUnit")]
		public string NormUnit { get; set; }

		[JsonPropertyName("salesAmount")]
		public int SalesAmount { get; set; }

		[JsonPropertyName("salesUnit")]
		public string SalesUnit { get; set; }
	}

	public class MediaAsset
	{
		[JsonPropertyName("location")]
		public string Location { get; set; }

		[JsonPropertyName("type")]
		public string Type { get; set; }
	}

	public class Compliance
	{
		[JsonPropertyName("productSafety")]
		public ProductSafety ProductSafety { get; set; }

		[JsonPropertyName("foodInformation")]
		public FoodInformation FoodInformation { get; set; }
	}

	public class ProductSafety
	{
		[JsonPropertyName("addresses")]
		public List<ComplianceAddress> Addresses { get; set; }
	}

	public class FoodInformation
	{
		[JsonPropertyName("addresses")]
		public List<ComplianceAddress> Addresses { get; set; }
	}

	public class ComplianceAddress
	{
		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("address")]
		public string Address { get; set; }

		[JsonPropertyName("roles")]
		public List<string> Roles { get; set; }

		[JsonPropertyName("regionCode")]
		public string RegionCode { get; set; }

		[JsonPropertyName("email")]
		public string Email { get; set; }

		[JsonPropertyName("url")]
		public string Url { get; set; }

		[JsonPropertyName("phone")]
		public string Phone { get; set; }

		[JsonPropertyName("components")]
		public List<string> Components { get; set; }
	}

	public class Order
	{
		[JsonPropertyName("maxOrderQuantity")]
		public MaxOrderQuantity MaxOrderQuantity { get; set; }
	}

	public class MaxOrderQuantity
	{
		[JsonPropertyName("quantity")]
		public int Quantity { get; set; }

		[JsonPropertyName("periodInDays")]
		public int PeriodInDays { get; set; }
	}

	public class Logistics
	{
		[JsonPropertyName("packingUnitCount")]
		public int PackingUnitCount { get; set; }

		[JsonPropertyName("packingUnits")]
		public List<PackingUnit> PackingUnits { get; set; }
	}

	public class PackingUnit
	{
		[JsonPropertyName("weight")]
		public int Weight { get; set; }

		[JsonPropertyName("width")]
		public int Width { get; set; }

		[JsonPropertyName("height")]
		public int Height { get; set; }

		[JsonPropertyName("length")]
		public int Length { get; set; }
	}

	public class Link
	{
		[JsonPropertyName("rel")]
		public string Rel { get; set; }

		[JsonPropertyName("href")]
		public string Href { get; set; }
	}
}
