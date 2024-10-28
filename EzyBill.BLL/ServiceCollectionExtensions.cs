using EzyBill.BLL.Interfaces;
using EzyBill.BLL.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EzyBill.BLL
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEzyBillServices(this IServiceCollection services)
        {
            services.AddScoped<IBillingService, BillingService>();
            return services;
        }
    }
}
