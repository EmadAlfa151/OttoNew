using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSystem.Shared.DTOs
{
    public class ProductImageDto
    {
        public int ProductImageId { get; set; }
        public string Image { get; set; } = string.Empty;
        public int Number { get; set; } = 1;
        public byte[] ImageBytes { get; set; }
        public int ProductId { get; set; }
    }
}
