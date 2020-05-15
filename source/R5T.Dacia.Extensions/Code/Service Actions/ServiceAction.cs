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


    public class ServiceAction<T>
    {
        #region Static

        public static DoNothingServiceAction AddedElsewhere { get; } = new DoNothingServiceAction();
        public static DoNothingServiceAction AlreadyAdded { get; } = new DoNothingServiceAction();


        public static ServiceAction<T> New(Action action)
        {
            var serviceAction = new ServiceAction<T>(action);
            return serviceAction;
        }

        #endregion


        private Action<IServiceCollection> Action { get; }


        public static implicit operator ServiceAction<T>(DoNothingServiceAction doNothingServiceAction)
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
            this.Action(services);
        }
    }
}
