using System;

using Microsoft.Extensions.DependencyInjection;


namespace R5T.Dacia
{
    /// <summary>
    /// An <see cref="IServiceAction{T}"/> implementation that runs its action any number of times.
    /// </summary>
    public class MulitpleRunServiceAction<T> : IServiceAction<T>
    {
        public Action<IServiceCollection> Action { get; }


        public MulitpleRunServiceAction(Action<IServiceCollection> action)
        {
            this.Action = action;
        }

        public MulitpleRunServiceAction(Action action)
        {
            this.Action = (services) => action();
        }

        public void Run(IServiceCollection services)
        {
            this.Action(services);
        }
    }
}
