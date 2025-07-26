
    public class OrderDTO
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public int OrderStatus { get; set; }
        public string MarketplaceOrderNumber { get; set; }
        public int kMarketplaceAccountId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerHint { get; set; }
        public string InternalHint { get; set; }
        public int PaymentMethodId { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime ShippingTime { get; set; }
        public CustomerDTO Customer { get; set; } // Add the full customer data here
        public CustomerAddressDto Address { get; set; }
        public int OdooId { get; set; }
        public int? kMarketplaceId { get; set; } // Nullable to allow for cases where this might not be set
}
