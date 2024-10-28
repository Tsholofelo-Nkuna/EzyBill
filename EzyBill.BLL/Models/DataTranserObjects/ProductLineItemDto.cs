using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzyBill.BLL.Models.DataTranserObjects
{
    public class ProductLineItemDto
    {
        public string Name { get; set; } = string.Empty;
        public int Qty { get; set; }
        public double Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public Guid ProductId { get; set; }
    }
}
