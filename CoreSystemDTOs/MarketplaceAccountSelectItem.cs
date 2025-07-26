using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSystem.Shared.DTOs
{
    public class MarketplaceAccountSelectItem
    {
        public string DisplayName { get; set; }
        public Tuple<int, int> Value { get; set; }
    }

}
