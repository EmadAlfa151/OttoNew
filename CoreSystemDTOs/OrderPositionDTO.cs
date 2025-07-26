
    public class OrderPositionDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int PositionType { get; set; }
        public string Name { get; set; }
        public int ProductId { get; set; }
        public double NetPrice { get; set; }
        public double Vat { get; set; }
        public int Quantity { get; set; }
        public int QuantityShipped { get; set; }
        public string PositionHint { get; set; }
        public int FulfillableQuantity { get; set; }
        public string ProductName { get; set; }
}
