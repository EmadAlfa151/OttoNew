
using System.Text.Json.Serialization;

public class shopfiyOrderDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("financial_status")]
        public string FinancialStatus { get; set; }

        [JsonPropertyName("fulfillment_status")]
        public string FulfillmentStatus { get; set; }

        [JsonPropertyName("customer")]
        public ShopfiyCustomer Customer { get; set; }
        [JsonPropertyName("line_items")]
        public List<LineItem> LineItems { get; set; }
        public int AccountId { get; set; } = 0;
        public int? MarketplaceId { get; set; }
}

    public class ShopfiyCustomer
    {
        public long Id { get; set; }
        public string Email { get; set; }
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; }
        public string Phone { get; set; }
        public int OrdersCount { get; set; }
        [JsonPropertyName("default_address")]
        public ShopifyAddress DefaultAddress { get; set; }
        public int AccountId { get; set; } = 0;
        public int? MarketplaceId { get; set; }
}

    public class ShopifyAddress
    {
        public string Company { get; set; }
        public string Address1 { get; set; } 
        public string Address2 { get; set; } 
        public string Zip { get; set; }
        public string City { get; set; }
        public string Province { get; set; } 
        public string Country { get; set; }
        public string Phone { get; set; }
    }

    public class ShopifyCustomerResponse
    {
        public List<ShopfiyCustomer> Customers { get; set; }
    }


    public class LineItem
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("fulfillable_quantity")]
        public int FulfillableQuantity { get; set; }

        [JsonPropertyName("fulfilled_quantity")]
        public int FulfilledQuantity { get; set; }

        [JsonPropertyName("price")]
        public string Price { get; set; } 

        [JsonPropertyName("sku")]
        public string Sku { get; set; }

        [JsonPropertyName("variant_title")]
        public string VariantTitle { get; set; }

        [JsonPropertyName("product_id")]
        public long? ProductId { get; set; }

        [JsonPropertyName("variant_id")]
        public long? VariantId { get; set; }
    }

    public class ShopifyOrderResponse
    {
        public List<shopfiyOrderDto> Orders { get; set; } = new();
    }

