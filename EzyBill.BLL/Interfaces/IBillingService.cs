﻿using EzyBill.BLL.Models.DataTranserObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzyBill.BLL.Interfaces
{
    public interface IBillingService
    {
        public IEnumerable<InvoiceDto> GetInvoices(InvoiceFilterDto filter);
        public InvoiceDto? GenerateInvoice(Guid customerId, IEnumerable<Guid> productIdentifiers);
    }
}