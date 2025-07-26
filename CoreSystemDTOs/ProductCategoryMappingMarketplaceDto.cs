using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSystem.Shared.DTOs
{
    public class ProductCategoryMappingMarketplaceDto
    {
        public int ProductCategoryMappingMarketplacesId { get; set; }
        public int ProductCategoryId { get; set; }
        public int MarketplaceCategoryId { get; set; }
    }
}
