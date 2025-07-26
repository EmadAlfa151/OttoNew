using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSystem.Shared.DTOs
{
    public class PaginatedDtoResult<TDto>
    {
        public int TotalCount { get; set; }
        public int TotalItems { get; set; }
        public int PageSize { get; set; }        
        public int CurrentPage { get; set; }     
        public int TotalPages { get; set; }     
        public List<TDto> Items { get; set; }  
    }
}
