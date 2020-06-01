using System;

using Microsoft.Extensions.DependencyInjection;


namespace R5T.Dacia
{
    public static class ServiceAction
    {
        public static DoNothingServiceAction AddedElsewhere { get; } = new DoNothingServiceAction();
        public static DoNothingServiceAction AlreadyAdded { get; } = new DoNothingServiceAction();
        //public static DoNothingServiceAction AddedElsewhere { get; } = new DoNothingServiceAction();


        public static ServiceAction<T> New<T>(Action action)
        {
            var serviceAction = new ServiceAction<T>(action);
            return serviceAction;
        }
    }


    /// <summary>
    /// Basic <see cref="IServiceAction{T}"/> implementation that runs only ONCE.
    /// Note: not thread-safe.
    /// </summary>
    public class ServiceAction<T> : IServiceAction<T>
    {
        #region Static

        public static DoNothingServiceAction<T> AddedElsewhere { get; } = new DoNothingServiceAction<T>();
        public static DoNothingServiceAction<T> AlreadyAdded { get; } = new DoNothingServiceAction<T>();


        public static ServiceAction<T> New(Action action)
        {
            var serviceAction = new ServiceAction<T>(action);
            return serviceAction;
        }

        public static ServiceAction<T> New(Action<IServiceCollection> action)
        {
            var serviceAction = new ServiceAction<T>(action);
            return serviceAction;
        }

        #endregion


        public Action<IServiceCollection> Action { get; }

        private bool HasRun { get; set; } = false;


        public static implicit operator ServiceAction<T>(DoNothingServiceAction doNothingServiceAction)
        {
            var serviceAction = new ServiceAction<T>(DoNothingServiceAction.DoNothingAction);
            return serviceAction;
        }

        public static implicit operator ServiceAction<T>(DoNothingServiceAction<T> doNothingServiceAction)
        {
            var serviceAction = new ServiceAction<T>(DoNothingServiceAction.DoNothingAction);
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
