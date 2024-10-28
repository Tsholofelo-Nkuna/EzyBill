using EzyBill.BLL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzyBill.BLL.Models.DataTranserObjects
{
    public class InvoiceFilterDto
    {
        public DateTime StartDate {  get; set; } = DateTime.MinValue;
        public DateTime EndDate { get; set; } = DateTime.MaxValue;
        public PaymentStatus? PaymentStatus { get; set; }
    }
}
