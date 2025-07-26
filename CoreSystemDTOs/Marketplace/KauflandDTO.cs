
using System.Text.Json.Serialization;

public class KauflandOrderDto
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("order_number")]
        public string OrderNumber { get; set; }

        [JsonPropertyName("billing_address")]
        public KauflandAddressDto BillingAddress { get; set; }

        [JsonPropertyName("delivery_address")]
        public KauflandAddressDto DeliveryAddress { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("order_state")]
        public string OrderState { get; set; }

        [JsonPropertyName("line_items")]
        public List<KauflandLineItemDto> LineItems { get; set; }
    }

    public class KauflandAddressDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("company")]
        public string Company { get; set; }

        [JsonPropertyName("street")]
        public string Street { get; set; }

        [JsonPropertyName("house_number")]
        public string HouseNumber { get; set; }

        [JsonPropertyName("additional")]
        public string Additional { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("postcode")]
        public string Postcode { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }
    }

    public class KauflandLineItemDto
    {
        [JsonPropertyName("product_id")]
        public long ProductId { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }
    }

    public class KauflandCategoryResponse
    {
        [JsonPropertyName("data")]
        public List<KauflandCategoryDto> Data { get; set; }
    }

    public class KauflandCategoryDto
    {
        [JsonPropertyName("id_category")]
        public long Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("title_singular")]
        public string TitleSingular { get; set; }

        [JsonPropertyName("title_plural")]
        public string TitlePlural { get; set; }

        [JsonPropertyName("level")]
        public int Level { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("is_leaf")]
        public bool IsLeaf { get; set; }

        [JsonPropertyName("id_parent_category")]
        public long? ParentId { get; set; }
    }
    public class KauflandProductDto
    {
        [JsonPropertyName("id_product")]
        public long Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("eans")]
        public List<string> Eans { get; set; }

        [JsonPropertyName("main_picture")]
        public string MainPicture { get; set; }

        [JsonPropertyName("manufacturer")]
        public string Manufacturer { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("category")]
        public KauflandCategoryDto Category { get; set; }

        // ? Optional: you can compute GTIN from EANs[0]
        public string Gtin => Eans?.FirstOrDefault();

        // ? Optional: Map brand to manufacturer for your system
        public string Brand => Manufacturer;
    }

    public class KauflandUnitResponse
    {
        [JsonPropertyName("data")]
        public List<KauflandUnitDto> Data { get; set; }
    }

    public class KauflandOrderResponse
    {
        [JsonPropertyName("data")]
        public List<KauflandOrderDto> Data { get; set; }

        [JsonPropertyName("total")]
        public int Total { get; set; }

        [JsonPropertyName("offset")]
        public int Offset { get; set; }

        [JsonPropertyName("limit")]
        public int Limit { get; set; }
    }

    public class KauflandProductResponse
    {
        [JsonPropertyName("data")]
        public KauflandProductDto Data { get; set; }

        [JsonPropertyName("total")]
        public int Total { get; set; }

        [JsonPropertyName("offset")]
        public int Offset { get; set; }

        [JsonPropertyName("limit")]
        public int Limit { get; set; }
    }

    public class KauflandUnitDto
    {
        [JsonPropertyName("id_unit")]
        public Int64 Id { get; set; }

        [JsonPropertyName("sku")]
        public string Sku { get; set; }

        [JsonPropertyName("id_product")]
        public int ProductId { get; set; }

        [JsonPropertyName("condition")]
        public string Condition { get; set; }

        [JsonPropertyName("fulfillmentType")]
        public string FulfillmentType { get; set; }

        [JsonPropertyName("stock")]
        public int Stock { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("storefront")]
        public string Storefront { get; set; }

        [JsonPropertyName("date_lastchange_iso")]
        public DateTime UpdatedAt { get; set; }
        [JsonPropertyName("amount")]
        public int Amount { get; set; }
    }

    public class KauflandCsvImageProductDto
    {
        public string Ean { get; set; }
        public string Locale { get; set; }
        public string Title { get; set; }
        public string Colour { get; set; }
        public string Picture1 { get; set; }
        public string Picture2 { get; set; }
        public string PictureUrl { get; set; }
        public string PictureAltText { get; set; }
    }
    public class RootOrders
    {
        public List<KauflandOrderDto> data { get; set; }
    }
    public class Buyer
    {
        public long id_buyer { get; set; }
        public string email { get; set; }
    }
    public class KauflandOrderUnit
    {
        [JsonPropertyName("id_order_unit")]
        public long IdOrderUnit { get; set; }

        [JsonPropertyName("id_order")]
        public string IdOrder { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("buyer")]
        public Buyer Buyer { get; set; }

        [JsonPropertyName("billing_address")]
        public KauflandAddressDto BillingAddress { get; set; }

        [JsonPropertyName("shipping_address")]
        public KauflandAddressDto ShippingAddress { get; set; }

        [JsonPropertyName("product")]
        public KauflandProductDto Product { get; set; }

        [JsonPropertyName("ts_created_iso")]
        public string TsCreatedIso { get; set; }
    }

    public class RootOrderUnits
    {
        public List<KauflandOrderUnit> data { get; set; }
    }


