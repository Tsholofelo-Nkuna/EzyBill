using EzyBill.BLL.Interfaces;
using EzyBill.BLL.Services;
using EzyBill.DAL;
using Microsoft.Extensions.DependencyInjection;


namespace EzyBill.BLL
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEzyBillServices(this IServiceCollection services)
        {
            services.AddEzyBillRepos();
            services.AddScoped<IBillingService, BillingService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IProductService, ProductService>();
            return services;
        }
    }
}
