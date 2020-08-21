using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using R5T.Dacia.Internals;


namespace R5T.Dacia
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Requests for <typeparamref name="TService"/> are satisfied by providing an instance of <typeparamref name="TDerivedService"/>.
        /// </summary>
        public static IServiceCollection AddSingletonForward<TService, TDerivedService>(this IServiceCollection services,
            IServiceAction<TDerivedService> derivedServiceAction)
            where TService : class
            where TDerivedService : class, TService
        {
            services
                .AddSingleton<TService>(serviceProvider =>
                {
                    var service = serviceProvider.GetRequiredService<TDerivedService>();
                    return service;
                })
                .Run(derivedServiceAction)
                ;

            return services;
        }

        /// <summary>
        /// Requests for <typeparamref name="TService"/> are satisfied by providing an instance of <typeparamref name="TDerivedService"/>.
        /// </summary>
        public static IServiceAction<TService> AddSingletonForwardAction<TService, TDerivedService>(this IServiceCollection services,
            IServiceAction<TDerivedService> derivedServiceAction)
            where TService : class
            where TDerivedService : class, TService
        {
            var serviceAction = ServiceAction.New<TService>(() => services.AddSingletonForward<TService, TDerivedService>(
                derivedServiceAction));

            return serviceAction;
        }

        public static IServiceCollection RunServiceAction<TService>(this IServiceCollection services, IServiceAction<TService> serviceAction)
        {
            serviceAction.Run(services);

            return services;
        }

        /// <summary>
        /// Quality-of-life overload for <see cref="IServiceCollectionExtensions.RunServiceAction{T}(IServiceCollection, IServiceAction{T})"/>.
        /// </summary>
        public static IServiceCollection Run<TService>(this IServiceCollection services, IServiceAction<TService> serviceAction)
        {
            services.RunServiceAction(serviceAction);

            return services;
        }

        public static IServiceCollection RunServiceActions<TService>(this IServiceCollection services, IEnumerable<IServiceAction<TService>> serviceActions)
        {
            foreach (var serviceAction in serviceActions)
            {
                services.RunServiceAction(serviceAction);
            }

            return services;
        }

        /// <summary>
        /// Reruns an <see cref="IServiceAction{TService}"/>.
        /// Service actions are designed to only run once. This method allows re-running a service action.
        /// </summary>
        public static IServiceCollection Rerun<TService>(this IServiceCollection services, IServiceAction<TService> serviceAction)
        {
            // Access the service action's action directly, and run it.
            serviceAction.Action(services);

            return services;
        }
    }
}


namespace R5T.Dacia.Extensions
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Allows separation of code-block for adding multiple services.
        /// Does not do anything special, just serves to separate code for adding the services for a multiple service.
        /// </summary>
        public static IServiceCollection AddMultipleServices(this IServiceCollection services, Action<IServiceCollection> action)
        {
            action(services);

            return services;
        }

        /// <summary>
        /// Allows fluent separation of a code-block for adding services.
        /// </summary>
        public static IServiceCollection AddServices(this IServiceCollection services, Action<IServiceCollection> action)
        {
            action(services);

            return services;
        }

        public static IServiceProvider BuildIntermediateServiceProvider(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
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
        /// Adds services for a multiple service in a way that allows getting services via <see cref="IServiceProviderExtensions.GetMultipleService{TService}(IServiceProvider)"/>.
        /// </summary>
        public static IServiceCollection AddSingletonMultipleService<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService
        {
            services
                .AddSingleton<TImplementation>()
                .AddSingleton<IMultipleServiceHolder<TService>, MultipleServiceHolder<TImplementation>>();

            return services;
        }

        public static IServiceCollection AddServiceIndirects(this IServiceCollection services, Type serviceType)
        {
            var serviceIndirectServiceGenericType = typeof(IServiceIndirect<>);
            var serviceIndirectImplementationGenericType = typeof(ServiceIndirect<,>);

            var serviceServiceDescriptors = services
                .Where(x => x.ServiceType == serviceType)
                .ToList();

            foreach (var serviceServiceDescriptor in serviceServiceDescriptors)
            {
                // Remove the service service descriptor.
                services.Remove(serviceServiceDescriptor);

                // Add the service implementation type directly as itself.
                var serviceImplementationDescriptor = new ServiceDescriptor(serviceServiceDescriptor.ImplementationType, serviceServiceDescriptor.ImplementationType, serviceServiceDescriptor.Lifetime);

                services.Add(serviceImplementationDescriptor);

                // Now add the service indirect.
                var serviceIndirectServiceType = serviceIndirectServiceGenericType.MakeGenericType(serviceServiceDescriptor.ServiceType);
                var serviceIndirectImplementationType = serviceIndirectImplementationGenericType.MakeGenericType(serviceServiceDescriptor.ServiceType, serviceServiceDescriptor.ImplementationType);

                var serviceIndirectServiceDescriptor = new ServiceDescriptor(serviceIndirectServiceType, serviceIndirectImplementationType, serviceServiceDescriptor.Lifetime);
                services.Add(serviceIndirectServiceDescriptor);
            }

            return services;
        }

        public static IServiceCollection AddServiceIndirects<TService>(this IServiceCollection services)
        {
            services.AddServiceIndirects(typeof(TService));

            return services;
        }

        public static IServiceCollection RemoveServices(this IServiceCollection services, Type serviceType)
        {
            var serviceDescriptors = services
                .Where(x => x.ServiceType == serviceType)
                .ToList();

            serviceDescriptors.ForEach(serviceDescriptor =>
            {
                services.Remove(serviceDescriptor);
            });

            return services;
        }

        public static IServiceCollection RemoveServices<TService>(this IServiceCollection services)
        {
            services.RemoveServices(typeof(TService));

            return services;
        }
    }
}
