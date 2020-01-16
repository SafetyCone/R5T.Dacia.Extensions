using System;

using Microsoft.Extensions.DependencyInjection;


namespace R5T.Dacia
{
    public static class ServiceProviderHelper
    {
        public static IServiceProvider GetEmptyServiceProvider()
        {
            var emptyServiceProvider = new ServiceCollection()
                .BuildServiceProvider();

            return emptyServiceProvider;
        }
    }
}
