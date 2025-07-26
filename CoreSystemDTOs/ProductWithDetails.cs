
using CoreSystem.Shared.DTOs;

public class ProductWithDetails
    {
        public ProductDTO Product { get; set; }
        public ProductDescriptionDto Description { get; set; } 
        public ManufacturerDto Manufacturer { get; set; }
        public ConditionDto Condition { get; set; } 
    }
