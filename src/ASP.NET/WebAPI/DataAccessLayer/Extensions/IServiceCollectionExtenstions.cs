using DataAccessLayer.Implementations;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace DataAccessLayer.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddTransient<IVragenRepository, VragenRepository>();
            services.AddTransient<ITeamsRepository, TeamsRepository>();
            services.AddTransient<IHighscoreRepository, HighscoreRepository>();
            services.AddTransient<IAbilityRepository, AbilityRepository>();

            return services;
        }

        public static IServiceCollection RegisterORM(this IServiceCollection services, string connection)
        {
            services.AddDbContext<AppDBContext>(options =>
                options.UseSqlServer(connection));

            return services;
        }


    }
}
