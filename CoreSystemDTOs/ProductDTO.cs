
using CoreSystem.Shared.DTOs;
using System.Collections.ObjectModel;

public class ProductDTO
    {
        public int Id { get; set; } = 0;
        public string Sku { get; set; }
        public string Name { get; set; }
        public double NetPrice { get; set; } = 0;
        public int VatTypeId { get; set; } = 0;
        public bool Enabled { get; set; }= false;
        public bool StockEnabled { get; set; } = false;
        public double PackedWeight { get; set; } = 0;
        public double Length { get; set; } = 0;
        public double Width { get; set; } = 0;
        public double Weight { get; set; } = 0;
        public int HandlingTime { get; set; } = 0;
        public string GTIN { get; set; } = string.Empty;
        public string MPN { get; set; } = string.Empty;
        public string ASIN { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public string UPC { get; set; } = string.Empty;
        public string OriginCountry { get; set; } = string.Empty;
        public string TaricCode { get; set; } = string.Empty;
        public int ManufacturerId { get; set; }
        public int SupplierId { get; set; }
        public int? OdooId { get; set; }

        public int? ResponsiblePersonGPSRId { get; set; }

        public int? ProductStockId { get; set; }

        public int? AttributeId { get; set; }

        public int? SalesChannelProductMappingId { get; set; }

        public int? CategoryId { get; set; }

        public int? kMarketplaceAccountId { get; set; }
        public int? ConditionId { get; set; }
        public string ShortDescription { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string ContactPerson { get; set; } = string.Empty;
        public string ManufacturerName { get; set; } = string.Empty;

        public string ConditionName { get; set; } = string.Empty;

        public string AttributeGroupName { get; set; } = string.Empty;
        public string AttributeName { get; set; } = string.Empty;

        public string CompanyName { get; set; } = string.Empty;
        public string CompanyNameAddition { get; set; } = string.Empty;
        public string Salutation { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Forename { get; set; } = string.Empty;
        public string Surename { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string Housenumber { get; set; } = string.Empty;
        public string AdressAddition { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Phonenumber { get; set; } = string.Empty;
        public string Mobilephonenumber { get; set; } = string.Empty;
        public string Faxnumber { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
        public int SurchargeType { get; set; } = 0;
        public double SurchargeAmount { get; set; } = 0;
        public int Quantity { get; set; } = 0;
        public int KWarehouseId { get; set; } = 0;
        public string AddressAddition { get; set; } = string.Empty;
        public string? CategoryName { get; set; } = string.Empty;
        public ObservableCollection<VariantDto>? ProductVariants { get; set; } = [];
        public List<VariantDto> Variants { get; set; } = [];


}


