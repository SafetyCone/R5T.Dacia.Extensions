using System;


namespace R5T.Dacia
{
    public class ServiceIndirect<TService, TImplementation> : IServiceIndirect<TService>
        where TImplementation: TService
    {
        public TService Service { get; }


        public ServiceIndirect(TImplementation implementation)
        {
            this.Service = implementation;
        }
    }
}
