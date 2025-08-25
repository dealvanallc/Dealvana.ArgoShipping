using System;

using Dealvana.ArgoShipping;

using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddArgoShippingService(
            this IServiceCollection services,
            string configurationSection = nameof(ArgoShippingSettings),
            ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            services.Add(new ServiceDescriptor(
                typeof(ArgoShippingSettings),
                serviceProvider =>
                {
                    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                    var argoSettings = configuration.GetRequiredSection(configurationSection);
                    var settings = new ArgoShippingSettings();
                    argoSettings.Bind(settings);

                    if (string.IsNullOrEmpty(settings.Username))
                    {
                        throw new InvalidOperationException($"{configurationSection}.Username cannot be null or empty");
                    }

                    if (string.IsNullOrEmpty(settings.Password))
                    {
                        throw new InvalidOperationException($"{configurationSection}.Password cannot be null or empty");
                    }

                    return settings;
                },
                lifetime));

            services.Add(new ServiceDescriptor(
                typeof(IArgoShippingService),
                typeof(ArgoShippingService),
                lifetime));

            return services;
        }

        public static IServiceCollection AddArgoShippingService(
            this IServiceCollection services,
            ArgoShippingSettings settings,
            ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            if (string.IsNullOrEmpty(settings.Username))
            {
                throw new InvalidOperationException($"settings.Username cannot be null or empty");
            }

            if (string.IsNullOrEmpty(settings.Password))
            {
                throw new InvalidOperationException("settings.Password cannot be null or empty");
            }

            services.Add(new ServiceDescriptor(typeof(ArgoShippingSettings), settings));

            services.Add(new ServiceDescriptor(
                typeof(IArgoShippingService),
                typeof(ArgoShippingService),
                lifetime));

            return services;
        }

        public static IServiceCollection AddArgoShippingService(
            this IServiceCollection services,
            string username,
            string password,
            bool isSandbox,
            ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            return services.AddArgoShippingService(new ArgoShippingSettings
            {
                Username = username,
                Password = password,
                Sandbox = isSandbox
            });
        }
    }
}
