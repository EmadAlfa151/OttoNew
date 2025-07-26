using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSystem.Shared.DTOs
{
    public class VariantDto
    {
        public string Sku { get; set; } = null!;
        public string Barcode { get; set; } = null!;
        public decimal NetPrice { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public Dictionary<string, string> Attributes { get; set; } = new();
        public ObservableCollection<AttributeItem>? VariantsAttributes { get; set; } = new();
    }
    public class AttributeItem
    {
        public string Key { get; set; } = "";
        public string Value { get; set; } = "";
    }
}

