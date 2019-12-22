using System;

using Microsoft.Extensions.DependencyInjection;


namespace R5T.Dacia.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection SubTree(this IServiceCollection services, Action<IServiceCollection> action)
        {
            action(services);

            return services;
        }
    }
}
