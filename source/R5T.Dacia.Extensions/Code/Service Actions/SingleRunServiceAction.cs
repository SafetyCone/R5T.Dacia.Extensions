using System;

using Microsoft.Extensions.DependencyInjection;


namespace R5T.Dacia
{
    public class SingleRunServiceAction<TService> : IServiceAction<TService>
    {
        private bool HasRun { get; set; } = false;


        public Action<IServiceCollection> Action { get; }


        public SingleRunServiceAction(Action<IServiceCollection> action)
        {
            this.Action = action;
        }

        public SingleRunServiceAction(Action action)
        {
            this.Action = (services) => action();
        }

        public void Run(IServiceCollection services)
        {
            if (!this.HasRun)
            {
                this.Action(services);

                this.HasRun = true;
            }
        }
    }
}
