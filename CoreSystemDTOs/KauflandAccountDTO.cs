using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSystem.Shared.DTOs
{
    public class KauflandAccountDTO
    {
        public int Id { get; set; }
        //public DateTime? CreatedAt { get; set; }
        public int MarketplaceTypeId { get; set; }
        public string? AccountName { get; set; }
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }
        public string? MarketplaceTypeName { get; set; } // from tMarketplaceType
    }
}
