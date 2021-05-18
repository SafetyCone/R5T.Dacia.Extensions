using System;

using Microsoft.Extensions.DependencyInjection;

using R5T.Dacia.Extensions;


namespace R5T.Dacia
{
    public static class ServiceProviderHelperExtensions
    {
        /// <summary>
        /// Gets an empty service provider.
        /// Useful for when a configuration service provider is required, but configuration of the configuration requires no services.
        /// </summary>
        public static ServiceProvider GetEmptyServiceProvider(this ServiceProviderHelper serviceProviderHelper)
        {
            var emptyServiceProvider = new ServiceCollection()
                .BuildServiceProvider();

            return emptyServiceProvider;
        }

        public static ServiceProvider GetServiceProvider(this ServiceProviderHelper serviceProviderHelper, Action<IServiceCollection> configureServicesAction)
        {
            var serviceProvider = new ServiceCollection()
                .AddServices(configureServicesAction)
                .BuildServiceProvider();

            return serviceProvider;
        }

        /// <summary>
        /// Output the service provider so it can be used in a using statement, thus use an out parameter for the instance.
        /// </summary>
        public static ServiceProvider GetInstanceOfType<T>(this ServiceProviderHelper serviceProviderHelper, Action<IServiceCollection> configureServicesAction, out T instance)
            where T: class
        {
            void ConfigureServicesActionWrapper(IServiceCollection services)
            {
                services.AddTransient<T>();

                configureServicesAction(services);
            }

            var serviceProvider = serviceProviderHelper.GetServiceProvider(ConfigureServicesActionWrapper);

            instance = serviceProvider.GetRequiredService<T>();

            return serviceProvider;
        }

        /// <summary>
        /// Output the service provider so it can be used in a using statement, thus use an out parameter for the instance.
        /// </summary>
        public static ServiceProvider GetInstanceOfType<T>(this ServiceProviderHelper serviceProviderHelper, out T instance)
            where T: class
        {
            return serviceProviderHelper.GetInstanceOfType<T>(ServiceCollectionHelper.DoNothing, out instance);
        }

        /// <summary>
        /// Get an instance using a service provider as the constructor.
        /// Warning: The instance is allowed to escape the service provider that constructed it, which gets disposed, thus any disposable services the instance relies on will also be in a disposed state.
        /// </summary>
        public static T GetInstanceOfType<T>(this ServiceProviderHelper serviceProviderHelper)
            where T: class
        {
            using (serviceProviderHelper.GetInstanceOfType<T>(ServiceCollectionHelper.DoNothing, out T instance))
            {
                return instance;
            }
        }
    }
}
