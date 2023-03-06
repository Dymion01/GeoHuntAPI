
using Hakaton.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Hakaton.Persistance
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("HakatonDatabase"), providerOptions => providerOptions.EnableRetryOnFailure());
            });
            services.AddScoped<IAppDbContext, AppDbContext>();
            return services;
        }
    }
}