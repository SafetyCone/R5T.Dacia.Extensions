using System;

using Microsoft.Extensions.DependencyInjection;


namespace R5T.Dacia
{
    /// <summary>
    /// Performs an action on a <see cref="IServiceCollection"/> instance.
    /// Useful in communicating intent while configuring the DI container.
    /// </summary>
    /// <typeparam name="T">The service definition type. The type parameter is a dummy, allowing the service action to communicate what service type of the action.</typeparam>
    public interface IServiceAction<T>
    {
        Action<IServiceCollection> Action { get; }


        void Run(IServiceCollection services);
    }
}
