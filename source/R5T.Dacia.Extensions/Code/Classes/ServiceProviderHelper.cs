using System;

using Microsoft.Extensions.DependencyInjection;


namespace R5T.Dacia
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// The <see cref="ServiceProviderHelper"/> class is instantiable to allow it to have extension methods in client libraries.
    /// </remarks>
    public class ServiceProviderHelper
    {
        #region Static

        public static Lazy<IServiceProvider> EmptyServiceProvider { get; } = new Lazy<IServiceProvider>(ServiceProviderHelper.GetEmptyServiceProvider);


        /// <summary>
        /// Allows syntactic-sugar of starting a code line with <see cref="ServiceProviderHelper"/>...
        /// </summary>
        public static ServiceProviderHelper New()
        {
            var serviceProviderHelper = new ServiceProviderHelper();
            return serviceProviderHelper;
        }

        /// <summary>
        /// A method that performs no action on an <see cref="IServiceCollection"/> instance.
        /// Useful for when a null-operation is required in creating a service provider.
        /// </summary>
        public static IServiceCollection DoNothingAction(IServiceCollection services)
        {
            // Do nothing.

            return services;
        }

        /// <summary>
        /// Gets an empty service provider.
        /// Useful for when a configuration service provider is required, but configuration of the configuration requires no services.
        /// </summary>
        public static IServiceProvider GetEmptyServiceProvider()
        {
            var emptyServiceProvider = new ServiceCollection()
                .BuildServiceProvider();

            return emptyServiceProvider;
        }

        #endregion
    }
}
