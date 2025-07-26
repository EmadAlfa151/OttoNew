
    public class ShippingPackageDTO
    {
        public int Id { get; set; }
        public int ShippingMethodId { get; set; }
        public int OrderId { get; set; }
        public string TrackingNumber { get; set; }
        public double PackageWeight { get; set; }
        public DateTime CreationTime { get; set; }
    }
