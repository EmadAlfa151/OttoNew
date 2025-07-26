using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSystem.Shared.DTOs
{
    public class BatchImportResult
    {
        public int Total { get; set; }
        public int SuccessCount { get; set; }
        public int FailedCount => Total - SuccessCount;
        public List<string> Errors { get; set; } = new();
    }
}
