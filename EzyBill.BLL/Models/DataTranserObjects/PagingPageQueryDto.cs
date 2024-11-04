using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzyBill.BLL.Models.DataTranserObjects
{
    public class PagingPageQueryDto<TFilters> where TFilters : class
    {
        public TFilters? Filters { get; set; } = null;
        public int PageSize { get; set; } = 30;
        public int TotalRecordCount { get; set; }
        public int PageIndex { get; set; } = 1;

    }
}
