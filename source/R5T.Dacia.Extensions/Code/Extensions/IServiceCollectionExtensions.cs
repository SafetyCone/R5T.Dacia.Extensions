using System;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using R5T.Dacia.Internals;


namespace R5T.Dacia.Extensions
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Allows separation of code-block for adding multiple services.
        /// Does not do anything special, just serves to separate code for adding the services for a multiple service.
        /// </summary>
        public static IServiceCollection AddMultipleService(this IServiceCollection services, Action<IServiceCollection> action)
        {
            action(services);

            return services;
        }

        /// <summary>
        /// Allows separation of code-block for adding services.
        /// </summary>
        public static IServiceCollection AddServices(this IServiceCollection services, Action<IServiceCollection> action)
        {
            action(services);

            return services;
        }

        /// <summary>
        /// Get a service out of the current state of the service collection.
        /// </summary>
        /// <remarks>
        /// Build a service provider from the current state of the service collection and get a required service.
        /// </remarks>
        public static T GetIntermediateRequiredService<T>(this IServiceCollection services)
        {
            var intermediateServiceProvider = services.BuildServiceProvider();

            var output = intermediateServiceProvider.GetRequiredService<T>();
            return output;
        }

        /// <summary>
        /// Adds the <typeparamref name="TImplementation"/> instance as a singleton instance (if not null), else adds the <typeparamref name="TImplementation"/> as a service type implementation.
        /// </summary>
        public static IServiceCollection AddSingletonAsTypeIfInstanceNull<TService, TImplementation>(this IServiceCollection services, TImplementation instance)
            where TService : class
            where TImplementation : class, TService
        {
            var instanceIsNullService = ServiceHelper.IsNullService(instance);
            if (instanceIsNullService)
            {
                services.AddSingleton<TService, TImplementation>();
            }
            else
            {
                services.AddSingleton<TService>(instance);
            }

            return services;
        }

        public static IServiceCollection TryAddSingletonFluent<TService>(this IServiceCollection services)
            where TService : class
        {
            services.TryAddSingleton<TService>();

            return services;
        }

        public static IServiceCollection TryAddSingletonFluent<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService
        {
            services.TryAddSingleton<TService, TImplementation>();

            return services;
        }

        /// <summary>
        /// Adds services for a multiple service in a way that allows getting services via <see cref="IServiceProviderExtensions.GetMultipleServices{TService}(IServiceProvider)"/>.
        /// </summary>
        public static IServiceCollection AddMultipleServiceSingleton<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService
        {
            services
                .AddSingleton<TImplementation>()
                .AddSingleton<IMultipleServiceHolder<TService>, MultipleServiceHolder<TImplementation>>();

            return services;
        }
    }
}
