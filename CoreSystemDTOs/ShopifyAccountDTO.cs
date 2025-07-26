using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSystem.Shared.DTOs
{
    public class ShopifyAccountDTO
    {
        public int Id { get; set; }

        public string AccountName { get; set; } = null!;

        public string ApiToken { get; set; } = null!;

        public string StoreName { get; set; } = null!;

        public int MarketplaceTypeId { get; set; }
        public string MarketplaceTypeName { get; set; } 
    }
}
