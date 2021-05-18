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

        // Not lazy since generally always needed.
        public static ServiceProvider GetNewEmptyServiceProvider()
        {
            return ServiceProviderHelper.New().GetEmptyServiceProvider();
        }

        /// <summary>
        /// Allows syntactic-sugar of starting a code line with <see cref="ServiceProviderHelper"/>...
        /// </summary>
        public static ServiceProviderHelper New()
        {
            var serviceProviderHelper = new ServiceProviderHelper();
            return serviceProviderHelper;
        }

        /// <summary>
        /// Get an instance using a service provider as the constructor.
        /// Warning: The instance is allowed to escape the service provider that constructed it, which gets disposed, thus any disposable services the instance relies on will also be in a disposed state.
        /// </summary>
        public static T GetInstanceOfType<T>()
            where T: class
        {
            var instance = ServiceProviderHelperExtensions.GetInstanceOfType<T>(ServiceProviderHelper.New());
            return instance;
        }

        #endregion
    }
}
