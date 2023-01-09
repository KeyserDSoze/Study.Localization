// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Globalization;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extension methods for setting up localization services in an <see cref="IServiceCollection" />.
/// </summary>
public static class LocalizationServiceCollectionExtensions
{
    /// <summary>
    /// Adds services required for application localization.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddMultipleLocalization<T>(this IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddOptions();

        AddLocalizationServices<T>(services, x =>
        {
            x.ResourcesPath = string.Empty;
        });

        return services;
    }

    /// <summary>
    /// Adds services required for application localization.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <param name="setupAction">
    /// An <see cref="Action{LocalizationOptions}"/> to configure the <see cref="LocalizationOptions"/>.
    /// </param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddMultipleLocalization<T>(
        this IServiceCollection services,
        Action<MultipleLocalizationOptions> setupAction)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        if (setupAction == null)
        {
            throw new ArgumentNullException(nameof(setupAction));
        }

        AddLocalizationServices<T>(services, setupAction);

        return services;
    }
    internal static void AddLocalizationServices<T>(
        IServiceCollection services,
        Action<MultipleLocalizationOptions> setupAction)
    {
        services.AddTransient<IStringLocalizerFactory, ResourceManagerStringLocalizerFactory>();
        services.AddTransient(typeof(IStringLocalizer<>), typeof(MultipleStringLocalizer<>));
        services.AddSingleton<IStringLocalizerFactory, MultipleResourceManagerStringLocalizerFactory>();
        var m = new MultipleLocalizationOptions
        {
            FullNameAssembly = typeof(T).Assembly.GetName().Name
        };
        setupAction?.Invoke(m);
        services.AddSingleton(m);
    }
}
