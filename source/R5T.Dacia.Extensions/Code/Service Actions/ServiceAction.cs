using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;


namespace R5T.Dacia
{
    public static class ServiceAction
    {
        public static DoNothingServiceAction AddedElsewhere { get; } = new DoNothingServiceAction();
        public static DoNothingServiceAction AlreadyAdded { get; } = new DoNothingServiceAction();
        //public static DoNothingServiceAction AddedElsewhere { get; } = new DoNothingServiceAction();


        public static DoNothingServiceAction<TService> ServiceAddedElsewhere<TService>()
        {
            return new DoNothingServiceAction<TService>();
        }

        public static IServiceAction<TService> New<TService>(Action action)
        {
            var serviceAction = new ServiceAction<TService>(action);
            return serviceAction;
        }

        public static IServiceAction<IEnumerable<TService>> NewFromEnumerable<TService>(params IServiceAction<TService>[] serviceActions)
        {
            var actions = serviceActions.Select(x => x.Action);

            var serviceAction = new CompositeServiceAction<TService>(actions);
            return serviceAction;
        }

        public static IServiceAction<IEnumerable<TService>> NewFromEnumerable<TService>(IEnumerable<IServiceAction<TService>> serviceActions)
        {
            var actions = serviceActions.Select(x => x.Action);

            var serviceAction = new CompositeServiceAction<TService>(actions);
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
    public class ServiceAction<TService> : SingleRunServiceAction<TService>
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

        #region Operators

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

        #endregion


        public ServiceAction(Action<IServiceCollection> action)
            : base(action)
        {
        }

        public ServiceAction(Action action)
            : base(action)
        {
        }
    }
}
