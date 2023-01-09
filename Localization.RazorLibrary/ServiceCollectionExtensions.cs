﻿using Microsoft.Extensions.DependencyInjection;

namespace Localization.RazorLibrary
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLibraryLocalization(this IServiceCollection services)
        {
            services.AddGreaterLocalization(x =>
            {
                x.ResourcesPath = string.Empty;
            });
            return services;
        }
    }
}
