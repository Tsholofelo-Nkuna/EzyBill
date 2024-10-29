﻿using EzyBill.BLL.Interfaces;
using EzyBill.BLL.Models.DataTranserObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EzyBill.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;
        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        [HttpPost("[action]")]
        public IEnumerable<CustomerDto> Add(List<CustomerDto> customers)
        {
            return this._customerService.Add(customers);
        }
    }
}