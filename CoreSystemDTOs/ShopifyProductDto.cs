
using System.Text.Json;
using System.Text.Json.Serialization;

public class ShopifyProductDto
    {
        public long Id { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("vendor")]
        public string Vendor { get; set; }

        [JsonPropertyName("body_html")]
        public string BodyHtml { get; set; }
        [JsonPropertyName("product_type")]
        public string ProductType { get; set; }
        [JsonPropertyName("tags")]
        public string Tags { get; set; }
        [JsonPropertyName("variants")]
        public List<ShopifyVariant> Variants { get; set; }

        [JsonPropertyName("images")]
        public List<ShopifyImage> Images { get; set; }

        [JsonPropertyName("options")]
        public List<ShopifyOption> Options { get; set; }

        [JsonPropertyName("category")]
        public ShopifyCategory Category { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        public List<string> Collections { get; set; }
        public string Sku { get; set; }
        public string Barcode { get; set; }
        public int AccountId { get; set; } = 0;
        public int? MarketplaceId { get; set; } 
}
    public class ShopifyVariant
    {
        [JsonPropertyName("sku")]
        public string Sku { get; set; }
        [JsonPropertyName("price")]
        public string Price { get; set; }
        [JsonPropertyName("inventory_quantity")]
        public int InventoryQuantity { get; set; }
        [JsonPropertyName("barcode")]
        public string Barcode { get; set; }
        [JsonExtensionData]
        public IDictionary<string, JsonElement>? ExtensionData { get; set; }

        /// <summary>
        /// Returns the option values sorted by their numeric suffix (1,2,3,…).
        /// </summary>
        public IReadOnlyList<string> GetOptionValues()
        {
            if (ExtensionData is null) return Array.Empty<string>();

            return ExtensionData
                   .Where(kv => kv.Key.StartsWith("option", StringComparison.OrdinalIgnoreCase))
                   .OrderBy(kv =>
                   {
                       var suffix = kv.Key.Substring("option".Length);
                       return int.TryParse(suffix, out var n) ? n : int.MaxValue;
                   })
                   .Select(kv => kv.Value.GetString() ?? string.Empty)
                   .ToList();
        }
    }
    public class ShopifyImage
    {
        [JsonPropertyName("src")]
        public string Src { get; set; }
    }

    public class ShopifyOption
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("values")]
        public List<string> Values { get; set; }
    }
    public class ShopifyProductResponse
    {
        public List<ShopifyProductDto> Products { get; set; } = new();
    }

    public class ShopifyImageDto
    {
        public string Src { get; set; }
    }
    public class ShopifyProductRequest
    {
        [JsonPropertyName("product")]
        public ShopifyProductDto Product { get; set; }
    }


    public class ShopifyTaxonomyResponse
    {
        [JsonPropertyName("product_taxonomy")]
        public ShopifyCategory ProductTaxonomy { get; set; }
    }

    public class ShopifyTaxonomyRoot
    {
        public string Version { get; set; }
        public List<ShopifyVertical> Verticals { get; set; }
    }

    public class ShopifyVertical
    {
        public string Name { get; set; }
        public string Prefix { get; set; }
        public List<ShopifyCategory> Categories { get; set; }
    }

    public class ShopifyCategory
    {
        public string Id { get; set; }
        public int Level { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        //public string Full_Name { get; set; }
        public string Parent_Id { get; set; }
        [JsonPropertyName("admin_graphql_api_id")]
        public string AdminGraphqlApiId { get; set; }

        [JsonPropertyName("full_name")]
        public string FullName { get; set; }
        public int AccountId { get; set; } = 0;
        [JsonPropertyName("children")]
        public List<ShopifyCategory>? Children { get; set; }
    }
    public class ShopifyProductDeletedDto
    {
        public long Id { get; set; }
    }




