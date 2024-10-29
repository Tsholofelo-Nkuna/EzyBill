using EzyBill.BLL.Models.DataTranserObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzyBill.BLL.Interfaces
{
    public interface ICustomerService
    {
        public IEnumerable<CustomerDto> Add(List<CustomerDto> customers);
    }
}
