using EzyBill.DAL.Entities;
using EzyBill.DAL.Interfaces;
using EzyBill.DAL.Interfaces.Base;
using EzyBill.DAL.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EzyBill.DAL.Repositories
{
    public class CustomerRepository : Repository<CustomerEntity>
    {
        public CustomerRepository(WebDbContext context): base(context) { } 
    }
}
