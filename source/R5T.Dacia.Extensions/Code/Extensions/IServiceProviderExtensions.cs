using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using R5T.Dacia.Internals;


namespace System
{
    public static class IServiceProviderExtensions
    {
        public static TOutput RunFunc<TService, TOutput>(this IServiceProvider serviceProvider,
            Func<TService, TOutput> function)
        {
            var service = serviceProvider.GetRequiredService<TService>();

            var output = function(service);
            return output;
        }

        public static Task<TOutput> RunFunc<TService, TOutput>(this IServiceProvider serviceProvider,
            Func<TService, Task<TOutput>> function)
        {
            var service = serviceProvider.GetRequiredService<TService>();

            var output = function(service);
            return output;
        }
    }
}

namespace R5T.Dacia.Extensions
{
    public static class IServiceProviderExtensions
    {
        public static bool TryGetService<TService>(this IServiceProvider serviceProvider, out TService service)
            where TService: class
        {
            service = serviceProvider.GetService<TService>();

            var output = ServiceHelper.IsNullService(service);
            return output;
        }

        public static bool HasService<TService>(this IServiceProvider serviceProvider)
            where TService: class
        {
            var output = serviceProvider.TryGetService<TService>(out _);
            return output;
        }

        /// <summary>
        /// Gets multiple services added using <see cref="IServiceCollectionExtensions.AddSingletonMultipleService{TService, TImplementation}(IServiceCollection)"/>.
        /// </summary>
        public static IEnumerable<TService> GetMultipleService<TService>(this IServiceProvider serviceProvider)
        {
            var multipleServiceHolders = serviceProvider.GetServices<IMultipleServiceHolder<TService>>();

            var multipleServices = multipleServiceHolders.Select(x => x.Value).ToArray();
            return multipleServices;
        }
    }
}
