using EzyBill.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EzyBill.DAL
{
    public class WebDbContext: IdentityDbContext
    {
        public WebDbContext(DbContextOptions<WebDbContext> options): base(options)
        {

        }
        public virtual DbSet<CustomerEntity> Customers { get; set; }
        public virtual DbSet<InvoiceEntity> Invoices { get; set; }
        public virtual DbSet<ProductEntity> Products { get; set; }
        public virtual DbSet<AddressEntity> Addresses { get; set; }
        public virtual DbSet<InvoiceProductEntity> InvoicesProducts { get; set; }
    }
}
