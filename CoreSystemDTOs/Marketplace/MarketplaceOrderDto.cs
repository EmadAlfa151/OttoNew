
    public class MarketplaceOrderDto
    {
        public string OrderId { get; set; }
        public string BuyerUsername { get; set; }
        public string BuyerFullName { get; set; } // Add this
        public string CustomerEmail { get; set; } // Add this
        public double TotalPrice { get; set; }
        public DateTime OrderDate { get; set; } // Add this
        public string Status { get; set; }

        // Add the missing properties here
        public int? MarketplaceId { get; set; } // MarketplaceId
        public int? OrderStatus { get; set; }   // OrderStatus
        public DateTime? ShippingTime { get; set; }  // ShippingTime
        public int? PaymentMethodId { get; set; }    // PaymentMethodId
        public CustomerDTO Customer { get; set; }  // Customer DTO
        public CustomerAddressDto Address { get; set; }  // Address DTO
    }


