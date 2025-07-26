
    public class EbayAccountDto
    {
        public int? Id { get; set; }
        public int MarketplaceConfigurationId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? TokenExpiry { get; set; }
        public string MarketplaceUserName { get; set; }
        public MarketplaceConfigurationDto MarketplaceConfig { get; set; }

    }


