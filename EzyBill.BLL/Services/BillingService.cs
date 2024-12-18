﻿using EzyBill.BLL.Enums;
using EzyBill.BLL.Interfaces;
using EzyBill.BLL.Models.DataTranserObjects;
using EzyBill.DAL.Entities;
using EzyBill.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace EzyBill.BLL.Services
{
    public class BillingService : IBillingService
    {

        private readonly CustomerRepository _customerRepository;
        private readonly ProductRepository _productRepository;
        private readonly InvoiceRepository _invoiceRepository;
        private readonly InvoiceProductRepository _invoiceProductRepository;
        private readonly ICurrentUserContext<string> _currentUserContext;
        public BillingService(
            CustomerRepository customerRepository,
            ProductRepository productRepository,
            InvoiceRepository invoiceRepository,
            InvoiceProductRepository invoiceProductRepository,
            ICurrentUserContext<string> currentUserContext
        ) 
        {
           _customerRepository = customerRepository;
           _invoiceRepository = invoiceRepository;
           _productRepository = productRepository;
           _invoiceProductRepository = invoiceProductRepository;
           _currentUserContext = currentUserContext;
        }

        public InvoiceDto? GenerateInvoice(Guid customerId, IEnumerable<Guid> productIdentifiers)
        {
            var customer = _customerRepository.
                Get(x => !x.IsDeleted && x.Id == customerId)
                .Include(x => x.Invoices).FirstOrDefault();
            if( customer == null )
            {
                return null;
            }
            var invoicedProducts = this._productRepository
                    .Get(x => productIdentifiers.Contains(x.Id)).ToList();
           
            var productIdFrequency = productIdentifiers
                .GroupBy(x => x)
                .Select(x => new { ProductId = x.Key, Frequency = x.Count() });

            var invoiceAmount = invoicedProducts //incoming existing products
                .Sum(x => x.Price * (productIdFrequency.FirstOrDefault(y => y.ProductId == x.Id)?.Frequency ?? 0));
            var invoiceE = new InvoiceEntity
            {
                AmountDue = invoiceAmount,
            };
            
            customer.Invoices.Add(invoiceE);
            this._customerRepository.Update(new List<CustomerEntity> { customer }, this._currentUserContext.CurrentUserId);
            var newInvoiceId = invoiceE.Id;
            var invoiceProducts = productIdentifiers
                .Where(pId => invoicedProducts.Any(i => i.Id == pId)) //filter out non-existing products
                .Select(x => new InvoiceProductEntity
                {
                    ProductId = x,
                    InvoiceId = newInvoiceId,

                }).ToList();
            this._invoiceProductRepository.Insert(invoiceProducts, _currentUserContext.CurrentUserId);
            this._invoiceProductRepository.SaveChanges();
            var instertedProductsForInvoice = this
                ._productRepository
                .Get(x => invoiceProducts.Select(x => x.ProductId).Contains(x.Id))
                .ToList();
            var productQty = this._invoiceProductRepository
                .Get(x => x.InvoiceId == newInvoiceId)
                .GroupBy(x => x.ProductId)
                .Select(x => new { ProductId = x.Key, Qty = x.Count() })
                .AsEnumerable().ToList();
           var invoiceDto = new InvoiceDto
            {
                
                InvoicedProducts = instertedProductsForInvoice.Select(p => new ProductLineItemDto
                {
                    Description = p.Description,
                    Name = p.Name,
                    Price = p.Price,
                    Qty = productQty.FirstOrDefault(y => y.ProductId == p.Id)?.Qty ?? 0,
                    Id = p.Id,
                }),
                Id = newInvoiceId
            };
            return invoiceDto;
        }

        public IEnumerable<InvoiceDto> GetInvoices(InvoiceFilterDto filter)
        {
            var invoiceQuery = this
                 ._invoiceRepository
                 .Get(x => !x.IsDeleted && x.CreatedOn >= filter.StartDate && x.CreatedOn <= filter.EndDate)
                 .Include(x => x.Customer)
                 .AsQueryable();

            if (filter.PaymentStatus != null)
            {
                var showOnlyPaid = filter.PaymentStatus == PaymentStatus.Paid;
                if (showOnlyPaid)
                {
                    invoiceQuery = invoiceQuery.Where(x => x.AmountDue == 0);
                }
                else
                {
                    invoiceQuery = invoiceQuery.Where(x => x.AmountDue > 0);
                }

            }
            var customerInvoices = invoiceQuery
                
                .AsEnumerable()
                .Select(x => new InvoiceDto
                {
                    CustomerName = x.Customer?.Name ?? string.Empty,
                    BillingAddressLine1 = ((x.Customer?.Address?.BuildingNumber ?? string.Empty) + " " + (x.Customer?.Address?.StreetName ?? string.Empty)).Trim(),
                    BillingAddressLine2 = (x.Customer?.Address?.City ?? string.Empty) + ", " + (x.Customer?.Address?.State ?? string.Empty) + " " + (x.Customer?.Address?.Zip4),
                    PaymentStatus = x.AmountDue == 0 ? PaymentStatus.Paid : PaymentStatus.Unpaid,
                    AmountDue = x.AmountDue,

                });
            return customerInvoices;
        }
    }
}
