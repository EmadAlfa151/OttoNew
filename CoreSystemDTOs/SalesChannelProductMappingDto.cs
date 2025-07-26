using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSystem.Shared.DTOs
{
    public class SalesChannelProductMappingDto
    {
        public int SalesChannelProductMappingId { get; set; }
        public int MarketplaceId { get; set; }
        public bool Enabled { get; set; }
        public int SurchargeType { get; set; }
        public double SurchargeAmount { get; set; }
    }
}
