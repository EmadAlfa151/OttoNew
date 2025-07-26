using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSystem.Shared.DTOs
{
    public class CategoryDto
    {
        public int ProductCategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? UpperCategoryId { get; set; }
        public int? OdooId { get; set; }
        public string? UpperCategoryName { get; set; } = string.Empty;
    }
}
