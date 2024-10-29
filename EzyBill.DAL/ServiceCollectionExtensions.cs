using EzyBill.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace EzyBill.DAL
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEzyBillRepos(this IServiceCollection services)
        {
            services.AddScoped<CustomerRepository>();
            services.AddScoped<ProductRepository>();
            services.AddScoped<InvoiceRepository>();
            services.AddScoped<InvoiceProductRepository>();
            return services;
        }
    }
}
