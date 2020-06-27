using System;

using Microsoft.Extensions.DependencyInjection;


namespace R5T.Dacia
{
    /// <summary>
    /// Encapsulates the addition of a service <typeparamref name="TService"/> to a <see cref="IServiceCollection"/> instance.
    /// Can be run to actually add the service.
    /// Useful in communicating intent while configuring the DI container.
    /// </summary>
    /// <typeparam name="TService">The service definition type. The type parameter is a dummy, allowing the service action to communicate what service type of the action.</typeparam>
    /// <remarks>
    /// NO covariance or contravariance! This is because the Microsoft DI-continer is not covariant or contravariant.
    /// </remarks>
    public interface IServiceAction<TService>
    {
        Action<IServiceCollection> Action { get; }


        void Run(IServiceCollection services);
    }
}
