using BusinessLogicLayer.Implementations;
using BusinessLogicLayer.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.NetworkInformation;
using System.Text;

namespace BusinessLogicLayer.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IVragenService, VragenService>();
            services.AddTransient<ITeamsService, TeamsService>();
            services.AddTransient<IHighscoreService, HighscoreService>();
            services.AddTransient<IAbilityService, AbilityService>();

            return services;
        }
    }
}
