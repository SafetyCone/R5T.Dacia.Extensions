using System;

using Microsoft.Extensions.DependencyInjection;


namespace R5T.Dacia
{
    public static class ServiceAction
    {
        public static DoNothingServiceAction AddedElsewhere { get; } = new DoNothingServiceAction();
        public static DoNothingServiceAction AlreadyAdded { get; } = new DoNothingServiceAction();
        //public static DoNothingServiceAction AddedElsewhere { get; } = new DoNothingServiceAction();


        public static ServiceAction<TService> New<TService>(Action action)
        {
            var serviceAction = new ServiceAction<TService>(action);
            return serviceAction;
        }

        public static ForwardedServiceAction<TService, TForwardedService> Forward<TService, TForwardedService>(Action action)
            where TForwardedService : TService
        {
            var forwardedServiceAction = new ForwardedServiceAction<TService, TForwardedService>(action);
            return forwardedServiceAction;
        }
    }


    /// <summary>
    /// Basic <see cref="IServiceAction{T}"/> implementation that runs only ONCE.
    /// Note: not thread-safe.
    /// </summary>
    public class ServiceAction<TService> : IServiceAction<TService>
    {
        #region Static

        public static DoNothingServiceAction<TService> AddedElsewhere { get; } = new DoNothingServiceAction<TService>();
        public static DoNothingServiceAction<TService> AlreadyAdded { get; } = new DoNothingServiceAction<TService>();


        public static ServiceAction<TService> New(Action action)
        {
            var serviceAction = new ServiceAction<TService>(action);
            return serviceAction;
        }

        public static ServiceAction<TService> New(Action<IServiceCollection> action)
        {
            var serviceAction = new ServiceAction<TService>(action);
            return serviceAction;
        }

        #endregion


        public Action<IServiceCollection> Action { get; }

        private bool HasRun { get; set; } = false;


        public static implicit operator ServiceAction<TService>(DoNothingServiceAction doNothingServiceAction)
        {
            var serviceAction = new ServiceAction<TService>(DoNothingServiceAction.DoNothingAction);
            return serviceAction;
        }

        public static implicit operator ServiceAction<TService>(DoNothingServiceAction<TService> doNothingServiceAction)
        {
            var serviceAction = new ServiceAction<TService>(DoNothingServiceAction.DoNothingAction);
            return serviceAction;
        }


        public ServiceAction(Action<IServiceCollection> action)
        {
            this.Action = action;
        }

        public ServiceAction(Action action)
        {
            this.Action = (services) => action();
        }

        public void Run(IServiceCollection services)
        {
            if(!this.HasRun)
            {
                this.Action(services);

                this.HasRun = true;
            }
        }
    }
}
