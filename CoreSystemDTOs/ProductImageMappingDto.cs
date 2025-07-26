using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSystem.Shared.DTOs
{
    public class ProductImageMappingDto
    {
        public int ProductImageMappingId { get; set; }
        public int ProductImageId { get; set; }
        public int ProductId { get; set; }
    }
}
