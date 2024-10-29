using EzyBill.BLL.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzyBill.BLL.Models.DataTranserObjects
{
    public class InvoiceDto
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string BillingAddressLine1 { get; set; } = string.Empty;
        public string BillingAddressLine2 { get; set; } = string.Empty;
        public IEnumerable<ProductLineItemDto> InvoicedProducts { get; set; } = Enumerable.Empty<ProductLineItemDto>();
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Unpaid;
        public double AmountDue { get; set; }
    }
}
