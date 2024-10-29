using EzyBill.BLL.Interfaces;
using EzyBill.BLL.Models.DataTranserObjects;
using EzyBill.DAL.Entities;
using EzyBill.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzyBill.BLL.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly CustomerRepository _customerRepository;
        private readonly ICurrentUserContext<string> _currentUserContext;
        public CustomerService(CustomerRepository customerRepository, ICurrentUserContext<string> currentUserContext)
        {
            _customerRepository = customerRepository;
            _currentUserContext = currentUserContext;
        }
        public IEnumerable<CustomerDto> Add(List<CustomerDto> customers)
        {
            var inserted = customers.Select(x => new CustomerEntity
            {
                Email = x.Email,
                Name = x.Name,
                Phone = x.Phone,
            }).ToList();
            this._customerRepository.Insert(inserted, _currentUserContext.CurrentUserId);
            this._customerRepository.SaveChanges();
            return inserted.Select(x => new CustomerDto { 
                Email = x.Email,
                Name = x.Name,
                Phone = x.Phone,
                Id = x.Id
            });
        }
    }
}
