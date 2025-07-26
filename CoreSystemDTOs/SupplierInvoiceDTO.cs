
    public class SupplierInvoiceDTO
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public string InvoiceNumber { get; set; }
        public int PurchaseOrderId { get; set; }
        public double TotalNet { get; set; }
        public double VATAmount { get; set; }
        public double TotalGross { get; set; }
        public bool IsBooked { get; set; }
    }
