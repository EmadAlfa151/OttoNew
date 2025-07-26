using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OttoNew.OdooSpecificDtos
{
	public class OdooProductDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string DefaultCode { get; set; }
		public string Ean { get; set; }
		public int CategoryId { get; set; }
		public string CategoryName { get; set; }
		public string SourceSystem { get; set; } = "Unknown"; // e.g., "OTTO" or "Odoo"
		public ProductPricingDto Pricing { get; set; } = new ProductPricingDto();
	}

	public class ProductDto
	{
		public string ProductReference { get; set; }
		public string Name { get; set; }
		public string Sku { get; set; }
		public string Ean { get; set; }
		public string Pzn { get; set; }
		public string Mpn { get; set; }
		public string Moin { get; set; }
		public DateTime? ReleaseDate { get; set; }

		public ProductDescriptionDto ProductDescription { get; set; }
		public List<ProductMediaDto> MediaAssets { get; set; } = new();
		public ProductOrderWrapperDto Order { get; set; }
		public ProductPricingDto Pricing { get; set; }
		public ProductLogisticsDto Logistics { get; set; }
		public ProductSafetyDto ProductSafety { get; set; }
		public string SourceSystem { get; set; } = "Unknown";

	}

	public class ProductDescriptionDto
	{
		public string Category { get; set; }
		public string BrandId { get; set; }
		public string ProductLine { get; set; }
		public DateTime? ProductionDate { get; set; }

		public bool MultiPack { get; set; }
		public bool Bundle { get; set; }
		public bool FscCertified { get; set; }
		public bool Disposal { get; set; }

		public string ProductUrl { get; set; }
		public string Description { get; set; }
	}

	public class ProductMediaDto
	{
		public string Type { get; set; } = "IMAGE";
		public string Location { get; set; } = "http://apartners.url/image-location";
	}

	public class ProductOrderWrapperDto
	{
		public MaxOrderQuantityDto MaxOrderQuantity { get; set; }
	}
	public class MaxOrderQuantityDto
	{
		public int Quantity { get; set; } = 10;
		public int PeriodInDays { get; set; } = 7;
	}

	public class ProductPricingDto
	{
		public PriceDto StandardPrice { get; set; }
		public string Vat { get; set; }
		public PriceDto Msrp { get; set; }
		public ProductSaleDto Sale { get; set; }
		public ProductNormPriceInfoDto NormPriceInfo { get; set; }
	}

	public class PriceDto
	{
		public decimal Amount { get; set; }
		public string Currency { get; set; }
	}

	public class ProductSaleDto
	{
		public PriceDto SalePrice { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
	}

	public class ProductNormPriceInfoDto
	{
		public decimal NormAmount { get; set; }
		public string NormUnit { get; set; }
		public decimal SalesAmount { get; set; }
		public string SalesUnit { get; set; }
	}

	public class ProductLogisticsDto
	{
		public int PackingUnitCount { get; set; }
		public List<ProductPackingUnitDto> PackingUnits { get; set; }
	}

	public class ProductPackingUnitDto
	{
		public int Weight { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		public int Length { get; set; }
	}

	public class ProductSafetyDto
	{
		public string Name { get; set; }
		public string Address { get; set; }
		public string RegionCode { get; set; }
		public string Email { get; set; }
		public string Url { get; set; }
		public string Phone { get; set; }
	}

}
