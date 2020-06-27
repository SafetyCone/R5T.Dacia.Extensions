using System;

using Microsoft.Extensions.DependencyInjection;


namespace R5T.Dacia
{
    public class ForwardedServiceAction<TService, TForwardedService> : ServiceAction<TService>, IForwardedServiceAction<TService, TForwardedService>
        where TForwardedService : TService
    {
        public ForwardedServiceAction(Action<IServiceCollection> action)
            : base(action)
        {
        }

        public ForwardedServiceAction(Action action)
            : base(action)
        {
        }
    }
}
