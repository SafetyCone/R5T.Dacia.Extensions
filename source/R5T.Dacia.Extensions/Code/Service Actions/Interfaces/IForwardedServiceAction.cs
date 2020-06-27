using System;


namespace R5T.Dacia
{
    /// <summary>
    /// Encapsulates allowing a service <typeparamref name="TForwardedService"/> to be used as a <typeparamref name="TService"/> from a <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection"/>.
    /// Useful in allowing a more specific service (like ILocalFileSystemOperator) to be used a more general service (IFileSystemOperator) by forwarding the IFileSystemOperator as an ILocalFileSystemOperator.
    /// </summary>
    /// <typeparam name="TService">The desired service.</typeparam>
    /// <typeparam name="TForwardedService">The service that is forwarded as an instance of the desired service.</typeparam>
    public interface IForwardedServiceAction<TService, TForwardedService> : IServiceAction<TService>
        where TService: TForwardedService
    {
    }
}
