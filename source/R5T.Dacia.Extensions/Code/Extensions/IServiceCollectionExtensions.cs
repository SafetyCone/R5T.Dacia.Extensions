using System;

using Microsoft.Extensions.DependencyInjection;


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
    }
}
