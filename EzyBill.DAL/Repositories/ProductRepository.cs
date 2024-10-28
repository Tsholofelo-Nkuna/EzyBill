using EzyBill.DAL.Entities;
using EzyBill.DAL.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzyBill.DAL.Repositories
{
    public class ProductRepository: Repository<ProductEntity>
    {
        ProductRepository(WebDbContext context): base(context) { }
    }
}
