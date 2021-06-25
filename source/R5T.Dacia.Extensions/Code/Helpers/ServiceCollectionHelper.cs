using System;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;


namespace R5T.Dacia
{
    public static class ServiceCollectionHelper
    {
        /// <summary>
        /// A method that performs no action on an <see cref="IServiceCollection"/> instance.
        /// Useful for when a null-operation is required in creating a service provider.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void DoNothing(IServiceCollection services)
        {
            // Do nothing.
        }

        public static async Task<ServiceCollection> ConfigureServices(ServiceCollection services, Func<ServiceCollection, Task> configureServicesAction)
        {
            await configureServicesAction(services);

            return services;
        }

        public static Task<ServiceCollection> ConfigureServices(Func<ServiceCollection, Task> configureServicesAction)
        {
            var services = new ServiceCollection();

            return ServiceCollectionHelper.ConfigureServices(services, configureServicesAction);
        }

        public static ServiceCollection ConfigureServicesSynchronous(ServiceCollection services, Action<ServiceCollection> configureServicesAction)
        {
            configureServicesAction(services);

            return services;
        }

        public static ServiceCollection ConfigureServicesSynchronous(Action<ServiceCollection> configureServicesAction)
        {
            var services = new ServiceCollection();

            configureServicesAction(services);

            return services;
        }

        public static T GetInstanceOfType<T>(ServiceCollection services)
            where T : class
        {
            services.AddSingleton<T>();

            var output = services.GetIntermediateRequiredService<T>();
            return output;
        }

        public static T GetInstanceOfTypeSynchronous<T>(Action<ServiceCollection> configureServices)
            where T : class
        {
            var services = ServiceCollectionHelper.ConfigureServicesSynchronous(configureServices);

            var output = ServiceCollectionHelper.GetInstanceOfType<T>(services);
            return output;
        }

        public static async Task<T> GetInstanceOfType<T>(Func<ServiceCollection, Task> configureServices)
            where T: class
        {
            var services = await ServiceCollectionHelper.ConfigureServices(configureServices);

            var output = ServiceCollectionHelper.GetInstanceOfType<T>(services);
            return output;
        }
    }
}
