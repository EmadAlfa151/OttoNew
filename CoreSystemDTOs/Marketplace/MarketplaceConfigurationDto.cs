
    public class MarketplaceConfigurationDto
    {
        public int Id { get; set; }
        public string MarketplaceType { get; set; } // e.g. "eBay"
        public string AccountName { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public bool UseSandbox { get; set; }
        public int? MinStock { get; set; }
        public int? MaxStock { get; set; }

    }

