using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzyBill.DAL
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEzyBillRepos(this ServiceCollection services)
        {

            return services;
        }
    }
}
