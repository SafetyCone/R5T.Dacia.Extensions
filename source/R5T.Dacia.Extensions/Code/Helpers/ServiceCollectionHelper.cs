using System;

using Microsoft.Extensions.DependencyInjection;


namespace R5T.Dacia
{
    public static class ServiceCollectionHelper
    {
        /// <summary>
        /// A method that performs no action on an <see cref="IServiceCollection"/> instance.
        /// Useful for when a null-operation is required in creating a service provider.
        /// </summary>
        public static void DoNothing(IServiceCollection services)
        {
            // Do nothing.
        }
    }
}
