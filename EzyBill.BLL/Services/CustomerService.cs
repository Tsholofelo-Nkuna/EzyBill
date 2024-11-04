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
        public bool Add(List<CustomerDto> customers)
        {
            var inserted = customers.Select(x => new CustomerEntity
            {
                Email = x.Email,
                Name = x.Name,
                Phone = x.Phone,
            }).ToList();
            this._customerRepository.Insert(inserted, _currentUserContext.CurrentUserId);
            return this._customerRepository.SaveChanges() > 0;
            
        }

        public bool Update(List<CustomerDto> customers)
        {
            var updateIdentifers = customers.Select(x => x.Id); ;
            var toBeUpdated =  this._customerRepository
                .Get(x => !x.IsDeleted && updateIdentifers.Contains(x.Id)).ToList();
            foreach (var item in toBeUpdated)
            {
                var source = customers.FirstOrDefault(x => x.Id == item.Id);
                item.Name = source!.Name;
                item.Phone = source!.Phone;
                item.Email = source!.Email;
            }
            this._customerRepository.Update(toBeUpdated, this._currentUserContext.CurrentUserId);
            var affectedRows = this._customerRepository.SaveChanges();
            return affectedRows > 0; 
        }

        public bool Delete(IEnumerable<Guid> ids)
        {
            this._customerRepository.Delete(ids, this._currentUserContext.CurrentUserId);
            return this._customerRepository.SaveChanges() > 0;
        }

        public IEnumerable<CustomerDto> GetCustomers(PagingPageQueryDto<CustomerDto> pageQuery, out int totalRecordCount)
        {
            var customerQuery = this._customerRepository.Get(x => !x.IsDeleted);
            if(pageQuery.Filters is CustomerDto filters)
            {
                if(filters.Email is not "")
                {
                    customerQuery = customerQuery.Where(x => x.Email.Contains(filters.Email));
                }

                if(filters.Phone is not "")
                {
                    customerQuery = customerQuery.Where(x => x.Phone.Contains(filters.Phone));
                }

                if(filters.Name is not "")
                {
                    customerQuery = customerQuery.Where(x => x.Name.Contains(filters.Name));
                }
            }
            totalRecordCount = customerQuery.Count();
            var customers = customerQuery
                .Skip((pageQuery.PageIndex -1)*pageQuery.PageSize)
                .Take(pageQuery.PageSize)
                .ToList();
            return customerQuery
                .Select(x => new CustomerDto { Email = x.Email, Phone = x.Phone, Id = x.Id, Name = x.Name, });
        }
    }

}
