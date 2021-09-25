using HealthAndBeauty.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthAndBeauty.Services
{
    public static class ServiceProviderExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<FoodSetsRepository>();
            services.AddScoped<GoogleMapsRepository>();
        }
    }
}
