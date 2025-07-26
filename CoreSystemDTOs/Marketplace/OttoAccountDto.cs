
    public class OttoAccountDto
    {
        public int Id { get; set; }
        public int MarketplaceConfigurationId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? TokenExpiry { get; set; }
        public string ClientEmail { get; set; }
        public DateTime? LastSyncProducts { get; set; }
        public DateTime? LastSyncOrders { get; set; }
        public DateTime? LastSyncShipments { get; set; }
        public DateTime CreatedAt { get; set; }
        public MarketplaceConfigurationDto MarketplaceConfig { get; set; }
    }

    // Represents weight in Otto product
    public class OttoWeight
    {
        public double Value { get; set; }
        public string Unit { get; set; } = "KG"; // Default to KG
    }

    // Represents a product in Otto system
    public class OttoProduct
    {
        public string ProductId { get; set; }
        public string Sku { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string ManufacturerPartNumber { get; set; }
        public string EAN { get; set; }
        public OttoPrice Price { get; set; }
        public int Quantity { get; set; }
        public List<OttoImage> Images { get; set; }
        public Dictionary<string, string> Attributes { get; set; }
        public OttoDimensions Dimensions { get; set; }
        public OttoWeight Weight { get; set; }
        public string Condition { get; set; }
        public string CategoryId { get; set; }
    }

    // Represents the price for an Otto product
    public class OttoPrice
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "EUR"; // Default to EUR
    }

    // Represents an image in the Otto system
    public class OttoImage
    {
        public string Url { get; set; }
        public string Type { get; set; } = "PRODUCT";
    }

    // Represents dimensions of a product in Otto system
    public class OttoDimensions
    {
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public string Unit { get; set; } = "CM"; // Default to CM
    }

    // Represents a shipment in Otto system
    public class OttoShipment
    {
        public string OrderNumber { get; set; }
        public string TrackingNumber { get; set; }
        public string CarrierCode { get; set; }
        public string ShipmentDate { get; set; }
        public List<OttoShipmentPosition> Positions { get; set; }
    }

    // Represents a position in the shipment (line items)
    public class OttoShipmentPosition
    {
        public string PositionItemId { get; set; }
        public int Quantity { get; set; }
    }

    // Represents an order from Otto
    public class OttoOrder
    {
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public OttoPrice OrderTotal { get; set; }
        public OttoCustomer Customer { get; set; }
        public OttoAddress DeliveryAddress { get; set; }
        public List<OttoOrderItem> Items { get; set; }
    }

    // Represents a customer in Otto system
    public class OttoCustomer
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }

    // Represents the delivery address in Otto system
    public class OttoAddress
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string Addition { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string CountryCode { get; set; }
        public string Phone { get; set; }
    }

    // Represents an order item in Otto system
    public class OttoOrderItem
    {
        public string PositionItemId { get; set; }
        public string Sku { get; set; }
        public string ProductTitle { get; set; }
        public int Quantity { get; set; }
        public OttoPrice UnitPrice { get; set; }
    }

    public class OttoOrderResponse
    {
        public List<OttoOrder> Orders { get; set; }
        public int TotalOrders { get; set; }
    }



