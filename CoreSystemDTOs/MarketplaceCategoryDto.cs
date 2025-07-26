using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSystem.Shared.DTOs
{
    public class MarketplaceCategoryDto
    {
        public int MarketplaceCategoryId { get; set; }
        public int MarketplaceType { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
