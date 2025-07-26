
    public class ProductStockDTO
    {
    public int ProductStockId { get; set; } // kProductStockId
    public int StockAvailable { get; set; } // iStockAvailable
    public int StockInOrders { get; set; } // iStockInOrders
    public int StockInDelivery { get; set; } // iStockInDelivery
    public int WarehouseId { get; set; } // kWarehouseId
    public int? OdooId { get; set; } // kOdooReference
    public string ProductName { get; set; } // comes from JOIN
    public int ProductVariantId { get; set; } // kProductVariantId
    }
