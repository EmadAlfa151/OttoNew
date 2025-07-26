using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSystem.Shared.DTOs
{
    public class LogLevelDto
    {
        public int LogLevelId { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}
