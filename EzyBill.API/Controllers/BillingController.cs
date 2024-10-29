﻿using EzyBill.BLL.Interfaces;
using EzyBill.BLL.Models.DataTranserObjects;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EzyBill.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingController : ControllerBase
    {
        private readonly IBillingService _billingService;
        private readonly ILogger<BillingController> _logger;
        public BillingController(IBillingService billingService, ILogger<BillingController> logger)
        {
            _billingService = billingService;
            _logger = logger;
        }

        [HttpPost("[action]")]
        public IEnumerable<InvoiceDto> GetInvoices([FromBody] InvoiceFilterDto filters)
        {
            try
            {
                return this._billingService.GetInvoices(filters);
            }
            catch(Exception ex)
            {
                _logger.LogError($"{ex.Message}\n Input parametets: {JsonSerializer.Serialize(filters)}");
                return Enumerable.Empty<InvoiceDto>();
            }
        }

    }
}
