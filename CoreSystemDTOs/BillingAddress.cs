using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSystem.Shared.DTOs
{
    public class BillingAddress
    {
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
    }

    public class KauflandProductWrapper
    {
        public KauflandProductDto Product { get; set; } = default!;
        public KauflandUnitDto Unit { get; set; } = default!;
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }
        public int? OdooId { get; set; }
    }

}
