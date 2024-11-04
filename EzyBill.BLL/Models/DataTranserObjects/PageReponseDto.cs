using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzyBill.BLL.Models.DataTranserObjects
{
    public class PageReponseDto<TCollection>: ResponseDto<IEnumerable<TCollection>> 
    {
        public int TotalRecordCount { get; set; }
    }
}
