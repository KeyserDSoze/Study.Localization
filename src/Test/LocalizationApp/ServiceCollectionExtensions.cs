using LocalizationApp.Resources;

namespace LocalizationApp
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInAppLocalization(this IServiceCollection services)
        {
            services.AddMultipleLocalization<Shared2>(x =>
            {
                x.ResourcesPath = string.Empty;
            });
            //services.AddLocalization(x =>
            //{
            //    //    x.ResourcesPath = "Resources";
            //    x.ResourcesPath = string.Empty;
            //});
            return services;
        }
    }
}
