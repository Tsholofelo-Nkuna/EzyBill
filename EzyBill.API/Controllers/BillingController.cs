using EzyBill.BLL.Interfaces;
using EzyBill.BLL.Models.DataTranserObjects;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EzyBill.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingController : ControllerBase
    {
        private readonly IBillingService _billingService;
        public BillingController(IBillingService billingService) {
            _billingService = billingService;
        }

        [HttpPost("[action]")]
        public IEnumerable<InvoiceDto> GetInvoices([FromBody] InvoiceFilterDto filters)
        {
            return this._billingService.GetInvoices(filters);
        }

    }
}
