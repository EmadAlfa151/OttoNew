using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSystem.Shared.DTOs
{
    public class AttributeEnabledMarketplaceDto
    {
        public int AttributeEnabledMarketplacesId { get; set; }
        public int AttributId { get; set; }
        public int MarketplaceId { get; set; }
        public bool Enabled { get; set; }
    }
}
