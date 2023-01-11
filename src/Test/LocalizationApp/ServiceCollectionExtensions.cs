using LocalizationApp;
using Microsoft.Extensions.DependencyInjection;

namespace Localization.RazorLibrary
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInAppLocalization(this IServiceCollection services)
        {
            services.AddMultipleLocalization<Shared2>(x =>
            {
                x.ResourcesPath = "Resources";
            });
            return services;
        }
    }
}
