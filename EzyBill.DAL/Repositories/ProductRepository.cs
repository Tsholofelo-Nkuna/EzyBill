using EzyBill.DAL.Entities;
using EzyBill.DAL.Repositories.Base;

namespace EzyBill.DAL.Repositories
{
    public class ProductRepository: Repository<ProductEntity>
    {
        public ProductRepository(WebDbContext context): base(context) { }
    }
}
