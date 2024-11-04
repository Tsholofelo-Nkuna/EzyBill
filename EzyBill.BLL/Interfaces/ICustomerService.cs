using EzyBill.BLL.Models.DataTranserObjects;
using EzyBill.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzyBill.BLL.Interfaces
{
    public interface ICustomerService
    {
        public bool Add(List<CustomerDto> customers);
        public bool Update(List<CustomerDto> customers);
        public bool Delete(IEnumerable<Guid> ids);
        public IEnumerable<CustomerDto> GetCustomers(PagingPageQueryDto<CustomerDto> pageQuery, out int totalRecordCount);
    }
}
